using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Managers : SingletonMonoBehaviour<Managers>
{
	private static GameManager gameManager;
	private static WeatherManager weatherManager;
	private static EntityManager entityManager;
	private static SpawningManager spawnManager;

	private static Dictionary<string, DecisionTree> decDictionary;

	public static GameManager Game { get { return gameManager; } }
	public static WeatherManager Weather { get { return weatherManager; } }
	public static EntityManager Entity { get { return entityManager; } }
	public static SpawningManager Spawn { get { return spawnManager; } }

	public static Dictionary<string, DecisionTree> DecDictionary { get { return decDictionary; } }


	// Use this for initialization
	void Start ()
	{
		gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
		weatherManager = (WeatherManager)FindObjectOfType(typeof(WeatherManager));
		entityManager = (EntityManager)FindObjectOfType(typeof(EntityManager));
		spawnManager = (SpawningManager)FindObjectOfType(typeof(SpawningManager));

		LoadDecisionTrees ();

	}

	void LoadDecisionTrees()
	{
		decDictionary = new Dictionary<string, DecisionTree>();
		decDictionary.Add("v00", new DecisionTree("Assets/Resources/VillagerDecisionTree.txt"));
		decDictionary.Add("v01", new DecisionTree("Assets/Resources/VillagerDecisionTreeBlood.txt"));
		decDictionary.Add("v02", new DecisionTree("Assets/Resources/VillagerDecisionTreeNew.txt"));
		decDictionary.Add("w00", new DecisionTree("Assets/Resources/VillagerDecisionTree.txt"));
		decDictionary.Add("w01", new DecisionTree("Assets/Resources/VillagerDecisionTreeBlood.txt"));
		decDictionary.Add("w02", new DecisionTree("Assets/Resources/VillagerDecisionTreeNew.txt"));

		foreach (var item in decDictionary.Keys) 
		{
			Debug.Log(item);
		}

		Debug.Log (decDictionary.Keys.ToString());
	}
}
