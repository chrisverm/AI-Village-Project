using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public Object blimpPrefab;

	public UI ui;


	void Start()
	{
		GenePool.Initialize (10);
		GenePool.CreatePopulation ();
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
	
	/// <summary>
	/// Call this at the start of a new round, hides the results screen.
	/// </summary>
	public void StartNewRound()
	{
		ui.HideResults();
	}
	
	/// <summary>
	/// Call this at the end of a round, shows the results screen.
	/// </summary>
	public void RoundEnded()
	{
		string results = "";
		
		int i = 1;
		foreach (Villager villager in Managers.Entity.Villagers)
		{
			results += "Villager : " + i + " Did this...\n";
			
			i++;
		}
		
		results += "\n";
		
		i = 1;
		foreach (Werewolf wolf in Managers.Entity.Werewolves)
		{
			results += "Werewolf : " + i + " Did this...\n";
			
			i++;
		}
		
		ui.ShowResults(results);
	}
	
	/// <summary>
	/// Saves a villager.
	/// Increment villager score.
	/// </summary>
	public void SaveVillager()
	{
		ui.SavedVillagers++;
	}
	
	/// <summary>
	/// Kills a villager.
	/// Increment werewolf score.
	/// </summary>
	public void KillVillager()
	{
		ui.DeadVillagers++;
	}
}