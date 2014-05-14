using UnityEngine;
using System.Collections;

public class Villager : NPC
{
	protected override void Start()
	{
		base.Start();
		//maxSpeed = 0.4f;
		maxForce = 0.035f;

		node = path.ClosestNode(Position);

		rational = true;
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

			SetBehavior(Behavior.FOLLOW_PATH);
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

	/// <summary>
	/// When behavior is changed.
	/// Adds text above the other text for villager gibberish
	/// </summary>
	protected override void OnBehaviorChanged (Behavior newBehavior)
	{
		base.OnBehaviorChanged(newBehavior);

		text.AddTextAbove(Managers.Entity.GetGibberish());
	}

	protected override void Respawn()
	{
		Debug.Log("KILLING VILLAGER");

		if (path.name == "New Path")
			Destroy(path.gameObject);

		Managers.Spawn.SpawnVillager(this);
		Managers.Game.KillVillager();
	}
}