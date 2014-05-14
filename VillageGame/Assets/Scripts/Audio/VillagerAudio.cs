using UnityEngine;
using System.Collections.Generic;

public class VillagerAudio : MonoBehaviour {

	public List<AudioSource> helpSources;
	public int helpDelay;

	private int currentHelp;

	public void SafePlayHelp()
	{
		if (!helpSources[currentHelp].isPlaying)
		{
            currentHelp = Random.Range(0, helpSources.Count);
            Debug.Log(currentHelp);
			helpSources[currentHelp].PlayDelayed(helpDelay);
		}
	}
}
