using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreedyCheckpoint : Checkpoint 
{
	Checkpoint currentCheckpoint;
	public List<PlaceCheckpoint> allCheckpoints; 
    protected override void SubscribePassenger(Passenger p)
    {
		currentCheckpoint = allCheckpoints[Random.Range(0,allCheckpoints.Count)]; 
		currentCheckpoint.TrySubscribePassenger(p);
		subscribedPassengers.Remove(p);
    }

	protected override void OnDrawGizmos()
	{
		Color c= Color.magenta;
		c.a = 0.5f;
		Gizmos.color = c;
		base.OnDrawGizmos();
	}

	public override void ForceStopInteract(Passenger p)
	{
		currentCheckpoint.ForceStopInteract(p);
	}
}
