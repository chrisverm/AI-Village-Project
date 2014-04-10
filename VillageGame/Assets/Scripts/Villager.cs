using UnityEngine;
using System.Collections;

public class Villager : NPC
{
	protected override void Start()
	{
		base.Start();
		maxSpeed = 0.4f;
		maxForce = 0.035f;

		node = path.ClosestNode(Position);

		rational = true;
		decisionTree = new DecisionTree("Assets/Resources/VillagerDecisionTree.txt");
	}

	protected override void Update()
	{
		closestEnemy = GetClosestWerewolf();

		base.Update();

		if (rational && Random.Range(0.0f, 1.0f) > 0.99999f)
		{
			rational = false;

			NavMeshPath p = new NavMeshPath();
			NavMesh.CalculatePath(Position, Managers.Entity.MainObjs["Cart"].transform.position, -1, p);
			path = Path.CreatePath(p.corners);
			node = 1;

			behavior = Behavior.FOLLOW_PATH;
			behaviorData = path;
		}

		if (collider.bounds.Intersects(closestEnemy.collider.bounds))
		{
			// werewolf makes killing sound
			closestEnemy.Play("Kill");

			Respawn();
		}

		if (Vector3.Distance(Position, Managers.Entity.MainObjs["Cart"].transform.position) < 10)
		{
			Respawn();
			Managers.Game.SaveVillager();
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject == Managers.Entity.MainObjs["Cart"])
		{
            Respawn();
			Managers.Game.SaveVillager();
		}
	}

	protected override void Respawn()
	{
		Debug.Log("KILLING VILLAGER");
		// ew.
		Position = Managers.Game.villagerSpawnLocations[Random.Range(0,5)].position;
		Managers.Game.KillVillager();
	}
}