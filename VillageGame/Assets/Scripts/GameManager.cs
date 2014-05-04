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

	public void SaveVillager()
	{
		ui.SavedVillagers++;
	}

	public void KillVillager()
	{
		ui.DeadVillagers++;
	}
}