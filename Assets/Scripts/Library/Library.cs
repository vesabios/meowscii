using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ActorLibrary : MonoBehaviour {

	void Awake () {
		ActorDatabase.AddActor ((PActor)ScriptableObject.CreateInstance("Kobold"));
		ActorDatabase.AddActor ((PActor)ScriptableObject.CreateInstance("Human"));

	}

}



public class ZoneLibrary : MonoBehaviour {

	void Awake() {
		ZoneDatabase.AddZone (PStaticZone.CreateInstance("Home"));

	}


}

public class ObjectLibrary : MonoBehaviour {

	// Use this for initialization
	void Awake () {


		PItem pi = new PWand () {
			charges = 4,
			cooldownTime = 12,
			weight = 1,
			cost = 25,
			shortDisplayName = "Magic Wand",
			description = "A magical wand with charges",
		};

		ItemDatabase.AddItem (pi);

	}


}