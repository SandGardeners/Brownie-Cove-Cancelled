using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

	static TimeManager _instance;
	public static TimeManager Instance
	{
		get
		{
			return _instance;
		}
	}
	
	public int hours;
	public int minutes;
	public int seconds;
	
	public System.TimeSpan BoardingTime
	{
		get
		{
			return new System.TimeSpan(hours,minutes,seconds);
		}
	}
	// Use this for initialization
	void Start () 
	{
		if(_instance != null)
		{
			Destroy(gameObject);
			return;
		}
		_instance = this;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
