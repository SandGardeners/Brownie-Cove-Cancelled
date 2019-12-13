using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class TimeBoard : MonoBehaviour {

	TMP_Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<TMP_Text>();
	}
	
	void Update()
	{ 
	        TimeSpan time = TimeManager.Instance.BoardingTime-TimeSpan.FromSeconds(Time.realtimeSinceStartup);
		string answer = string.Format("{0:D2}:{1:D2}:{2:D2}", 
                time.Hours, 	
                time.Minutes, 
                time.Seconds);

		text.text = "BC036 boarding in:\n<align=center><size=60>"+answer+"</size></align>";
	}
}
