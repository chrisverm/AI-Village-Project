using UnityEngine;
using System.Collections;

public class Moon : MonoBehaviour
{
	private Condition currCondition, prevCondition;
	public GameObject moon, aura;
	public Light dirLight, spotLight;
	private float transition, transitionTime;
	
	public Vector3 startMarker; // starting transform for the moon
	public Vector3 endMarker; // ending transform for the moon
	private float startTime; // time that the transformation began
	private float totalDistance; // distance between transform markers
	private float transformSpeed; // value at which to move the moon
	
	public bool Transitioning { get { return transition < transitionTime; } }

	public void Initialize(Condition initialCondition)
	{
		currCondition = initialCondition;

		moon.renderer.material.color = WeatherManager.CondDict[initialCondition].MoonColor;
		aura.renderer.material.color = WeatherManager.CondDict[initialCondition].AuraColor;
		dirLight.color = WeatherManager.CondDict[initialCondition].LightColor;

		transition = 0.0f;
		transitionTime = 0.0f;
	}

	public void ChangeState(Condition newCondition)
	{
		prevCondition = currCondition;
		currCondition = newCondition;

		transition = 0.0f;
		transitionTime = WeatherManager.CondDict[currCondition].TransitionTime;
	}

	public void Update()
	{
		aura.transform.LookAt(Managers.Entity.MainObjs["Mayor"].transform.position);
		aura.transform.Rotate(new Vector3(90, 0, 0));

		if (Transitioning)
		{
			float t = transition / transitionTime;
			moon.renderer.material.color = Color.Lerp(WeatherManager.CondDict[prevCondition].MoonColor, 
			                                          WeatherManager.CondDict[currCondition].MoonColor, 
			                                          t);
			aura.renderer.material.color = Color.Lerp(WeatherManager.CondDict[prevCondition].AuraColor, 
			                                          WeatherManager.CondDict[currCondition].AuraColor, 
			                                          t);
			dirLight.color = Color.Lerp(WeatherManager.CondDict[prevCondition].LightColor, 
			                            WeatherManager.CondDict[currCondition].LightColor, 
			                            t);

			if (currCondition == Condition.NEW_MOON)
			{
				RenderSettings.fog = true;
				RenderSettings.fogColor = Color.black;
				RenderSettings.fogDensity = Mathf.Lerp(0, 0.02f, t);
			}
			else if (prevCondition == Condition.NEW_MOON)
			{
				RenderSettings.fogDensity = Mathf.Lerp(0.02f, 0, t);
				if (t > 4.9) RenderSettings.fog = false;
			}
			else
			{
				RenderSettings.fog = false;
			}

			transition += Time.deltaTime;
		}
	}
}