using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class PassengerAI:Passenger
{
    public float minRadius;
	public float maxRadius;
	public float minSpeed;
	public float maxSpeed;
	float saveRadius;

	float saveSpeed;
	public float minSpeechDelay = 3f;
	public float maxSpeechDelay = 15f;
	
	
    TMP_Text textBillboard;
    public TextAsset sentenceOverride;
	string[] overrideSentences;

	internal string GetSentence()
    {
        if(overrideSentences != null)
		{
			return overrideSentences[Random.Range(0,overrideSentences.Length)];
		}
		else
		{
			return PassengersManager.GetRandomSentence();
		}
    }

    protected override void Update() 
	{
		base.Update();
        textBillboard.transform.parent.LookAt(Camera.main.transform.position, Vector3.up);
		animator.SetFloat("MoveSpeed", Mathf.Lerp(0f, 1f, Mathf.InverseLerp(0f, agent.speed, agent.velocity.magnitude)));

		if(currentTarget != null)
		{
			if(Vector3.Distance(transform.position, currentTarget.transform.position) <= currentTarget.radius)
			{
				currentTarget.TrySubscribePassenger(this);
			}
		}

    }

    
	private void Awake() {
		textBillboard = GetComponentInChildren<TMP_Text>();
		textBillboard.GetComponent<SentenceReader>().passenger = this;
		
	}
    protected override void Start() 
    {
        base.Start();
        Color c = MeshColor;
        float h,s,v;
		Color.RGBToHSV(c,out h,out s, out v);
		v-= 0.25f;
		c = Color.HSVToRGB(h,s,v);
        saveRadius = agent.radius = Random.Range(minRadius,maxRadius);
		saveSpeed = agent.speed = Random.Range(minSpeed,maxSpeed);
        textBillboard.transform.position += Vector3.down * Random.Range(0f,2.5f);
		MeshRenderer textMeshRender = textBillboard.GetComponent<MeshRenderer>();
		textMeshRender.material.SetColor("_OutlineColor", c);
		textMeshRender.material.SetColor("_UnderlayColor", c);
        if(sentenceOverride != null)
		{
			overrideSentences = sentenceOverride.text.Split('\n');
		}

    }
	public override void Resume()
	{
		agent.radius = saveRadius;
		agent.speed = saveSpeed;
		agent.avoidancePriority = 50;
		NextPathTarget();
	}

    int currentPathIndex = 0;
	Checkpoint currentTarget;
	List<Checkpoint> currentPath;
	public void SetPath(List<Checkpoint> path)
	{
		currentPath = path;
		NextPathTarget();
	}

	public void NextPathTarget()
	{
		agent.radius = saveRadius;
		agent.speed = saveSpeed;
		currentPathIndex++;
		UnityEngine.AI.NavMeshPath agentPath = currentPath[currentPathIndex-1].allPaths[currentPath[currentPathIndex]];
		currentTarget = currentPath[currentPathIndex];

		if(agent.SetPath(agentPath))
		{
			agent.isStopped = false;
		}
		else
		{
			Destroy(gameObject);
			Debug.Log("Can't set path");
		}
	}
}