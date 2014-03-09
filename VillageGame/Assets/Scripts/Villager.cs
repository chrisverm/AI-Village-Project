using UnityEngine;
using System.Collections;

public class Villager : NPC
{
	public bool goingToCart;

	protected override void Start()
	{
		base.Start();
		maxSpeed = 0.4f;
		maxForce = 0.035f;
		goingToCart = false;

		node = path.ClosestNode(Position);
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
				goingToCart = false;
				if (collider.bounds.Intersects(closest.collider.bounds))
				{
					Respawn();
					GameManager.Instance.KillVillager();
				}
				else
				{
					behavior = Behavior.FLEE;
					behaviorData = closest.Position;
				}
			}
			else
			{
				if (Vector3.Distance(Position, GameManager.Instance.house.Position) < 125)
				{
					behavior = Behavior.FOLLOW_PATH;
					behaviorData = path;
				}
				else
				{
					if (goingToCart)
					{
						behavior = Behavior.FOLLOW_PATH;
						behaviorData = path;
					}
					else if (Random.Range(0.0f, 1.0f) > 0.99f)
					{
						NavMeshPath p = new NavMeshPath();
						NavMesh.CalculatePath(Position, cartPos, -1, p);
						path = new Path(p.corners);
						node = 1; // Zero causes errors.
						goingToCart = true;

						behavior = Behavior.FOLLOW_PATH;
						behaviorData = path;
					}
					else
					{
						behavior = Behavior.WANDER;
						behaviorData = Vector3.zero;
					}
				}
			}
		}

		if (Vector3.Distance(Position, cartPos) < 30)
        {
            behavior = Behavior.SEEK;
            behaviorData = cartPos;
		}

		base.Update();
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject == GameManager.Instance.cart.gameObject)
		{
            Respawn();
            GameManager.Instance.SaveVillager();
		}
	}

	void Respawn()
	{
		// ew.
		Position = GameManager.Instance.villagerSpawnLocations[Random.Range(0,5)].position;
	}

}