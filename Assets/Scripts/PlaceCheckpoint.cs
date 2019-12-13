using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaceCheckpoint : Checkpoint {


	protected override void OnDrawGizmos()
	{
		Color c= Color.blue;
		c.a = 0.35f;
		Gizmos.color = c;
		base.OnDrawGizmos();
	}

    protected override void SubscribePassenger(Passenger p)
    {
		p.Resume();
		subscribedPassengers.Remove(p);
    }
}
