using UnityEngine;
using System.Collections;

public class Werewolf : NPC
{
	protected override void Start()
	{
		base.Start();

		node = path.ClosestNode(Position);
		maxSpeed = 0.6f;
		maxForce = 0.035f;

		rational = true;
		decisionTree = new DecisionTree("Assets/Resources/WerewolfDecisionTree.txt");
	}

	protected override void Update()
	{
		closestEnemy = GetClosestVillager();

		if((pastBehavior != Behavior.SEEK && pastBehavior != Behavior.SEEK_ARRIVAL) && behavior == Behavior.SEEK)
			AudioController.PlayMusic("Growl");

		base.Update();
	}

	protected override void Respawn()
	{
		Debug.Log("KILLING WEREWOLF");
	}
}