using UnityEngine;
using System.Collections;

public class Managers : SingletonMonoBehaviour<Managers>
{
	private static GameManager gameManager;
	private static WeatherManager weatherManager;
	private static EntityManager entityManager;
	private static SpawningManager spawnManager;

	public static GameManager Game { get { return gameManager; } }
	public static WeatherManager Weather { get { return weatherManager; } }
	public static EntityManager Entity { get { return entityManager; } }
	public static SpawningManager Spawn { get { return spawnManager; } }

	// Use this for initialization
	void Start ()
	{
		gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
		weatherManager = (WeatherManager)FindObjectOfType(typeof(WeatherManager));
		entityManager = (EntityManager)FindObjectOfType(typeof(EntityManager));
		spawnManager = (SpawningManager)FindObjectOfType(typeof(SpawningManager));
	}
}
