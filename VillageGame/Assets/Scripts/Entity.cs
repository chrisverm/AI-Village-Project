using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    public Dimensions dimensions;

    public Vector3 Position { get { return transform.position; } protected set { transform.position = value; } }

    // Use this for initialization
    protected virtual void Start()
    {
		dimensions = new Dimensions(gameObject.GetComponent<MeshFilter>().mesh.bounds.extents);
	}

    protected virtual void Update() { }
}

/// <summary>
/// The dimensions of a game object.
/// Represented in terms of the radius in the xz plane and the height.
/// </summary>
public class Dimensions
{
	public float Height { get; private set; }
    public float Radius { get; private set; }

	public float Width { get { return Radius * 2.0f; } }

    /// <summary>
    /// Calculates the dimensions of the game object and stores it.
    /// Calculates using the Mesh of the object.
    /// </summary>
    /// <param name="obj"></param>
	public Dimensions(Vector3 extents)
	{
		Height = extents.y;
		
		extents.y = 0;
		Radius = extents.magnitude;
	}
}