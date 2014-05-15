using UnityEngine;
using System.Collections.Generic;

public class WeatherAudio : MonoBehaviour 
{
    public List<AudioSource> weatherSounds;

    public void PlayRoundBegin(Condition condition)
    {
        weatherSounds[(int)condition].Play();
    }
}
