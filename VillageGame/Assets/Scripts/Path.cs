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
		get { return nodes[index % nodes.Count]; }
		set { nodes[index % nodes.Count] = value; }
	}


	private Path()
	{
		pathWidth = 10;
		nodeBoundary = 1;
	}
	// Use this for initialization
	public Path(Transform parent) : this()
	{
		nodes = new List<Vector3>();
		
		for (int i = 0; i < parent.childCount; i++) 
		{
			nodes.Add(parent.GetChild(i).position);
		}
	}

	public Path(List<Vector3> path) : this()
	{
		nodes = path;
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
