using UnityEngine;
using System.Collections;

public class Werewolf : NPC
{
	protected override void Start()
	{
		base.Start();

		maxSpeed = 0.6f;
	}

	protected override void Update()
	{
		base.Update();

		Villager closest = null;
		float dist = float.MaxValue;
		float curDist;

		foreach (Villager villager in GameManager.Instance.Villagers) 
		{
			curDist = Vector3.Distance(this.Position, villager.Position);
			if (curDist < 80 && curDist < dist)
			{
				closest = villager;
				dist = curDist;
			}
		}

		if (closest != null)
		{
			behavior = Behavior.SEEK;
			behaviorData = closest.Position;
		}
		else
		{
			behavior = Behavior.WANDER;
			behaviorData = Vector3.zero;
		}

		Vector3 mayorPos = GameManager.Instance.mayor.Position;

		if (Vector3.Distance(mayorPos, Position) < 35)
		{
			behavior = Behavior.FLEE;
			behaviorData = mayorPos;
		}

		velocity += Steering.Execute(this, behavior, behaviorData);
	}
}
