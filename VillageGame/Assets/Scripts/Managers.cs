using UnityEngine;
using System.Collections;

public class Managers : SingletonMonoBehaviour<Managers>
{
	private static GameManager gameManager;
	private static WeatherManager weatherManager;

	public static GameManager Game { get { return gameManager; } }
	public static WeatherManager Weather { get { return weatherManager; } }

	// Use this for initialization
	void Start ()
	{
		gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
		weatherManager = (WeatherManager)FindObjectOfType(typeof(WeatherManager));
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log(Time.deltaTime);
	}
}
