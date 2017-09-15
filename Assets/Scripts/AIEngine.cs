using System.Collections;
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

	class SpottedActorSignal : Signal {

		public PActor target;

	}

	class StateData
	{
		public Attribute<int> focus = new Attribute<int>(0);
		public Attribute<Vector3> waypoint = new Attribute<Vector3>(Vector3.zero);

	};

	class ActorState : ActorStateBase {

		List<Signal> signals;

		protected void CheckSignals() {
			//Owner.signals;
		}

		protected int GetMinDesiredDistanceFromTarget() {
			return 6;
		}

		protected int GetMaxDesiredDistanceFromTarget() {
			return 10;
		}

		protected int GetNewWaypointSearchRadius() {
			return 40;
		}
		
		protected void GetNewRandomWaypoint() {

			// for now we will find a random location in the game

			bool canOccupyNewLocation = false;

			int failsafe = 0;

			Vector2 newLocation = Vector2.zero;

			while (!canOccupyNewLocation) {

				Vector2 c = Random.insideUnitCircle;

				newLocation = Owner.location + c * GetNewWaypointSearchRadius();

				canOccupyNewLocation = Game.CanActorOccupyLocation (Owner, newLocation);

				if (failsafe++ > 100)
					return;

			}

			SetAttribute (Data.waypoint, newLocation);

		}


		protected float DistanceSqrRemaining() {
			return ((Owner.location - Data.waypoint.Value).sqrMagnitude);
		}

		protected float DistanceRemaining() {
			return ((Owner.location - Data.waypoint.Value).magnitude);
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

		public override void PerformStateActions(int actionPoints)
		{
			SetAttribute (Data.focus, Data.focus.Value - 1);

		}

		public override Transition EvaluateTransitions()
		{
			CheckSignals ();

			// if we get a signal

			return Transition.InnerEntry<PatrolWalking>();
		}
	}


	class PatrolWalking : ActorState {
		public override void OnEnter()
		{
			GetNewRandomWaypoint ();
			SetAttribute (Data.focus, 120);

		}

		public override void PerformStateActions(int actionPoints)
		{
			// do we have a target?

			if (HasWaypoint ()) { 

				float distance = DistanceRemaining ();

				if (distance > 0) {
					SetAttribute (Data.waypoint, Engine.player.location);

					if (distance < GetMinDesiredDistanceFromTarget()) {
						Owner.MoveTowardsLocation (Data.waypoint.Value, false);

					} else if (distance > GetMaxDesiredDistanceFromTarget()) {
						Owner.MoveTowardsLocation (Data.waypoint.Value, true);
					}

				}
			}

		}

		public override Transition EvaluateTransitions ()
		{
			if (HasWaypoint ()) { 
				if (DistanceSqrRemaining () < 1 || Data.focus.Value ==0)
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
