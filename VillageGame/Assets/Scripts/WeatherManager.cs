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
	[SerializeField] private float transitionTime;

	public Color MoonColor { get { return moon; } }
	public Color AuraColor { get { return aura; } }
	public Color LightColor { get { return light; } }
	public float TransitionTime { get { return transitionTime; } }
}

[System.Serializable]
public class ConditionPair
{
	[SerializeField] public Condition element;
	[SerializeField] public ConditionData data;
}

public class WeatherManager : SingletonMonoBehaviour<WeatherManager>
{
	public List<ConditionPair> conditions;
	private static Dictionary<Condition, ConditionData> condDict;
	public static Dictionary<Condition, ConditionData> CondDict { get { return condDict; } }

	private Condition condition;
	public Moon moon;
	private Timer timer;
	private bool dittlySquat;

	public Condition CurrentCondition
	{ get { return condition; } }

	public void Start ()
	{
		InitDictionary();

		moon.Initialize(Condition.FULL_MOON);
		//moon.ChangeState(Condition.BLOOD_MOON);

		timer = new Timer();
		
		timer.AutoReset = false;
		timer.Elapsed += timerElapsed;
		ResetTimer();
	}

	private void ResetTimer()
	{
		timer.Stop();
		timer.Interval = Random.Range(30.0f, 60.0f) * 1000;
		timer.Start();
	}

	public void Update ()
	{
        if (Input.GetKeyUp(KeyCode.B))
        { ChangeCondition(Condition.BLOOD_MOON); }

        if (Input.GetKeyUp(KeyCode.N))
        { ChangeCondition(Condition.NEW_MOON); }

		if (Input.GetKeyUp(KeyCode.F))
		{ ChangeCondition(Condition.FULL_MOON); }

		if (dittlySquat) 
		{
			Condition newCondition = condition;
			if (condition != Condition.FULL_MOON)
			{ newCondition = Condition.FULL_MOON; }
			else
			{
				while (condition == newCondition) 
				{ newCondition = (Condition)Random.Range(0,3); }
			}

            ChangeCondition(newCondition);
			dittlySquat = false;
		}
	}

    private void ChangeCondition(Condition newCondition)
    {
        Debug.Log("Changing to " + newCondition + " from " + condition);
        condition = newCondition;
        moon.ChangeState(newCondition);
        ResetTimer();
    }

	private void InitDictionary()
	{
		condDict = new Dictionary<Condition, ConditionData>(conditions.Count);

		for (int i = 0; i < conditions.Count; i++)
		{
			condDict.Add(conditions[i].element, conditions[i].data);
		}
	}

	private void timerElapsed(object sender, ElapsedEventArgs e)
	{ dittlySquat = true; }
}