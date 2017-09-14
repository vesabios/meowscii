﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Passive
Patrolling
Suspicious
Hostile
*/

namespace ActorHSM
{
	using Hsm;
	using ActorStateBase = Hsm.StateT<PActor, StateData>;

	enum PriorityLevel {



	}

	class StateData
	{
		public Attribute<int> focus = new Attribute<int>(0);
		public Attribute<Vector3> waypoint = new Attribute<Vector3>(Vector3.zero);

	};

	class ActorState : ActorStateBase {
		
		protected void GetNewWaypoint() {


			//SetAttribute (Data.waypoint, Engine.player.location);
			//return;

			// for now we will find a random location in the game

			bool canOccupyNewLocation = false;

			int failsafe = 0;

			Vector2 newLocation = Vector2.zero;

			while (!canOccupyNewLocation) {

				Vector2 c = Random.insideUnitCircle;

				int x = Random.Range (1, (int)Landscape.dims.x);
				int y = Random.Range (1, (int)Landscape.dims.y);

				newLocation = (Vector2)(Vector3)Owner.location + c * 40;

				canOccupyNewLocation = Game.CanActorOccupyLocation (Owner, newLocation);

				if (failsafe++ > 1000)
					return;

			}

			SetAttribute (Data.waypoint, newLocation);

		}


		protected float DistanceRemaining() {
			return ((Owner.location - Data.waypoint.Value).sqrMagnitude);
		}

		protected bool HasWaypoint() {
			return Data.waypoint.Value != Vector3.zero;
		} 

	}




	class Root : ActorState
	{
		public override void OnEnter()
		{
			SetAttribute(Data.focus, 0);
		}

		public override Transition EvaluateTransitions()
		{
			return Transition.InnerEntry<Passive>();
		}
	}

	class Alive : ActorState {
		
		public override Transition EvaluateTransitions()
		{
			return Transition.InnerEntry<Conscious>();
		}
	}

	class Conscious : ActorState {
		public override Transition EvaluateTransitions()
		{
			return Transition.InnerEntry<Passive>();
		}
	}

	class Passive : ActorState
	{
		public override void OnEnter()
		{
			
		}

		public override Transition EvaluateTransitions()
		{
			return Transition.Sibling<Patrolling> ();
		}
	}

	class Patrolling : ActorState
	{
		public override void OnEnter()
		{

			SetAttribute (Data.focus, 10);

		}

		public override void PerformStateActions(float aDeltaTime)
		{
			SetAttribute (Data.focus, Data.focus.Value - 1);

		}

		public override Transition EvaluateTransitions()
		{
			return Transition.InnerEntry<PatrolWalking>();
		}




	}


	class PatrolWalking : ActorState {
		public override void OnEnter()
		{
			GetNewWaypoint ();
			SetAttribute (Data.focus, 120);

		}

		public override void PerformStateActions(float aDeltaTime)
		{
			// do we have a target?

			if (HasWaypoint ()) { 

				if (DistanceRemaining () > 0) {

					Owner.MoveTowardsLocation (Data.waypoint.Value);


				}
			}

		}

		public override Transition EvaluateTransitions ()
		{
			if (HasWaypoint ()) { 
				if (DistanceRemaining () < 1 || Data.focus.Value ==0)
				{
					return Transition.Sibling<PatrolLookAround> ();
				}
			}

			return Transition.None ();
		}
	}

	class PatrolLookAround : ActorState {

		public override void OnEnter() {
			SetAttribute (Data.focus, 10);
		}

		public override Transition EvaluateTransitions()
		{
			if ( Data.focus.Value==0) {
				return Transition.Sibling<PatrolWalking> ();
			}

			return Transition.None ();
		}

	}

	class Suspicious : ActorState
	{
		public override void OnEnter()
		{

		}

		public override Transition EvaluateTransitions()
		{
			return Transition.None ();
		}
	}

	class Hostile : ActorState
	{
		public override void OnEnter()
		{

		}

		public override Transition EvaluateTransitions()
		{
			return Transition.None ();

		}
	}
}
