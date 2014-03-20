using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
	public Camera cam;
	private float distFromTarget;
	private float height;
	private Vector2 rotAngles;
	
	public Transform target;

	void Start() 
	{
		float targetHeight = target.collider.bounds.extents.y;
		
		height = targetHeight * 1.5f;
		distFromTarget = targetHeight * 3.5f;

		rotAngles = new Vector2(0, -30 * Mathf.Deg2Rad);

		cam.backgroundColor = Color.black;
	}
	
	void LateUpdate()
	{
		if (target == null)
		{
			Debug.Log("camera target is null");
			return;
		}
		
		rotAngles.x += Input.GetAxis("Mouse X") * Time.deltaTime * 3.5f;
		rotAngles.y += Input.GetAxis("Mouse Y") * Time.deltaTime * 3.5f;

		rotAngles.y = Mathf.Clamp(rotAngles.y, -85 * Mathf.Deg2Rad, 60 * Mathf.Deg2Rad);

		Vector3 position = new Vector3(
			-Mathf.Cos(rotAngles.y) * Mathf.Sin(rotAngles.x),
			-Mathf.Sin(rotAngles.y),
			-Mathf.Cos(rotAngles.y) * Mathf.Cos(rotAngles.x));

		position *= distFromTarget;
		position += target.position;

		Vector3 rayOrigin = new Vector3(position.x, 100, position.z);
		RaycastHit rayHit;
		Physics.Raycast(rayOrigin, Vector3.down, out rayHit);

		position.y = Mathf.Clamp(position.y, rayHit.point.y + 0.5f, target.position.y + distFromTarget);

		transform.position = position;
		transform.LookAt(target);
	}
}
