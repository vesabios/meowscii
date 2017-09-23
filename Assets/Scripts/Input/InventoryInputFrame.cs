using UnityEngine;
using System.Collections;

public class InventoryInputFrame : GUIElement
{

    public override void CheckKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { Inventory.Deactivate(); }




    }



}
