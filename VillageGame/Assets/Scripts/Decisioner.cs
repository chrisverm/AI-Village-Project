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
			}
		}
		
		return currNode;
	}
	
	private static bool CheckDecisions(NPC c, string func, string args)
	{
		bool b = false;
		
		switch(func)
		{
		case "IsCartClose":
			b = IsEntityClose(c, Managers.Game.cart, float.Parse(args));
			//Debug.Log("Is the cart close? " + b);
			return b;
		case "IsMayorClose":
			b = IsEntityClose(c, Managers.Game.mayor, float.Parse(args));
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
			b = IsEntityClose(c, Managers.Game.house, float.Parse(args));
			//Debug.Log("Is the house close? " + b);
			return b;
		default:
			
			return b;
		}
	}
	
	private static bool IsEntityClose(NPC c, Entity e, float maxDist)
	{
		return Vector3.Distance(c.Position, e.Position) < maxDist;
	}
	
	private static bool IsWerewolfClose(NPC c, float maxDist)
	{
		foreach (Werewolf werewolf in Managers.Game.Werewolves) 
		{
			if (Vector3.Distance(c.Position, werewolf.Position) < maxDist)
				return true;
		}
		
		return false;
	}
	
	private static bool IsVillagerClose(NPC c, float maxDist)
	{
		foreach (Villager villager in Managers.Game.Villagers) 
		{
			if (Vector3.Distance(c.Position, villager.Position) < maxDist)
				return true;
		}
		
		return false;
	}
}