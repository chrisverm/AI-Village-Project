using UnityEngine;
using System.Collections.Generic;

public class WerewolfAudio : MonoBehaviour 
{
	public List<AudioSource> growls;
	private int currentGrowl;

	public void SafePlayGrowl()
	{
		if (!growls[currentGrowl].isPlaying)
		{
			currentGrowl = (currentGrowl + 1) % growls.Count;
			growls[currentGrowl].Play();
		}
	}
}
