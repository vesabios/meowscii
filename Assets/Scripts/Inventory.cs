using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class Inventory : ScreenComponent {

    public static Inventory instance;
    public static InventoryInputFrame inputFrame;

	List<PItem> allItems;

    GUIElement inspector;

    PD objectToInspect;

	PItem selectedItem;

	Pix backgroundPix;

    void Awake()
    {
        active = false;
        instance = this;
		inputFrame = gameObject.AddComponent<InventoryInputFrame>();


		backgroundPix = Pix.Load ("parchment");
    }



    void CreateObjectCreationWindow()
    {
        inspector = gameObject.AddComponent<GUIFrame>();
        inspector.rect = new Rect(1, 4, 20, 13);
        GUIElement.rootElement = inputFrame;

        GUIText headerText = (GUIText)inspector.AddChild<GUIText>();
        headerText.text = "Object Selector";
        headerText.rect = new Rect(1, 4, 30, 1);


        allItems = ItemDatabase.GetAllItems();
		List<string> displayItems = new List<string>();
        foreach (PItem item in allItems)
            displayItems.Add(item.shortDisplayName);

        GUIListBox listBox = (GUIListBox)inspector.AddChild<GUIListBox>();
        listBox.rect = new Rect(1, 1, inspector.rect.width-2, 10);

		listBox.SetItems (displayItems);

			//new System.Action<int>((int index) => this.Callback(index))); // yowza... well, it works.

		listBox.listBoxCallback += Callback;
    }

	void Callback(int index) {
		Debug.Log (index + " selected");
		selectedItem = allItems [index];

	}

    void CreateObjectInspectorWindow()
    {
        objectToInspect = Engine.player;
        if (objectToInspect == null) return;

        inspector = gameObject.AddComponent<GUIFrame>();
        inspector.rect = new Rect(0, 0, 20, 31);
        GUIElement.rootElement = inputFrame;

        GUIText headerText = (GUIText)inspector.AddChild<GUIText>();
        headerText.text = objectToInspect.name;
        headerText.rect = new Rect(1, 1, 30, 1);


        int y = 3;

        foreach (FieldInfo field in objectToInspect.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance))
        {
            if (
                (field.FieldType == typeof(int)))
 
            {
                GUIElement row = (GUIElement)inspector.AddChild<GUIElement>();
                row.rect.position = new Vector2(1, y);

                GUIText fieldNameText = (GUIText)row.AddChild<GUIText>();
                fieldNameText.text = field.Name;

                GUIInt fieldValueText = (GUIInt)row.AddChild<GUIInt>();
                fieldValueText.bCanActivate = true;

                fieldValueText.field = field;
                fieldValueText.objectToInspect = objectToInspect;
                fieldValueText.value = (int)field.GetValue(objectToInspect);
                fieldValueText.rect.position = new Vector2(10, 0);

                y += 2;

            }
        }
    }

    void DestroyWindow()
    {

  
        Destroy(inspector);
    }

    public static void Activate()
    {
        instance.active = true;
        inputFrame.Activate();

		//instance.CreateObjectInspectorWindow ();

        instance.CreateObjectCreationWindow();
    }

    public static void Deactivate()
    {
        instance.active = false;
        PlayerInputFrame.instance.Activate();

        instance.DestroyWindow();
    }

    public override void ScreenUpdate()
    {
        if (active)
        {

			backgroundPix.Draw (new Vector2(0, 0));

            inspector.Draw(new Vector2(1, 1));
        }

    }

    public override void PrimaryDown(Vector2 pos)
    {
        if (inspector.PrimaryDown(pos)) return;

		PItem item = (PItem)ItemDatabase.GetItem(selectedItem.name);
        item.location = (Vector3)pos+(Vector3)World.view.position;
        World.AddWorldObject(item);

        Debug.Log("place item at: " + pos + "?");
    }

}
