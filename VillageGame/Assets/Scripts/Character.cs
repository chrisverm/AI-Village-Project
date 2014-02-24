using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Character : Entity
{
	public CharacterController characterControler;
	protected float maxSpeed;
	protected Behavior behavior;
	protected Vector3 behaviorData, velocity;

	public Vector3 Velocity { get { return velocity; } }
	public float MaxSpeed { get { return maxSpeed; } }

    public Vector3 Up { get { return transform.up; } }
    public Vector3 Right { get { return transform.right; } }
    public Vector3 Forward { get { return transform.forward; } }

    protected override void Start()
    {
        base.Start();

		characterControler = GetComponent<CharacterController>();
    }

    protected override void Update()
    {
        base.Update();

		characterControler.Move(Vector3.down);

		velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);
		characterControler.Move(Velocity);
		transform.LookAt(Position + Velocity);
    }
}