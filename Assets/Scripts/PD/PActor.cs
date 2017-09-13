using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System;


[System.Serializable]
public class PActor : PWorldObject { 

    public int speed;

    public List<PItem> startingInventory;
    public List<Guid> inventory;

    public List<Ability> startingAbilities;

    public SerializableDictionary<PEquipment.Slot, Guid> equipped;
    
    // this should only run ONCE, when the actor is instantiated for the first time
    public override void Init()
    {
        base.Init();

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

    public virtual bool TryMoving(Vector2 v)
    {
        v.y *= -1;

        Vector2 loc = (Vector2)(Vector3)location;

		Vector2 vertical = new Vector2 (v.y, 0);
		Vector2 horizontal = new Vector2 (0, v.x);

        if (Game.CanActorOccupyLocation(this, loc+v))
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

}


