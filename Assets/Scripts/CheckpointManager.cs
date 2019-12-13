using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ReflexCLI.Attributes;

public class CheckpointManager : MonoBehaviour 
{
	static CheckpointManager _instance;
	public static CheckpointManager Instance
	{
		get
		{
			return _instance;
		}
	}
	List<Checkpoint> allCheckpoints;
	EntranceCheckpoint[] entrances;
	PlaceCheckpoint[] places;
	ExitCheckpoint[] exits;
	
	private void Awake() {
		if(_instance != null)
		{
			Destroy(gameObject);
			return;
		}
		_instance = this;

		entrances = GetComponentsInChildren<EntranceCheckpoint>();
		places = GetComponentsInChildren<PlaceCheckpoint>();
		exits = GetComponentsInChildren<ExitCheckpoint>();

		allCheckpoints = new List<Checkpoint>();
		allCheckpoints.AddRange(entrances);
		allCheckpoints.AddRange(places);
		allCheckpoints.AddRange(exits);

		foreach(Checkpoint c in allCheckpoints)
		{
			NavMeshHit myPosition;
			
			if(NavMesh.SamplePosition(c.transform.position, out myPosition, 100f, NavMesh.AllAreas))
			{
				c.pathStart = myPosition.position;
				foreach(Checkpoint t in allCheckpoints)
				{
					if(t != c)
					{
						NavMeshHit theirPosition;			
						if(NavMesh.SamplePosition(t.transform.position, out theirPosition, 100f, NavMesh.AllAreas))
						{
							NavMeshPath path = new NavMeshPath();
							if(NavMesh.CalculatePath(myPosition.position, theirPosition.position, NavMesh.AllAreas, path))
							{
								c.allPaths.Add(t,path);
							}
							else
							{
								Debug.Log("Can't calculate path");
							}
						}
						else
						{
							Debug.Log("Can't sample their position");
						}
					}
				}
			}
			else
			{
				Debug.Log("Can't sample my position");
			}
		}

	}

	public static List<Checkpoint> GetRandomPath()
	{
		List<Checkpoint> path = new List<Checkpoint>();
		path.Add(GetRandomEntrance());
		List<Checkpoint> placesCopy = new List<Checkpoint>(Instance.places);
		int maxPlaces = Random.Range(1, Instance.places.Length);
		for(int i = 0; i < maxPlaces; i++)
		{
			Checkpoint randomCheckpoint = placesCopy[Random.Range(0, placesCopy.Count)];
			placesCopy.Remove(randomCheckpoint);
			path.Add(randomCheckpoint);
		}
		path.Add(GetRandomExit());

		return path;
	}

	internal static Checkpoint GetRandomPlace()
	{
		return Instance.places[Random.Range(0, Instance.places.Length)];
	}

    internal static Checkpoint GetRandomExit()
    {
        return Instance.exits[Random.Range(0, Instance.exits.Length)];
    }

    internal static Checkpoint GetRandomEntrance()
    {
        return Instance.entrances[Random.Range(0, Instance.entrances.Length)];
    }
}
