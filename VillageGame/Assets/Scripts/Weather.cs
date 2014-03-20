using UnityEngine;
using System.Collections;

public enum Conditions
{
	FULL_MOON = 0,
	BLOOD_MOON = 1,
	NEW_MOON = 2,
}

[System.Serializable]
public class ConditionColors
{
	public Color MOON;
	public Color AURA;
	public Color LIGHT;
	public float AuraAlpha;
	
	public void Initialize()
	{
		AURA.a = AuraAlpha;
	}
}

public class Weather : MonoBehaviour
{
	private Conditions condition;
	public ConditionColors fullMoon;
	public ConditionColors bloodMoon;
	public ConditionColors newMoon;
	public Moon moon;
	public float transitionTime;

	// Use this for initialization
	void Start ()
	{
		fullMoon.Initialize();
		bloodMoon.Initialize();
		newMoon.Initialize();
		
		condition = Conditions.FULL_MOON;
		moon.SetState(condition, fullMoon);
		moon.ChangeState(Conditions.NEW_MOON, newMoon, transitionTime);
	}

	// Update is called once per frame
	void Update ()
	{

	}
}