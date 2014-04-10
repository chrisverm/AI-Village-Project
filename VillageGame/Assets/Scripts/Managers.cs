using UnityEngine;
using System.Collections;

public class Managers : SingletonMonoBehaviour<Managers>
{
	private static GameManager gameManager;
	private static WeatherManager weatherManager;
	private static EntityManager entityManager;

	public static GameManager Game { get { return gameManager; } }
	public static WeatherManager Weather { get { return weatherManager; } }
	public static EntityManager Entity { get { return entityManager; } }

	// Use this for initialization
	void Start ()
	{
		gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
		weatherManager = (WeatherManager)FindObjectOfType(typeof(WeatherManager));
		entityManager = (EntityManager)FindObjectOfType(typeof(EntityManager));
	}
	
	// Update is called once per frame
	void Update () { }
}
