using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{
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
		position.y = Mathf.Clamp(position.y, 0.1f, target.position.y + distFromTarget);
		transform.position = position;
		
		transform.LookAt(target);
	}
}
