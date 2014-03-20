using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Path
{
	private float pathWidth;
	private float nodeBoundary;

	public static GameObject debug;
	private List<Vector3> nodes;

	public float PathWidth { get { return pathWidth; } }
	public float NodeBoundary { get { return nodeBoundary; } }
	
	public Vector3 this[int index]
	{
		get { if (index < 0) index = 0; return nodes[index % nodes.Count]; } // what about when nodes = 0?
		set { if (index < 0) index = 0; nodes[index % nodes.Count] = value; } // this could go craaaazzy.
	}

	private Path()
	{
		pathWidth = 10;
		nodeBoundary = 1;
	}
	
	public Path(Transform parent) : this()
	{
		nodes = new List<Vector3>();
		
		for (int i = 0; i < parent.childCount; i++) 
		{
			nodes.Add(parent.GetChild(i).position);
		}

		if (nodes.Count <= 0)
		{
			nodes.Add(Vector3.zero);
			Debug.Log("Transform passed had no children to create a path with");
		}
	}

	public Path(List<Vector3> path) : this()
	{ 
		nodes = path; 

		if (nodes.Count <= 0)
		{
			nodes.Add(Vector3.zero);
			Debug.Log("List or array passed to make a path was empty");
		}
	}

	public Path(Vector3[] path) : this(path.ToList<Vector3>()) {}
	
	public int ClosestNode(Vector3 currentPosition)
	{
		float closestDist = float.MaxValue;
		int closestNode = 0;

		for (int node = 0; node < nodes.Count; node++) 
		{
			float dist = Vector3.Distance(this[node], currentPosition);

			if (dist < closestDist)
			{
				closestNode = node;
				closestDist = dist;
			}
		}

		return closestNode;
	}
}
