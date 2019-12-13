using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceCheckpoint : Checkpoint {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnDrawGizmos()
	{
		Color c= Color.green;
		c.a = 0.35f;
		Gizmos.color = c;
		base.OnDrawGizmos();
	}

    protected override void SubscribePassenger(Passenger p)
    {
    }
}
