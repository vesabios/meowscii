using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System;


public class Monster : PActor {

}

public class Signal {

}

public class ActorList : List<PActor> {}

public interface IActor
{
	PActor Reference { get; }
}

[System.Serializable]
public class PActor : PWorldObject, IActor { 

	public PActor Reference { get { return this; } }


    protected int baseMovementCost; // how many action points does it cost to move 1 square
	protected int baseActionCost; // how many action points does it take to use a full action
	protected int baseAttackCost;

	protected int actionPoints;

	public SerializableDictionary<System.Guid, int> feelingsTowardActor = new SerializableDictionary<System.Guid, int> ();

    public List<PItem> startingInventory;
    public List<Guid> inventory;

    public List<Ability> startingAbilities;

	public List<Signal> signals;

    public SerializableDictionary<PEquipment.Slot, Guid> equipped;

	protected Dijkstra dijkstra;

	public Hsm.StateMachine stateMachine;

	public System.Guid targetActor = System.Guid.Empty;

	public int hp;


	public enum Alignment {
		LAWFUL_GOOD, 	NEUTRAL_GOOD, CHAOTIC_GOOD,
		LAWFUL_NEUTRAL, TRUE_NEUTRAL, CHAOTIC_NEUTRAL,
		LAWFUL_EVIL, 	NEUTRAL_EVIL, CHAOTIC_EVIL
	};


	static int ALIGNMENT_INFLUENCE = 20;
	static int FOOD_INFLUENCE = 20;
	static int COMMON_RACE_INFLUENCE = 30;
	public int disposition = 0; // what's the natural disposition. 0 is neutral, negative numbers are naturally hostily


	void OnEnable() {
		name = this.GetType ().ToString (); // SUPER FUCKING IMPORTANT, DO NOT REMOVE

		dijkstra = (Dijkstra)ScriptableObject.CreateInstance("Dijkstra") as Dijkstra;

	}

    
    // this should only run ONCE, when the actor is instantiated for the first time
    public override void Init()
    {
        base.Init();


		stateMachine = new Hsm.StateMachine();
		stateMachine.Init<ActorHSM.Root>(this, new ActorHSM.StateData());
		//stateMachine.DebugLogLevel = 2;

		if (inventory==null)
        {
            inventory = new List<Guid>();

			if (startingInventory != null) {
				foreach (PItem item in startingInventory)
				{
					if (item != null)
					{
						GameData.data.Add(ItemDatabase.GetItem(item.name));
						inventory.Add(item.guid);
					}
				}

			}
        }
    }


    public override void Draw() {
        Color32 brush = Screen.GenerateBrush();
        brush.r = (byte)Convert.ToInt32('@');
        Screen.SetWorldPixelInScreenSpace(location, brush, Screen.Layer.FLOATING);
    }

	public bool IsAlive() {
		return hp > 0;
	}

	public virtual int GetFeelingsTowardActor(PActor actor) {


		//Debug.Log ("feelings towards: " + actor.name);
		int feelings = 0;

		if (actor.GetType () == this.GetType ()) {
			feelings += COMMON_RACE_INFLUENCE;
		}

		//Debug.Log (feelings);

		// does this actor have a previous relationship with us? if so, return the value
		if (feelingsTowardActor.ContainsKey (actor.guid)) {
			int val;
			feelingsTowardActor.TryGetValue (actor.guid, out val);
			feelings += val;
		}
		//Debug.Log (feelings);

		// otherwise, does this actor have a predjudice towards this type of actor?
		feelings += CalculatePredjudiceTowards (actor);

		//Debug.Log (this.name+" feelings towards "+actor.name+" = "+feelings);

		return feelings;
	}

	public Alignment alignment;

	public int DetermineHostility( Alignment a) {
		return Mathf.Abs ((int)alignment / 3) - ((int)a/3);
	}
	public bool IsActorFood(PActor a) {
		return false;

	}

	public virtual int CalculatePredjudiceTowards(PActor actor) {

		int predjudice = 0;

		// check alignment
		if (DetermineHostility(actor.alignment) > 1) {
			predjudice -= ALIGNMENT_INFLUENCE;
		}

		if (IsActorFood(actor)) {
			predjudice -= FOOD_INFLUENCE;
		}


		// check natural enemies

		return predjudice + disposition;

	}

	public virtual int GetAttackValue () {
		return 10;
	}

	public virtual int GetAttackBonus() {
		return 0;
	}

	public virtual int GetMovementCost() {
		return baseMovementCost;
	}

	public virtual int GetActionCost() {
		return baseActionCost;
	}

	public virtual int GetAttackCost() {
		return baseAttackCost;
	}

	public virtual int GetAvailableActionPoints() {
		return actionPoints;
	}

	protected override bool IsMotile() {
		return (GetAvailableActionPoints() > GetMovementCost());
	}
		
	public virtual int TryMoving(Vector3 v) // returns actionpoints spent
    {
		if (!IsMotile ())
			return 0;


		int ap = GetMovementCost ();

		Vector2 loc = location;

		Vector2 vertical = new Vector2 (v.y, 0);
		Vector2 horizontal = new Vector2 (0, v.x);

		Vector2 initialLocation = location;

		Vector2 newLocation = location;

		if (Game.CanActorOccupyLocation (this, loc + (Vector2)v)) {
			newLocation.y += v.y;
			newLocation.x += v.x;
		} else if (Game.CanActorOccupyLocation (this, loc + horizontal)) {
			newLocation.x += horizontal.x;
		} else if (Game.CanActorOccupyLocation (this, loc + vertical)) {
			newLocation.y += vertical.y;
		} else {
			return 0;
		}


		Vector2 actualMoveVector = newLocation - initialLocation;

		if (IsVectorDiagonal (actualMoveVector)) {
			ap = Mathf.RoundToInt(ap * 1.4f); // hardcoded magic number for diagonal 
		}

		if (GetAvailableActionPoints() >= 0) {
			location = newLocation;
			actionPoints -= ap;
			return ap;
		}

		return 0;


    }


	public void MoveTowardsLocation(Vector3 targetLocation, bool towards = true) {

		dijkstra.ThreadInit();

		// place all actors into dijkstra graph as obstacles
		foreach (PD pd in GameData.data)
		{
			if (pd is PActor)
			{
				PActor a = (PActor)pd;
				if (a.zoneID == World.currentZone.guid) {
					if (a.IsAlive ()) {
						
						if (pd != this) {
							if (Vector2.Distance (location, a.location) < 2) {
								dijkstra.SetObstacle (a.location);
							}
						}
					}
				}
			}
		}

		dijkstra.SetOrigin (location); // this is where the actor will be moving FROM

		dijkstra.SetTarget(targetLocation); // this is where the actor will be trying to move TO

		if (!towards) {
			dijkstra.ThreadIterate ();
			dijkstra.Retreat ();
			dijkstra.SetObstacle (targetLocation);
			dijkstra.ThreadIterate(true); // set true to indicate moving AWAY

		} else {
			
			dijkstra.ThreadIterate();

		}


		// re-add the actor obstacles, just in case the target is also an obstacle
		foreach (PD pd in GameData.data)
		{
			if (pd is PActor)
			{
				PActor a = (PActor)pd;
				if (a.zoneID == World.currentZone.guid) {
					if (a != this) {
						if (a.IsAlive ()) {
							dijkstra.SetObstacle (a.location);

						}
					}
				}
			}
		}



		//bool traversable = dijkstra.IsTraversable(targetLocation);

		//if (traversable) {

		Vector2 moveVector = dijkstra.GetMoveVector (location);

		TryMoving(moveVector);

		//}

	}

	public bool CanSee(PActor actor) {
		// can we actually see right now?

		return true;
	}

	public ActorList GetVisibleActors() {

		ActorList results = new ActorList ();

		List<PWorldObject> worldObjects = World.GetVisibleObjects ();

		foreach (PWorldObject worldObject in worldObjects) {
			if (worldObject is PActor) {
				PActor actor = (PActor)worldObject;

				if (CanSee(actor)) {
					if (actor != this) {
						results.Add (actor);
					}
				}
			}
		}


		return results;

	}

	public PActor LookForTarget() {

		Debug.Log ("looking for target...");

		ActorList visibleActors = GetVisibleActors ();

		PActor currentActor = null;
		int score = 0;

		foreach (PActor actor in visibleActors) {

			int feelings = GetFeelingsTowardActor (actor);

			if (feelings < score) {
				score = feelings;
				currentActor = actor;
			}

		}

		return currentActor;



	}

	public void Die() {

	}

	public void TakeDamageFrom(PActor fromActor, int amount) {

		hp -= amount;

		if (hp <= 0) {
			Die ();
		}



		// damaging another actor will lower it's opinion of the attacker
		if (feelingsTowardActor.ContainsKey (fromActor.guid)) {
			feelingsTowardActor [fromActor.guid] += -amount;
		} else {
			feelingsTowardActor.Add( fromActor.guid, -amount);
		}

	}

	public int Attack(PActor targetActor) {

		// check for main weapon

		int attackRoll = Engine.AttackRoll (GetAttackValue ());

		//Debug.Log (attackRoll);

		if (attackRoll > 10) {
			int damage = Engine.DamageRoll (10);
			new VFX.MeleeAttack (targetActor.location);
			new VFX.Floater (targetActor.location, damage.ToString ());

			targetActor.TakeDamageFrom (this, damage);
			Engine.Delay (0.3f);


		} else {
			
			new VFX.Whiff (targetActor.location);
			targetActor.TakeDamageFrom (this, 0);

			Engine.Delay (0.2f);

		}



		return GetAttackCost ();
	}



	public override void Tick(int ap) {
		actionPoints += ap;
		stateMachine.Update (0);
	}


	public override bool BlocksMovement() {
		return IsAlive();
	}


}


