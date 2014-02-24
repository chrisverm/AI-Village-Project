using UnityEngine;
using System.Collections;

public class Mayor : Character
{
	private float rotation;
	private float rotSensitivity = 200.0f;
	private float friction = 0.9f;
	
	protected override void Start()
	{
		base.Start();
		
		maxSpeed = 0.5f;
		rotation = 0.0f;
	}
	
	protected override void Update()
	{
		base.Update();
		
		rotation += Input.GetAxis("Mouse X") * Time.deltaTime * rotSensitivity;
		Quaternion curRot = Quaternion.Euler(0.0f, rotation, 0.0f);
		transform.rotation = curRot;
		
		velocity += transform.forward * (Input.GetAxis("Vertical") * MaxSpeed);
		velocity += transform.right * (Input.GetAxis("Horizontal") * MaxSpeed);
		velocity *= friction;
	}
}