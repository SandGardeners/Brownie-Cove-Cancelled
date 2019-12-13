using System;
using UnityEngine;

public class CounterPlaceCheckpoint : PlaceCheckpoint 
{
    public Transform counter;
    public float minDelay;
    public float maxDelay;
    public Passenger host;
    public int maxQueue = 10;
    protected override void SubscribePassenger(Passenger p)
    {
      if(p is PassengerPlayer || subscribedPassengers.Count < maxQueue-1)
      {
        if(subscribedPassengers.Count == 1)
        {
          p.arrived += ReadyToAdvance;
        }
        p.OverrideAgent(counter.position+counter.forward*(2f*subscribedPassengers.Count+1), 0.7f, 3f);
      }
      else
      {
        p.Resume();
        subscribedPassengers.Remove(p);
      }
    }    

    void ReadyToAdvance(Passenger lol)
    {
      float delay = (lol is PassengerPlayer)?5f:UnityEngine.Random.Range(minDelay,maxDelay);
      Invoke("PlayInteract", delay-1f);
      if(host != null)
      {
        Invoke("PlayCounterInteract", delay-1.5f);
      } 
      Invoke("TryAdvanceQueue",delay);
    }

    public override void ForceStopInteract(Passenger p)
    {
      Debug.Log("Stopping player yooo");
      AdvanceQueue(subscribedPassengers.IndexOf(p));
    }

    void PlayCounterInteract()
    {
      host.Interact();
    }
    void PlayInteract()
    {
      if(subscribedPassengers.Count > 0)
      {
        Passenger p = subscribedPassengers[0];
        p.Interact();
      }
    }

    void TryAdvanceQueue()
    {
      AdvanceQueue();
    }

    void AdvanceQueue(int index = 0)
    {
      if(index < subscribedPassengers.Count)
      {

        Passenger p = subscribedPassengers[index];
        p.Resume();
        p.arrived = null;
        subscribedPassengers.Remove(p);
        for(int i = index; i < subscribedPassengers.Count; i++)
        {
          if(i == 0)
          {
            subscribedPassengers[i].arrived += ReadyToAdvance;
          }
          subscribedPassengers[i].OverrideAgent(counter.position+counter.forward*(2f*i+1), 0.7f, 3f);
        }
      }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
      for(int i = 0; i < maxQueue; i++)
      {
        Gizmos.DrawSphere(counter.position+counter.forward*(2f*i+1), 0.5f);
      }  
    }
}