using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Condition
{
	FULL_MOON = 0,
	BLOOD_MOON = 1,
	NEW_MOON = 2,
}

[System.Serializable]
public class ConditionData
{
	[SerializeField] private Color moon;
	[SerializeField] private Color aura;
	[SerializeField] private Color light;
	[SerializeField] private float auraAlpha;
	[SerializeField] private float transitionTime;

	public Color MoonColor { get { return moon; } }
	public Color AuraColor { get { return aura; } }
	public Color LightColor { get { return light; } }
	public float TransitionTime { get { return transitionTime; } }

	public void Initialize()
	{
		aura.a = auraAlpha;
	}
}

[System.Serializable]
public class ConditionPair
{
	[SerializeField] public Condition element;
	[SerializeField] public ConditionData data;
}

public class Weather : MonoBehaviour
{
	public List<ConditionPair> conditions;
	private static Dictionary<Condition, ConditionData> condDict;
	public static Dictionary<Condition, ConditionData> CondDict { get { return condDict; } }

	private Condition condition;
	public Moon moon;

	public void Start ()
	{
		InitDictionary();

		moon.Initialize(Condition.FULL_MOON);
		moon.ChangeState(Condition.BLOOD_MOON);
	}

	public void Update ()
	{

	}

	private void InitDictionary()
	{
		condDict = new Dictionary<Condition, ConditionData>(conditions.Count);

		for (int i = 0; i < conditions.Count; i++)
		{
			conditions[i].data.Initialize();
			condDict.Add(conditions[i].element, conditions[i].data);
		}
	}
}