using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Path
{
	private List<Transform> nodes;
	
	// Use this for initialization
	public Path(Transform parent)
	{
		nodes = new List<Transform>();
		
		for (int i = 0; i < parent.childCount; i++) 
		{
			nodes.Add(parent.GetChild(i));
		}
	}
	
	public Vector3 ClosestNode(Vector3 currentPosition)
	{
		Vector3 closest = Vector3.zero;;
		float closestDist = float.MaxValue;
		
		foreach(Transform node in nodes) 
		{
			float dist = Vector3.Distance(node.position, currentPosition);
			if (dist < closestDist )
			{
				closest = node.position;
				closestDist = dist;
			}
		}
		return closest;
	}

	public Vector3 GetNextNode(Vector3 currentNode)
	{
		int index = nodes.FindIndex((e)=> { return e.position == currentNode; } );
		Vector3 node = nodes[(index + 1) % nodes.Count].position;
		return node;
	}
}
