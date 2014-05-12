using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ObjectIDPair
{
	public string id;
	public GameObject obj;
}

public class EntityManager : MonoBehaviour
{
	public int numberOfVillagers;
	public int numberOfWerewolves;

	public string gibberishFilePath;
	public int maxSentanceLength;

	public Object villagerPrefab;
	public Object werewolfPrefab;

	public List<Path> werewolfPaths;
	public List<Path> villagerPaths;

	public ObjectIDPair[] kvPairs;

	private Dictionary<string, GameObject> mainObjs;
	public List<Werewolf> werewolves;
	public List<Villager> villagers;
	
	public List<Werewolf> Werewolves { get { return werewolves; } }
	public List<Villager> Villagers { get { return villagers; } }
	public Dictionary<string, GameObject> MainObjs { get { return mainObjs; } }

	private Markov markovChain;

	void Awake()
	{
		markovChain = new Markov();
		markovChain.CreateGraph(gibberishFilePath);
	}

	// Use this for initialization
	void Start ()
	{
		mainObjs = new Dictionary<string, GameObject>(kvPairs.Length);
		
		for (int i = 0; i < kvPairs.Length; i++)
			mainObjs.Add(kvPairs[i].id, kvPairs[i].obj);

		villagers = new List<Villager>();
		werewolves = new List<Werewolf>();

		if (numberOfVillagers > Managers.Spawn.VillagerSpawns ||
		    numberOfWerewolves > Managers.Spawn.WerewolfSpawns)
		{ Debug.LogError("Too many villagers/werewolves for the spawn points we have"); }

		for (int i = 0; i < numberOfVillagers; i++) 
		{
			Villager villager = ((GameObject)Instantiate(villagerPrefab)).GetComponent<Villager>();
			Managers.Spawn.SpawnVillager(villager, i);
			
			villager.path = villagerPaths[i % villagerPaths.Count];
			villagers.Add(villager);
		}

		for (int i = 0; i < numberOfWerewolves; i++) 
		{	
			Werewolf wolf = ((GameObject)Instantiate(werewolfPrefab)).GetComponent<Werewolf>();
			Managers.Spawn.SpawnWerewolf(wolf, i);

			wolf.path = werewolfPaths[i % werewolfPaths.Count];
			werewolves.Add(wolf);
		}
	}

	/// <summary>
	/// Returns the next gibberish sentance to use.
	/// </summary>
	/// <returns> The gibberish sentance. </returns>
	public string GetGibberish()
	{
		// Get a sentance.
		string sent = string.Join(" ", markovChain.GenGibSent(maxSentanceLength));

		// Capitalize first letter.
		sent = char.ToUpper(sent[0]) + sent.Substring(1);

		// Trim end of spaces, tabs, and null chars.
		sent = sent.TrimEnd(' ', '\t', '\0');

		// End sentace with a period.
		sent += ".";

		return sent;
	}
}
