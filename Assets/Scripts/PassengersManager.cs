using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReflexCLI.Attributes;
public class PassengersManager : MonoBehaviour 
{
	[ConsoleCommand()]
	public static string GetRandomSentence()
	{
		return Instance.sentences[Random.Range(0, Instance.sentences.Length)];
	}
	static PassengersManager _instance;
	public static PassengersManager Instance
	{
		get
		{
			return _instance;
		}
	}

	public GameObject agentPrefab;
	public TextAsset sentencesLibrary;
	public int startPassengers;
	public int maxNumberOfPassengers;
	public float minDelayNewPassengers;
	public float maxDelayNewPassengers;

	public int minSpawnNumber;
	public int maxSpawnNumber;
	float timer;
	string[] sentences;

	[ConsoleCommand()]
	static void InstantiateAgents(int nb = 1)
	{
		for(int i = 0; i < nb; i++)
		{
			Instance.AddAgent();
		}
	}
	// Use this for initialization
	void Awake() 
	{
		if(_instance != null)
		{
			Destroy(gameObject);
			return;
		}
		_instance = this;

		sentences = sentencesLibrary.text.Split('\n');
		timer = Random.Range(minDelayNewPassengers, maxDelayNewPassengers);
	}

	private void Start() {
		InstantiateAgents(startPassengers);
	}


	private void Update() 
	{
		if(transform.childCount < maxNumberOfPassengers)
		{
			timer -= Time.deltaTime;
			if(timer <= 0)
			{
				InstantiateAgents(Random.Range(minSpawnNumber, maxSpawnNumber));
				timer = Random.Range(minDelayNewPassengers, maxDelayNewPassengers);
			}
		}
	}
	
	void AddAgent()
	{
		List<Checkpoint> randomPath = CheckpointManager.GetRandomPath();
		GameObject go = Instantiate(agentPrefab, randomPath[0].pathStart, Quaternion.identity, transform);
		go.GetComponent<PassengerAI>().SetPath(randomPath);
	}
}
