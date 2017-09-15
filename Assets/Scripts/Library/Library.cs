using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public static class ActorLibrary  {

	public static void Init () {
		ActorDatabase.AddActor ((PActor)ScriptableObject.CreateInstance("Kobold"));
		ActorDatabase.AddActor ((PActor)ScriptableObject.CreateInstance("Human"));
		Debug.Log ("ADDED ACTOR LIBRARY ACTORS");

	}

}



public static class ZoneLibrary  {

	public static void Init() {
		ZoneDatabase.AddZone (PStaticZone.CreateInstance("Home"));

	}


}

public static class ObjectLibrary  {

	// Use this for initialization
	public static void Init () {


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