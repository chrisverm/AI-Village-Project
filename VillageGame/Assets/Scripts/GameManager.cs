using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public int numberOfvillagers;
	public int numberOfWerewolves;
	public Object villagerPrefab;
	public Object werewolfPrefab;
	public Object blimpPrefab;

	public UI ui;

	private static GameManager instance;
	public static GameManager Instance { get { return instance; } }

	private List<Werewolf> werewolves;
	private List<Villager> villagers;
	private List<Entity> obstacles;

	public Mayor mayor;
	public Entity house;
	public Entity cart;
	public Transform villagerSpawnLocationsObject;
	public Transform werewolfSpawnLocationsObject;

	public List<Transform> villagerSpawnLocations;
	public List<Transform> werewolfSpawnLocations;
	public List<Werewolf> Werewolves { get { return werewolves; } }
	public List<Villager> Villagers { get { return villagers; } }
	public List<Entity> Obstacles { get { return obstacles; } }

	void Start()
	{
		instance = this;

		werewolves = new List<Werewolf>();
		villagers = new List<Villager>();
		obstacles = new List<Entity>();
		villagerSpawnLocations = new List<Transform>();
		werewolfSpawnLocations = new List<Transform>();

		for (int i = 0; i < villagerSpawnLocationsObject.childCount ; i++) 
		{ villagerSpawnLocations.Add(villagerSpawnLocationsObject.transform.GetChild(i)); }

		for (int i = 0; i < werewolfSpawnLocationsObject.childCount; i++) 
		{ werewolfSpawnLocations.Add(werewolfSpawnLocationsObject.transform.GetChild(i)); }

		for (int i = 0; i < numberOfvillagers; i++) 
		{
			Villager villager = ((GameObject)Instantiate(villagerPrefab)).GetComponent<Villager>();
			villagers.Add(villager);
			villager.transform.position = villagerSpawnLocations[i].position;
		}

		for (int i = 0; i < numberOfWerewolves; i++) 
		{
			Werewolf werewolf = ((GameObject)Instantiate(werewolfPrefab)).GetComponent<Werewolf>();
			werewolves.Add(werewolf);
			werewolf.transform.position = werewolfSpawnLocations[i].position;
		}
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0)) // left mouse button
		{
			Screen.showCursor = false;
			Screen.lockCursor = true;
		}
		
		if (Input.GetMouseButtonDown(1)) // right mouse button
		{
			Screen.showCursor = true;
			Screen.lockCursor = false;
		}
	}
}