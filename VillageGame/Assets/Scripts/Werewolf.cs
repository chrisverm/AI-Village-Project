using UnityEngine;
using System.Collections;

public class Werewolf : NPC
{
	public WerewolfAudio werewolfAudio;

	protected override void Start()
	{
		base.Start();

		node = path.ClosestNode(Position);
		//maxSpeed = 0.6f;
		maxForce = 0.035f;

		rational = true;
	}

	protected override void Update()
	{
		closestEnemy = GetClosestVillager();

		if((pastBehavior != Behavior.SEEK && pastBehavior != Behavior.SEEK_ARRIVAL) && behavior == Behavior.SEEK)
			werewolfAudio.SafePlayGrowl();

		base.Update();
	}

	protected override void Respawn()
	{
		Debug.Log("KILLING WEREWOLF");
	}
}