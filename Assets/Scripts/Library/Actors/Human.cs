using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : PActor {

	public Human() {

		base.Init ();

		baseMovementCost = 30;
		baseActionCost = 40;
		baseAttackCost = 30;

	}



	public override int GetAvailableActionPoints() {
		return 100000;
	}

	public override void Tick(int actionPoints) {
		return;
	}

}

/*

on Player move:

	action points spent = movementCost





*/