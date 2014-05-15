using UnityEngine;
using System.Collections.Generic;

public class VillagerAudio : MonoBehaviour 
{
	public int helpDelay;
	public List<AudioSource> helpSources;

	public AudioSource deathSource;
	public AudioSource gibberishSource;

	private int currentHelp;

	public void SafePlayHelp()
	{
		if (!helpSources[currentHelp].isPlaying)
		{
            currentHelp = Random.Range(0, helpSources.Count);
			helpSources[currentHelp].PlayDelayed(helpDelay);
		}
	}

	public void SafePlayGibberish()
	{
		if (!gibberishSource.isPlaying)
		{
			gibberishSource.Play();
		}
	}

	public void SafePlayDeath()
	{
		if (!deathSource.isPlaying)
		{
            helpSources[currentHelp].Stop();
			deathSource.Play();
		}
	}
}
