using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public Object blimpPrefab;
	public UI ui;
    public float roundTime;

    private bool timing;
    private float startTime;
    private float t;

    private Vector3 mayorStartPos;

	void Start()
	{
		GenePool.Initialize(10);
		GenePool.CreatePopulation();
        mayorStartPos = Managers.Entity.MainObjs["Mayor"].transform.position;

        StartNewRound();
	}

	void Update()
	{
        if (timing)
        {
            t = (Time.time -  startTime) / roundTime;
            Managers.Weather.MoveMooon(t);

            if (t >= 1) 
                RoundEnded();
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Return))
                StartNewRound();
        }

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

        Managers.Entity.MainObjs["Mayor"].transform.position = mayorStartPos;
        Managers.Entity.CreateNPCs();
        Managers.Weather.RandomCondition();

        startTime = Time.time;
        timing = true;
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
			results += "Villager : " + i + " had this speed gene : " + villager.SpeedGene + System.Environment.NewLine;
			
			i++;
		}
		
		results += "\n";
		
		i = 1;
		foreach (Werewolf wolf in Managers.Entity.Werewolves)
		{
			results += "Werewolf : " + i + " had this speed gene : " + wolf.SpeedGene + System.Environment.NewLine;

			i++;
		}

		results += System.Environment.NewLine + "Press Enter to continue to the next round...";

		ui.ShowResults(results);

        timing = false;
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