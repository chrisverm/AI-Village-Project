using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class NPC : Character 
{
	protected Behavior behavior;
	protected object behaviorData;

	public Path path;
	public int node;

	// Use this for initialization
	protected override void Start() 
    {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update() 
    {
		velocity += Vector3.ClampMagnitude(Steering.Execute(this, behavior, behaviorData), maxForce);

        base.Update();
	}
}