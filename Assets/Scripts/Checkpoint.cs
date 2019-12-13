using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public abstract class Checkpoint : MonoBehaviour {
	protected abstract void SubscribePassenger(Passenger p);
	protected List<Passenger> subscribedPassengers;
	public void TrySubscribePassenger(Passenger p)
	{
		if(!subscribedPassengers.Contains(p))
		{
			subscribedPassengers.Add(p);
			SubscribePassenger(p);
		}
	}

	public virtual void ForceStopInteract(Passenger p)
	{

	}


	public float radius;
	public Dictionary<Checkpoint, NavMeshPath> allPaths; 

	[HideInInspector]
	public Vector3 pathStart;
	// Use this for initialization
	protected virtual void Awake() 
	{
		allPaths = new Dictionary<Checkpoint, NavMeshPath>();
		subscribedPassengers = new List<Passenger>();
	}
	

	protected virtual void OnDrawGizmos() 
	{
		Gizmos.DrawSphere(transform.position, radius);
	}
}
