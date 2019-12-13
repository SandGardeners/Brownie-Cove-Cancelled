using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerPlayer : Passenger {

	FPS fpsComponent;
	Rigidbody rigidbody;

	public GameObject noControlCamera;

	public GameObject image;
	bool inControl;
	private void SetControl(bool _inControl)
	{
		inControl = _inControl;
		fpsComponent.enabled = inControl;
		rigidbody.isKinematic = !inControl;
		agent.isStopped = inControl;
		noControlCamera.SetActive(!inControl);
	}
	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		rigidbody = GetComponent<Rigidbody>();
		fpsComponent = GetComponent<FPS>();
		SetControl(true);
	}
    public override void OverrideAgent(Vector3 destination, float radius = -1f, float speed = -1f)
	{
		SetControl(false);
		base.OverrideAgent(destination, radius, speed);
	}

	public override void Resume()
	{
		SetControl(true);
	}
	protected override void Update ()
	{
		base.Update();
		image.gameObject.SetActive(currentPlace != null);
		if(inControl)
		{
			if(currentPlace != null)
			{
				if(Input.GetKeyDown(KeyCode.E))
				{
					currentPlace.TrySubscribePassenger(this);
				}
			}
			animator.SetFloat("MoveSpeed", Mathf.Lerp(0f, 1f, Mathf.InverseLerp(0f, fpsComponent.speed,fpsComponent.m_velocity.magnitude)));
		}
		else
		{
			animator.SetFloat("MoveSpeed", Mathf.Lerp(0f, 1f, Mathf.InverseLerp(0f, agent.speed, agent.velocity.magnitude)));
			if(Input.GetKeyDown(KeyCode.E))
			{
				Debug.Log("Stopping yoo");
				currentPlace.ForceStopInteract(this);
			}
		}
	}

	Checkpoint currentPlace;
	private void OnTriggerStay(Collider other) {
		if(other.tag == "Place")
		{
			currentPlace = other.GetComponent<Checkpoint>();
		}
	}

	private void OnTriggerExit(Collider other) {
		if(other.tag == "Place" && other.GetComponent<Checkpoint>() == currentPlace)
		{
			currentPlace = null;
		}
	}
}
