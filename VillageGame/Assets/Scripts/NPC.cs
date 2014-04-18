using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class NPC : Character 
{
	protected bool rational;
	protected Behavior behavior;
	protected Behavior pastBehavior;
	protected object behaviorData;
	protected DecisionTree decisionTree;

	protected NPC closestEnemy;

	public Path path;
	public int node;

	// Use this for initialization
	protected override void Start() 
    {
        base.Start();


	}
	
	// Update is called once per frame
	protected override void Update() 
    {
		pastBehavior = behavior;

		if (rational)
			Decide();

		velocity += Vector3.ClampMagnitude(Steering.Execute(this, behavior, behaviorData), maxForce);

        base.Update();
	}

	protected virtual void Respawn()
	{

	}

	private void Decide()
	{
		Node n = Decisioner.Decide(this, decisionTree);

		behavior = (Behavior)Enum.Parse(typeof(Behavior), n.Func.ToUpper());
		
		switch(n.Args)
		{
		case "Cart":
			behaviorData = Managers.Entity.MainObjs["Cart"].transform.position;
			break;
		case "Mayor":
			behaviorData = Managers.Entity.MainObjs["Mayor"].transform.position;
			break;
		case "Werewolf":
			behaviorData = closestEnemy.Position;
			break;
		case "Villager":
			behaviorData = closestEnemy.Position;
			break;
		case "Path":
			behaviorData = path;
			break;
		case "None":
			behaviorData = Vector3.zero;
			break;
		}
	}
	
	protected Werewolf GetClosestWerewolf()
	{
		Werewolf closest = null;
		float dist = float.MaxValue;
		float curDist = 0;
		
		foreach (Werewolf werewolf in Managers.Entity.Werewolves) 
		{
			curDist = Vector3.Distance(this.Position, werewolf.Position);
			if (curDist < dist)
			{
				closest = werewolf;
				dist = curDist;
			}
		}
		
		return closest;
	}
	
	protected Villager GetClosestVillager()
	{
		Villager closest = null;
		float dist = float.MaxValue;
		float curDist = 0;
		
		foreach (Villager villager in Managers.Entity.Villagers) 
		{
			curDist = Vector3.Distance(this.Position, villager.Position);
			if (curDist < dist)
			{
				closest = villager;
				dist = curDist;
			}
		}
		
		return closest;
	}

	/// <summary>
	/// Play the audio for the specified category
	/// </summary>
	/// <param name="audioCategory">Audio category to be played</param>
	public void Play(string audioCategory)
	{
		AudioController.PlayMusic(audioCategory);
	}
}