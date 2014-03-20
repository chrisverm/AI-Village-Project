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

		base.Update();
	}

	protected override void Respawn()
	{
		Debug.Log("KILLING WEREWOLF");
	}
}