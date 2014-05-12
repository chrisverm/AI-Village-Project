using UnityEngine;
using System.Collections.Generic;

public class SpawningManager : MonoBehaviour 
{
	[SerializeField] private Transform villagerSpawnLocationsObject;
	[SerializeField] private  Transform werewolfSpawnLocationsObject;
	
	private List<Vector3> villagerSpawnLocations;
	private List<Vector3> werewolfSpawnLocations;

	public int VillagerSpawns
	{ get { return villagerSpawnLocations.Count; } }
	
	public int WerewolfSpawns
	{ get { return werewolfSpawnLocations.Count; } }

	void Awake()
	{
		villagerSpawnLocations = new List<Vector3>();
		werewolfSpawnLocations = new List<Vector3>();

		for (int i = 0; i < villagerSpawnLocationsObject.childCount; i++) 
		{ villagerSpawnLocations.Add(villagerSpawnLocationsObject.GetChild(i).position); }

		for (int i = 0; i < werewolfSpawnLocationsObject.childCount; i++) 
		{ werewolfSpawnLocations.Add(werewolfSpawnLocationsObject.GetChild(i).position); }
	}

	public void SpawnVillager(Villager villager, int index = 0)
	{ 
		villager.transform.position = villagerSpawnLocations[
     		(index == -1) ? Random.Range(0, VillagerSpawns) : index];

		// Reset path incase the villager made a break for it.
		villager.path = Managers.Entity.villagerPaths[0];
		villager.SetGenes(GenePool.PopGene());
	}

	public void SpawnWerewolf(Werewolf wolf, int index = 0)
	{
		wolf.transform.position = werewolfSpawnLocations[
        	(index == -1) ? Random.Range(0, WerewolfSpawns) : index];
		wolf.SetGenes(GenePool.PopGene());
	}
}
