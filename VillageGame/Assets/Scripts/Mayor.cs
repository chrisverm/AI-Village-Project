using UnityEngine;
using System.Collections;

public class Mayor : Character
{
	private const float RUN_SPEED = 0.6f;
	private const float WALK_SPEED = 0.4f;

	private float rotation;
	private float rotSensitivity = 200.0f;
	private float friction = 0.9f;
	
	protected override void Start()
	{
		base.Start();
		
		maxSpeed = WALK_SPEED;
		maxForce = 0.035f;
		rotation = 0.0f;
	}
	
	protected override void Update()
	{
		if (Input.GetKey(KeyCode.LeftShift))
			maxSpeed = RUN_SPEED;
		else
			maxSpeed = WALK_SPEED;

		rotation += Input.GetAxis("Mouse X") * Time.deltaTime * rotSensitivity;
		Quaternion curRot = Quaternion.Euler(0.0f, rotation, 0.0f);
		transform.rotation = curRot;
		
		velocity += transform.forward * (Input.GetAxis("Vertical") * MaxSpeed);
		velocity += transform.right * (Input.GetAxis("Horizontal") * MaxSpeed);
		velocity *= friction;

		base.Update();
	}
}