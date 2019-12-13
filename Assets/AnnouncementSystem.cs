using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AnnouncementSystem : MonoBehaviour {

	AudioSource source;
	SentenceReader reader;

	int index = 0;
	public TextAsset[] announcementsText;
	public AudioClip[] announcementsClip;

	public float[] announcementsTime;
	// Use this for initialization
	void Start () 
	{
		reader = GetComponentInChildren<SentenceReader>();
		reader.done += ProgressEvent;
		source = GetComponent<AudioSource>();
	}

	void ProgressEvent()
	{
		switch(index)
		{
			case 1:
				TimeManager.Instance.hours = 6;
				TimeManager.Instance.minutes = 0;
				break;
			case 5:
				SceneManager.LoadScene("end");
				break;
		}
	}

	void ReadAnnouncement()
	{
		reader.FeedSentence(announcementsText[index].text);
		source.PlayOneShot(announcementsClip[index], 1f);
		index++;
	}	
	
	// Update is called once per frame
	void Update () 
	{
		if(index < announcementsTime.Length)
		{
			if(Time.realtimeSinceStartup > announcementsTime[index])
			{
				ReadAnnouncement();
			}
		}
	}
}
