using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System;

public class Signal {

}

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

    public List<PItem> startingInventory;
    public List<Guid> inventory;

    public List<Ability> startingAbilities;

	public List<Signal> signals;

    public SerializableDictionary<PEquipment.Slot, Guid> equipped;

	protected Dijkstra dijkstra;

	Hsm.StateMachine stateMachine;

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
					if (pd != this) {
						if (Vector2.Distance (location, a.location) < 2) {
							dijkstra.SetObstacle (a.location);
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
						dijkstra.SetObstacle (a.location);
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


	public int Attack(PActor actor) {
		Debug.Log ("attacking " + actor);
		return GetAttackCost ();
	}



	public override void Tick(int ap) {
		actionPoints += ap;
		stateMachine.Update (0);
	}

}


