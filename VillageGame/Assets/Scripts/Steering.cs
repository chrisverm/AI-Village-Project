using UnityEngine;

public enum Behavior
{
	NONE = 0,
	SEEK = 1,
	SEEK_ARRIVAL = 2,
	FLEE = 3,
	WANDER = 4,
	FOLLOW_PATH = 5,
}

public static class Steering
{

	public static Vector3 Execute(NPC c, Behavior behavior, object data)
	{
		Vector3 desired = new Vector3(0.0f, 0.0f, 0.0f);
		
		switch (behavior)
		{
		case Behavior.SEEK:
			desired = Seek(c, (Vector3)data);
			break;
		case Behavior.SEEK_ARRIVAL:
			desired = SeekArrival(c, (Vector3)data);
			break;
		case Behavior.FLEE:
			desired = Flee(c, (Vector3)data);
			break;
		case Behavior.WANDER:
			desired = Wander(c, (Vector3)data);
			break;
		case Behavior.FOLLOW_PATH:
			desired = FollowPath(c, (Path)data);
			break;
		}
		
		return desired;
	}
	
	private static Vector3 Seek(NPC c, Vector3 goal)
	{
		Vector3 desired = goal - c.Position;
		desired.y = 0;
		desired = desired.normalized * c.MaxSpeed;
		return desired - c.Velocity;
	}
	
	private static Vector3 SeekArrival(NPC c, Vector3 goal)
	{
		Vector3 desired = goal - c.Position;
		desired.y = 0;
		desired = desired.normalized * (desired.magnitude - 4.0f);
		
		float speed = c.MaxSpeed;
		if (desired.magnitude < c.MaxSpeed * 50)
			speed *= desired.magnitude / (c.MaxSpeed * 50);
		desired = desired.normalized * speed;
		
		return desired - c.Velocity;
	}
	
	private static Vector3 Flee(NPC c, Vector3 goal)
	{
		Vector3 desired = goal - c.Position;
		desired.y = 0;
		desired = desired.normalized * c.MaxSpeed;
		return c.Velocity - desired;
	}
	
	private static Vector3 Wander(NPC c, Vector3 goal)
	{
		Vector3 desired = c.Forward;

		desired += (c.Right * Random.Range(-Time.deltaTime * 25,Time.deltaTime * 25));

		return desired + c.Velocity;
	}

	private static Vector3 FollowPath(NPC c, Path p)
	{
		Vector2 characterPos = new Vector2(c.Position.x, c.Position.z);
		Vector2 characterVel = new Vector2(c.Velocity.x, c.Velocity.z);
		Vector2 predictPos = characterPos + characterVel;

		Vector2 prevNode = new Vector2(p[c.node - 1].x, p[c.node - 1].z);
		Vector2 nextNode = new Vector2(p[c.node].x, p[c.node].z);

		Vector2 prevToPredict = predictPos - prevNode;
		Vector2 prevToNext = nextNode - prevNode;
		prevToNext.Normalize();
		prevToNext *= Vector2.Dot(prevToPredict, prevToNext);
		Vector2 normalPoint = prevNode + prevToNext;

		if (Vector2.Distance(characterPos, normalPoint) > p.PathWidth)
		{
			return Seek(c, new Vector3(normalPoint.x, 0, normalPoint.y));
			//return new Vector3(normalPoint.x, 0, normalPoint.y);
		}
		else if (Vector2.Distance(characterPos, nextNode) < p.NodeRadius)
		{
			c.node++;
			return FollowPath(c, p);
		}
		else
		{
			return Seek(c, new Vector3(nextNode.x, 0, nextNode.y));
		}

		// to debug (need to get the debug prefab from GM).
		/*
	`	GameObject debugger;
		debugger = (GameObject)MonoBehaviour.Instantiate(debug, new Vector3(prevNode.x, 0, prevNode.y), Quaternion.identity);
		debugger.renderer.material.color = Color.red;
		debugger.name = "PrevNode";
		MonoBehaviour.Destroy(debugger, 1);
		
		debugger = (GameObject)MonoBehaviour.Instantiate(debug, new Vector3(nextNode.x, 0, nextNode.y), Quaternion.identity);
		debugger.renderer.material.color = Color.blue;
		debugger.name = "NextNode";
		MonoBehaviour.Destroy(debugger, 1);
		
		debugger = (GameObject)MonoBehaviour.Instantiate(debug, new Vector3(normalPoint.x, 0, normalPoint.y), Quaternion.identity);
		debugger.renderer.material.color = Color.yellow;
		debugger.name = "Closest";
		MonoBehaviour.Destroy(debugger, 1);
		*/
	}
}