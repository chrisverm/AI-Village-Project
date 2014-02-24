using UnityEngine;
using System.Collections;

public class Villager : NPC
{
	protected override void Start()
	{
		base.Start();
		maxSpeed = 0.4f;
	}

	protected override void Update()
	{
		Vector3 mayorPos = GameManager.Instance.mayor.Position;
		Vector3 cartPos = GameManager.Instance.cart.Position;
		
		if (Vector3.Distance(Position, cartPos) < 20)
		{
			behavior = Behavior.SEEK;
			behaviorData = cartPos;
		}
		else if (Vector3.Distance(Position, mayorPos) < 30)
		{
			behavior = Behavior.SEEK_ARRIVAL;
			behaviorData = mayorPos;
		}
		else
		{
			Werewolf closest = null;
			float dist = float.MaxValue;
			float curDist = 0;

			foreach (Werewolf werewolf in GameManager.Instance.Werewolves) 
			{
				curDist = Vector3.Distance(this.Position, werewolf.Position);
				if (curDist < 80 && curDist < dist)
				{
					closest = werewolf;
					dist = curDist;
				}
			}

			if (closest != null)
			{
				if (collider.bounds.Intersects(closest.collider.bounds))
				{
					Respawn();
				}
				else
				{
					behavior = Behavior.FLEE;
					behaviorData = closest.Position;
				}
			}
			else
			{
				behavior = Behavior.WANDER;
				behaviorData = mayorPos;
			}
		}

		velocity += Steering.Execute(this, behavior, behaviorData);

		if (Vector3.Distance(Position, cartPos) < 10)
		{
			Destroy(gameObject);
		}

		base.Update();
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject == GameManager.Instance.cart.gameObject)
		{
			Respawn();
		}
	}

	void Respawn()
	{
		// ew.
		Position = GameManager.Instance.villagerSpawnLocations[Random.Range(0,5)].position;
	}

}