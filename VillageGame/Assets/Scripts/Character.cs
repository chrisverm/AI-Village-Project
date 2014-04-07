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

		//if (this is Mayor)
			//StabalizeOnTerrain();

		transform.LookAt(Position + Velocity.normalized, Up);
    }

	protected void StabalizeOnTerrain()
	{
		/*
		float halfWidth = transform.renderer.bounds.size.x / 2.0f;
		RaycastHit bl, br, fl, fr;

		Vector3 backLeft = Position;
		backLeft.x -= halfWidth;
		backLeft.z -= halfWidth;
		Vector3 backRight = Position;
		backRight.x += halfWidth;
		backRight.z -= halfWidth;
		Vector3 frontLeft = Position;
		frontLeft.x -= halfWidth;
		frontLeft.z += halfWidth;
		Vector3 frontRight = Position;
		frontRight.x += halfWidth;
		frontRight.z += halfWidth;


		Physics.Raycast(backLeft + Vector3.up, Vector3.down, out bl);
		Physics.Raycast(backRight + Vector3.up, Vector3.down, out br);
		Physics.Raycast(frontLeft + Vector3.up, Vector3.down, out fl);
		Physics.Raycast(frontRight + Vector3.up, Vector3.down, out fr);

		transform.up = (Vector3.Cross(br.point - Vector3.up, bl.point - Vector3.up) + 
		                Vector3.Cross(bl.point - Vector3.up, fl.point - Vector3.up) + 
		                Vector3.Cross(fl.point - Vector3.up, fr.point - Vector3.up) + 
		                Vector3.Cross(fr.point - Vector3.up, br.point - Vector3.up)
		                ).normalized;
		*/
		/*
		Vector3 rayOrigin = Position;
		RaycastHit rayHit;
	
		if (Physics.Raycast(Position + Vector3.up, Vector3.down, out rayHit))
		{
			transform.up = rayHit.normal;
		}
		*/
		//Debug.Log(Physics.Raycast(rayOrigin, -Up, out rayHit, 10, 3));//1 << 8));
		//Debug.Log(rayHit.normal);
		//upper= rayHit.normal;
	}
}