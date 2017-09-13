using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase  {

	static private List<PItem> items;
	static private bool isLoaded = false;

	static private void ValidateDatabase() {
		if (items==null) items = new List<PItem>();
		if (!isLoaded) LoadDatabase();
	}

	static public void LoadDatabase() {
		if (isLoaded) return;
		isLoaded = true;
		LoadDatabaseForce();
	}

	static public void LoadDatabaseForce() {
		ValidateDatabase();
		PItem[] resources = Resources.LoadAll<PItem>(@"");
		foreach (PItem i in resources) {
			if (!items.Contains(i)) {
				items.Add(i);
			}
		}
	}

	static public void ClearDatabase() {
		isLoaded = false;
		items.Clear();
	}

	static public PItem GetItem(string name) {
		ValidateDatabase();
		foreach (PItem i in items) {

			if (i.name == name) {
				var o = ScriptableObject.Instantiate(i) as PItem;
				o.name = name;
				o.Init();
				return o;
			}
		}
		return null;
	}

	public static void AddItem(PItem item) {
		items.Add (item);
	}

	static public List<PItem> GetAllItems() {
		ValidateDatabase();
		return items;
	}

}
