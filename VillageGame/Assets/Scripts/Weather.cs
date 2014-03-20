using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

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
	private Timer timer;
	private bool dittlySquat;

	public void Start ()
	{
		InitDictionary();

		moon.Initialize(Condition.FULL_MOON);
		//moon.ChangeState(Condition.BLOOD_MOON);

		timer = new Timer();
		
		timer.AutoReset = false;
		timer.Elapsed += timerElapsed;
		timer.Interval = Random.Range(30.0f, 60.0f) * 1000;
		timer.Start();
	}

	public void Update ()
	{
		if (dittlySquat) 
		{
			Condition newCondition = condition;
			if (condition != Condition.FULL_MOON)
			{
				newCondition = Condition.FULL_MOON;
			}
			else
			{
				while (condition == newCondition) 
				{
					newCondition = (Condition)Random.Range(0,3);
				}
			}
			
			//Debug.Log(newCondition);
			condition = newCondition;
			moon.ChangeState(newCondition);
			dittlySquat = false;
			timer.Interval = Random.Range(30.0f, 60.0f) * 1000;
			timer.Start();
		}
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

	private void timerElapsed(object sender, ElapsedEventArgs e)
	{ dittlySquat = true; }
}