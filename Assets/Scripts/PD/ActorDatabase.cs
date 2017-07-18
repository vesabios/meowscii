using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorDatabase  {

	static private List<PActor> actors;
	static private bool isLoaded = false;

	static private void ValidateDatabase() {
		if (actors == null) actors = new List<PActor>();
		if (!isLoaded) LoadDatabase();
	}

	static public void LoadDatabase() {
		if (isLoaded) return;
		isLoaded = true;
		LoadDatabaseForce();
	}

	static public void LoadDatabaseForce() {
		ValidateDatabase();
		PActor[] resources = Resources.LoadAll<PActor>(@"");
		foreach (PActor i in resources) {
			if (!actors.Contains(i)) {
                i.LoadExternalData();
                actors.Add(i);
			}
		}
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

	static public void ClearDatabase() {
        isLoaded = false;
        actors.Clear();
	}

	static public PActor GetActor(string name) {
		ValidateDatabase();
		foreach (PActor i in actors) {

			if (i.name == name) {
				var o = ScriptableObject.Instantiate(i) as PActor;
				o.name = i.name;
                o.Init();
                o.LoadExternalData();

                return o;
			}
		}
		return null;
	}

	static public List<PActor> GetAllActors() {
		ValidateDatabase();
		return actors;
	}

}
