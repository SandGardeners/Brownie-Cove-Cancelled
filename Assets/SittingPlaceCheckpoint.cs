using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingPlaceCheckpoint : PlaceCheckpoint 
{
  public float minSittingTime;
  public float maxSittingTime;
  List<Transform> emptyChairs;
  List<Transform> busyChairs;

  Transform playerChair = null;
  protected override void Awake() 
  {
    base.Awake();
    emptyChairs = new List<Transform>();  
    busyChairs = new List<Transform>();

    foreach(Chair c in GetComponentsInChildren<Chair>())
    {
      emptyChairs.Add(c.transform);
    }  
  }
	// Use this for initialization
    protected override void SubscribePassenger(Passenger p)
    {
      if(emptyChairs.Count > 0)
      {
        Transform assignedChair = emptyChairs[Random.Range(0, emptyChairs.Count)];
        emptyChairs.Remove(assignedChair);
        busyChairs.Add(assignedChair);
        p.OverrideAgent(assignedChair.position, 0.7f, 3f);
        if(p is PassengerPlayer)
        {
          playerChair = assignedChair;
        }
        p.arrived += (x) => {x.SitInChair(assignedChair); if(!(p is PassengerPlayer)) StartCoroutine(SittingForAWhile(x, assignedChair));};
      }
      else
      {
        p.Resume();
        subscribedPassengers.Remove(p);
      }
    }    

    public override void ForceStopInteract(Passenger p)
    {
      p.PlayUp();
      p.Resume();
      p.arrived = null;
      busyChairs.Remove(playerChair);
      emptyChairs.Add(playerChair);

      subscribedPassengers.Remove(p);
    }
	
    IEnumerator SittingForAWhile(Passenger p, Transform chair)
    {
      yield return new WaitForSeconds(Random.Range(minSittingTime, maxSittingTime));

      p.PlayUp();
      p.Resume();
      p.arrived = null;

      busyChairs.Remove(chair);
      emptyChairs.Add(chair);

      subscribedPassengers.Remove(p);
      yield return null;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
