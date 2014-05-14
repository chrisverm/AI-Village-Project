using UnityEngine;
using System.Collections;

using System;

public static class Decisioner
{
	public delegate void Del();
	
	public static Node Decide(NPC c, DecisionTree t)
	{
		if (t.Root == null)
			return null;
		
		Node currNode = t.Root;
		
		while (!currNode.IsLeaf)
		{
			if (CheckDecisions(c, currNode.Func, currNode.Args))
			{
				currNode = currNode.TruePtr;
			}
			else
			{
				currNode = currNode.FalsePtr;
				c.behavDist = currNode.Args;
			}
		}
		
		return currNode;
	}
	
	private static bool CheckDecisions(NPC c, string func, string args)
	{
		bool b = false;

		//c.dist = float.Parse (args);
		//Debug.Log(Time.time);

		switch(func)
		{
		case "IsCartClose":
			b = IsGameObjectClose(c, Managers.Entity.MainObjs["Cart"], float.Parse(args));
			//Debug.Log("Is the cart close? " + b);
			return b;
		case "IsMayorClose":
			b = IsGameObjectClose(c, Managers.Entity.MainObjs["Mayor"], float.Parse(args));
			//Debug.Log("Is the mayor close? " + b);
			return b;
		case "IsWerewolfClose":
			b = IsWerewolfClose(c, float.Parse(args));
			//Debug.Log("Is a werewolf close? " + b);
			return b;
		case "IsVillagerClose":
			b = IsVillagerClose(c, float.Parse(args));
			//Debug.Log("Is a villager close? " + b);
			return b;
		case "IsHouseClose":
			b = IsGameObjectClose(c, Managers.Entity.MainObjs["House"], float.Parse(args));
			//Debug.Log("Is the house close? " + b);
			return b;
		default:
			
			return b;
		}
	}
	
	private static bool IsGameObjectClose(NPC c, GameObject go, float maxDist)
	{
		return Vector3.Distance(c.Position, go.transform.position) < maxDist;
	}
	
	private static bool IsWerewolfClose(NPC c, float maxDist)
	{
		foreach (Werewolf werewolf in Managers.Entity.Werewolves) 
		{
			if (Vector3.Distance(c.Position, werewolf.Position) < maxDist)
				return true;
		}
		
		return false;
	}
	
	private static bool IsVillagerClose(NPC c, float maxDist)
	{
		foreach (Villager villager in Managers.Entity.Villagers) 
		{
			if (Vector3.Distance(c.Position, villager.Position) < maxDist)
				return true;
		}
		
		return false;
	}
}