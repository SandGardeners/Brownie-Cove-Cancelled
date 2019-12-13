using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Utility/Animation Offset")]
public class AnimationOffset : MonoBehaviour {
	public bool randomizeOffset = false;

	[Range(0,1)]
	public float startTime;
	private Animator animator;
	protected bool randomSet = false;

	void Start () {
		animator = GetComponent<Animator>();
	}

	void Update(){
		if (randomizeOffset){
			if (!randomSet){
				animator.SetTimeForCurrentClip(Random.value);
				randomSet = true;
			}
		}else{
			if (!randomSet){
				animator.SetTimeForCurrentClip(startTime);
				randomSet = true;
			}
		}
	}
}
