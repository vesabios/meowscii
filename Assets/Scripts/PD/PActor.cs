using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System;



public interface IActor
{
	PActor Reference { get; }
}

[System.Serializable]
public class PActor : PWorldObject, IActor { 

	public PActor Reference { get { return this; } }


    public int speed;

    public List<PItem> startingInventory;
    public List<Guid> inventory;

    public List<Ability> startingAbilities;

    public SerializableDictionary<PEquipment.Slot, Guid> equipped;

	protected Dijkstra dijkstra;

	Hsm.StateMachine stateMachine;

	void OnEnable() {
		name = this.GetType ().ToString ();
		Debug.Log (">>>>" + name);
	}

    
    // this should only run ONCE, when the actor is instantiated for the first time
    public override void Init()
    {
        base.Init();

		dijkstra = (Dijkstra)ScriptableObject.CreateInstance("Dijkstra") as Dijkstra;

		stateMachine = new Hsm.StateMachine();
		stateMachine.Init<ActorHSM.Root>(this, new ActorHSM.StateData());
		//stateMachine.DebugLogLevel = 2;

        if (inventory==null)
        {
            inventory = new List<Guid>();
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


    public override void Draw() {
        Color32 brush = Screen.GenerateBrush();
        brush.r = (byte)Convert.ToInt32('@');
        Screen.SetWorldPixelInScreenSpace(location, brush, Screen.Layer.FLOATING);

    }

    public virtual bool TryMoving(Vector3 v)
    {
        v.y *= -1;

		Vector2 loc = (Vector2)(Vector3)location;

		Vector2 vertical = new Vector2 (v.y, 0);
		Vector2 horizontal = new Vector2 (0, v.x);

		if (Game.CanActorOccupyLocation(this, loc+(Vector2)v))
        {
            location.y += v.y;
            location.x += v.x;
        } 
		else if (Game.CanActorOccupyLocation(this, loc + horizontal))
        {
            location.x += horizontal.x;
        }
        else if (Game.CanActorOccupyLocation(this, loc + vertical))
        {
            location.y += vertical.y;
        }

        return true;
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
						if (Vector3.Distance (location, a.location) < 5) {
							dijkstra.SetObstacle (a.location);
						}
					}
				}
			}
		}

		dijkstra.SetOrigin (location);

		dijkstra.SetTarget(targetLocation);

		if (!towards ) {
			dijkstra.ThreadIterate();
			dijkstra.IRetreat();
			dijkstra.SetObstacle(targetLocation);
		}

		dijkstra.ThreadIterate();

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

		moveVector.y *= -1;

		TryMoving(moveVector);

		//}





	}


	public void Tick() {

		if (Engine.player == this)
			return;

		stateMachine.Update (Time.deltaTime);

	}


}


