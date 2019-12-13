using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using DG.Tweening;

public abstract class Passenger : MonoBehaviour {

	NavMeshAgent _agent;

	bool overrided = false;

	public System.Action<Passenger> arrived;

    public virtual void OverrideAgent(Vector3 destination, float radius = -1f, float speed = -1f)
    {
		overrided = true;
		if(radius != -1f)
			agent.radius = radius;
		if(speed != -1f)
			agent.speed = speed;
        agent.SetDestination(destination);
		agent.avoidancePriority = 5;
    }

	public void Interact()
	{
		animator.SetTrigger("Interact");
	}

	
    internal void PlaySit()
    {
        animator.SetBool("isSitting", true);
    }
	bool sat = false;
	internal void PlayUp()
	{
		if(sat == true)
		{
			transform.position = savePosition;
			sat = false;
		}
		agent.enabled = true;
		animator.SetBool("isSitting", false);
	}
	public virtual void Resume()
	{
		
	}


    protected NavMeshAgent agent
	{
		get
		{
			if(_agent == null)
			{
				_agent = GetComponent<NavMeshAgent>();
			}
			return _agent;
		}
	}

	public Color MeshColor
	{
		get
		{
			return meshRenderer.material.GetColor("_MainColor");
		}
		set
		{
			meshRenderer.material.SetColor("_MainColor", value);
		}
	}
	
	Vector3 savePosition;
    internal void SitInChair(Transform assignedChair)
    {
		sat = true;
		savePosition = transform.position;
		agent.enabled = false;
        transform.position = assignedChair.transform.position;
		transform.rotation = assignedChair.transform.rotation; 
		PlaySit();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    protected Animator animator;
	protected SkinnedMeshRenderer meshRenderer;


	// Use this for initialization
	protected virtual void Start () 
	{
	
		meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
		Mesh mesh = meshRenderer.sharedMesh;
		for(int i = 0; i < mesh.blendShapeCount; i++)
		{
			meshRenderer.SetBlendShapeWeight(i,Random.Range(0f,100f));
		}

		Color c = Random.ColorHSV(0f,1f,0f,0.9f,0.3f,1f,1f,1f);
		MeshColor = c;

		animator = GetComponent<Animator>();
	}

	public Tweener DOSetColor(Color c, float duration)
	{
		return DOTween.To(()=>MeshColor, (x)=>MeshColor = x, c, duration);
	}
	
	
	
	// Update is called once per frame
	protected virtual void Update () {
	
		if(overrided)
		{
			if(!agent.pathPending && !agent.hasPath)
			{
				overrided = false;
				if(arrived != null)
					arrived.Invoke(this);
			}
		}
	}
}
