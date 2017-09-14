using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZoneDatabase  {


	static private List<PZone> zones;
	static private bool isLoaded = false;

	static private void ValidateDatabase() {
		if (zones==null) zones = new List<PZone>();
		if (!isLoaded) LoadDatabase();
	}

	static public void LoadDatabase() {
		if (isLoaded) return;
		isLoaded = true;
		LoadDatabaseForce();
	}

	static public void LoadDatabaseForce() {
		ValidateDatabase();
		PZone[] resources = Resources.LoadAll<PZone>(@"");
		foreach (PZone i in resources) {
			if (!zones.Contains(i)) {
				zones.Add(i);
			}
		}
	}

	static public void ClearDatabase() {
		isLoaded = false;
		zones.Clear();
	}

	static public PZone GetZone(string name) {
		ValidateDatabase();
		foreach (PZone i in zones) {

			if (i.name == name) {
				var o = ScriptableObject.Instantiate(i) as PZone;
				o.name = name;
				o.Init();
				return o;
			}
		}
		return null;
	}

	static public void AddZone(PZone zone) {
		ValidateDatabase ();

		zones.Add (zone);
	}

	static public List<PZone> GetAllZones() {
		ValidateDatabase();
		return zones;
	}

}
