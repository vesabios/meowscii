using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Kobold : Monster {

	public static int seed=0;


	public override void Init ()
	{

		base.Init ();

		baseMovementCost = 90;
		baseActionCost = 40;
		baseAttackCost = 30;

		shortDisplayName = GeneratePronounceableName (6);

		disposition = -20;

		hp = 10;
		baseHitPoints = 10;
	}
		

	public override void Draw() {

		Color32 brush = Screen.GenerateBrush();
		if (IsAlive ()) {
			brush.r = (byte)Convert.ToInt32 ('k');

		} else {

			brush = Screen.GenerateBrush (59, 58, 37, 0);
		//	brush.r = (byte)Convert.ToInt32 ('x');

		}
		Screen.SetWorldPixelInScreenSpace(location, brush, Screen.Layer.FLOATING);

		/*
		if (Input.GetKey (KeyCode.S)) {
			for (int y = 0; y < 33; y++) {
				for (int x = 0; x < 60; x++) {

					int u = x + (int)Engine.player.location.x - 10;
					int v = y + (int)Engine.player.location.y - 10;

					Color32 c = Screen.GenerateBrush (dijkstra.graph [u, v], dijkstra.graph [u, v]);
					Screen.SetPixel ((uint)x, (uint)y, c, Screen.Layer.FLOATING);

				}
			}
		}
		*/




	}


	static string GeneratePronounceableName(int length)
	{
		const string vowels = "aeiou";
		const string consonants = "bcdfghjklmnpqrstvwxyz";

		var rnd = new System.Random(seed++); 
	

		length = length % 2 == 0 ? length : length + 1;

		var name = new char[length];

		for (var i = 0; i < length; i += 2)
		{
			name[i] = vowels[rnd.Next(vowels.Length)];
			name[i+1] = consonants[rnd.Next(consonants.Length)];
		}

		return new string(name);
	}

}
