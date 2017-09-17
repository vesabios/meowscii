using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Player {

	public static void HandleMovementInput (Vector2 moveVector) {

		PWorldObject worldObject = World.GetObjectAt (Engine.player.location + moveVector);

		int actionPointsSpent = 0;

		if (worldObject != null) {
			actionPointsSpent = InteractWithObject(worldObject);
		} else {
			actionPointsSpent = Engine.player.TryMoving(moveVector);
		}

		Engine.ProcessTurn(actionPointsSpent);

	}

	public static int InteractWithObject(PWorldObject worldObject) {

		if (worldObject is PActor) {
			return InteractWithActor ((PActor)worldObject);
		}

		return 0;
	}

	public static int InteractWithActor(PActor actor) {

		// check if it's a friendly actor or not

		return Engine.player.Attack (actor);


	}


}
