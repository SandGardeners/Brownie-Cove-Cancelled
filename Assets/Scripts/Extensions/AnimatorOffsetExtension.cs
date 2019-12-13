using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorOffsetExtension {
	public static Animator SetTimeForCurrentClip(this Animator self, float percent, int layer = 0)
	{
		AnimatorClipInfo[] cInfo = self.GetCurrentAnimatorClipInfo(layer);
		if (cInfo.Length > 0){
			AnimationClip clip = cInfo[0].clip;
			self.Play(clip.name, layer, clip.length * percent);
		}
		return self;
	}
}
