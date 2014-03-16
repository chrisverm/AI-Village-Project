using UnityEngine;
using System.Collections;

public enum Conditions
{
	FULL_MOON,
	BLOOD_MOON,
	NEW_MOON,
}

public class Weather : MonoBehaviour
{
	public GameObject moon;
	public GameObject moonAura;
	public Light moonLight;

	Conditions condition;

	// Use this for initialization
	void Start ()
	{
		condition = Conditions.FULL_MOON;
	}

	// Update is called once per frame
	void Update ()
	{
		moonAura.transform.LookAt(GameManager.Instance.mayor.Position);
		moonAura.transform.Rotate(new Vector3(90, 0, 0));

		switch (condition)
		{
		case Conditions.FULL_MOON:
			moon.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			moonAura.transform.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.25f);
			moonLight.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			break;
		case Conditions.BLOOD_MOON:
			moon.renderer.material.color = new Color(1.0f, 0, 0, 0.5f);
			moonAura.transform.renderer.material.color = new Color(1.0f, 0.2f, 0.2f, 1.75f);
			moonLight.color = new Color(1.0f, 0.1f, 0.1f, 1.0f);
			break;
		case Conditions.NEW_MOON:
			moon.renderer.material.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
			moonAura.transform.renderer.material.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
			moonLight.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
			break;
		}
	}
}