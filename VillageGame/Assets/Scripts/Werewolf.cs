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
		fitness = Managers.Game.roundTime;

		rational = true;
	}

	protected override void Update()
	{
		closestEnemy = GetClosestVillager();

		if((pastBehavior != Behavior.SEEK && pastBehavior != Behavior.SEEK_ARRIVAL) && behavior == Behavior.SEEK)
			werewolfAudio.SafePlayGrowl();

		if (!Managers.Game.RoundOver)
		{
			fitness -= 1 * Time.deltaTime;
		}

		base.Update();
	}

	protected override void Respawn()
	{
		Debug.Log("KILLING WEREWOLF");
	}

	public void KilledVillager()
	{
		fitness += Random.Range(8.0f, 12.0f);
	}
}