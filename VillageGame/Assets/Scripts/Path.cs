using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Path : MonoBehaviour
{
	[SerializeField] private List<Transform> nodes;
	[SerializeField] private float pathWidth = 10;
	[SerializeField] private float nodeRadius = 1;

	public float PathWidth { get { return pathWidth; } }
	public float NodeRadius { get { return nodeRadius; } }
	public int NumNodes { get { return nodes.Count; } }

	public Vector3 this[int index]
	{
		get { if (index < 0) index = 0; return nodes[index % nodes.Count].position; } // what about when nodes = 0?
		set { if (index < 0) index = 0; nodes[index % nodes.Count].position = value; } // this could go craaaazzy.
	}

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

	public static Path CreatePath(Transform transforms, float pathWidth = 10.0f, float nodeRadius = 1.0f)
	{
		GameObject obj = new GameObject("New Path", typeof(Path));
		obj.transform.parent = GameObject.Find("Paths").transform;
		
		Path p = obj.GetComponent<Path>();
		p.pathWidth = pathWidth;
		p.nodeRadius = nodeRadius;

		p.nodes = new List<Transform> (transforms.childCount);
		for (int i = 0; i < transforms.childCount; i++) 
		{
			p.nodes.Add(transforms.GetChild(i));
		}

		if (p.nodes.Count <= 0)
		{
			p.nodes.Add(new GameObject().transform);
			p.nodes[0].position = Vector3.zero;
			Debug.Log("Transform passed had no children to create a path with");
		}

		return p;
	}

	public static Path CreatePath(Vector3[] array, float pathWidth = 10.0f, float nodeRadius = 4.0f)
	{
		GameObject obj = new GameObject("New Path", typeof(Path));
		obj.transform.parent = GameObject.Find("Paths").transform;

		Path p = obj.GetComponent<Path>();
		p.pathWidth = pathWidth;
		p.nodeRadius = nodeRadius;

		p.nodes = new List<Transform>(array.Length);
		for (int i = 0; i < array.Length; i++)
		{
			Transform newTransform = new GameObject("Node " + i).transform;
			newTransform.position = array[i];
			newTransform.parent = obj.transform;

			p.nodes.Add(newTransform);
		}
		
		if (p.nodes.Count <= 0)
		{
			p.nodes.Add(new GameObject().transform);
			p.nodes[0].position = Vector3.zero;
			Debug.Log("List or array passed to make a path was empty");
		}

		return p;
	}
}