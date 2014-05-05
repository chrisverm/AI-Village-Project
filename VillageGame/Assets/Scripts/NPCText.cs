using UnityEngine;
using System.Collections;

public class NPCText : MonoBehaviour {

	public TextMesh textPrefab;

	// Use this for initialization
	void Start() 
	{
		TextMesh newText = (TextMesh)Instantiate(textPrefab, transform.position, transform.rotation * Quaternion.AngleAxis(180, Vector3.up));
		newText.transform.parent = transform;

		newText.text = "Hello Chris";
	}
	
	// Update is called once per frame
	void Update() 
	{
		if (Camera.current != null)
			transform.LookAt(Camera.current.transform, Vector3.up);
	}
}
