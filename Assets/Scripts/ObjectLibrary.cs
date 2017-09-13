using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLibrary : MonoBehaviour {

	// Use this for initialization
	void Start () {


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
	
	// Update is called once per frame
	void Update () {
		
	}
}
