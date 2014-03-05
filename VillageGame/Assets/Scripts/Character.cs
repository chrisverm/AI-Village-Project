using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Character : Entity
{
	[SerializeField] protected CharacterController characterControler;

	protected float maxSpeed, maxForce;
	protected Vector3 velocity;

	public Vector3 Velocity { get { return velocity; } }
	public float MaxSpeed { get { return maxSpeed; } }
	public float MaxForce { get { return maxForce; } }

    public Vector3 Up { get { return transform.up; } }
    public Vector3 Right { get { return transform.right; } }
    public Vector3 Forward { get { return transform.forward; } }

    protected override void Update()
    {
        base.Update();

		characterControler.Move(Vector3.down);

		velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);
		characterControler.Move(Velocity);

		transform.LookAt(Position + Velocity.normalized);

    }
}