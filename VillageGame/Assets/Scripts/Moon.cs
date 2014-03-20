using UnityEngine;
using System.Collections;

public class Moon : MonoBehaviour
{
	public Conditions currCondition, prevCondition;
	public ConditionColors currColors, prevColors;
	public GameObject moon;
	public GameObject aura;
	public Light dirLight;
	private float transition;
	private float transitionTime;
	private Color moonColor, auraColor, lightColor;
	
	void Start () { }
	
	public void ChangeState(Conditions newCondition, ConditionColors newColors, float timeForTransition)
	{
		prevCondition = currCondition;
		prevColors = currColors;
		
		currCondition = newCondition;
		currColors = newColors;
		
		transition = 0.0f;
		transitionTime = timeForTransition;
	}
	
	public void SetState(Conditions newCondition, ConditionColors newColors)
	{
		ChangeState(newCondition, newColors, 0);
		
		moonColor = currColors.MOON;
		auraColor = currColors.AURA;
		lightColor = currColors.LIGHT;
		
		moon.renderer.material.color = moonColor;
		aura.renderer.material.color = auraColor;
		dirLight.color = lightColor;
	}
	
	void Update ()
	{
		aura.transform.LookAt(GameManager.Instance.mayor.Position);
		aura.transform.Rotate(new Vector3(90, 0, 0));
		
		if (transition < transitionTime)
		{
			moonColor = Color.Lerp(prevColors.MOON, currColors.MOON, transition / transitionTime);
			auraColor = Color.Lerp(prevColors.AURA, currColors.AURA, transition / transitionTime);
			lightColor = Color.Lerp(prevColors.LIGHT, currColors.LIGHT, transition / transitionTime);
			transition += Time.deltaTime;
			
			moon.renderer.material.color = moonColor;
			aura.renderer.material.color = auraColor;
			dirLight.color = lightColor;
		}
	}
}