using UnityEngine;

public enum Behavior
{
	NONE = 0,
	SEEK = 1,
	SEEK_ARRIVAL = 2,
	FLEE = 3,
	WANDER = 4,
}

public static class Steering
{

	public static Vector3 Execute(Character c, Behavior behavior, Vector3 data)
	{
		Vector3 desired = new Vector3(0.0f, 0.0f, 0.0f);
		
		switch (behavior)
		{
		case Behavior.SEEK:
			desired = Seek(c, data);
			break;
		case Behavior.SEEK_ARRIVAL:
			desired = SeekArrival(c, data);
			break;
		case Behavior.FLEE:
			desired = Flee(c, data);
			break;
		case Behavior.WANDER:
			desired = Wander(c, data);
			break;
		}
		
		return desired;
	}
	
	private static Vector3 Seek(Character c, Vector3 goal)
	{
		Vector3 desired = goal - c.Position;
		desired.y = 0;
		desired = desired.normalized * c.MaxSpeed;
		return desired - c.Velocity;
	}
	
	private static Vector3 SeekArrival(Character c, Vector3 goal)
	{
		Vector3 desired = goal - c.Position;
		desired.y = 0;
		desired = desired.normalized * (desired.magnitude - 4.0f);
		
		float speed = c.MaxSpeed;
		if (desired.magnitude < c.MaxSpeed * 50)				// 10 IS MINIMUM DIST NEEDED BETWEEN CHARACTERS (?)
			speed *= desired.magnitude / (c.MaxSpeed * 50);		// CHANGE 10 (?)
		desired = desired.normalized * speed;
		
		return desired - c.Velocity;
	}
	
	private static Vector3 Flee(Character c, Vector3 goal)
	{
		Vector3 desired = goal - c.Position;
		desired.y = 0;
		desired = desired.normalized * c.MaxSpeed;
		return c.Velocity - desired;
	}
	
	private static Vector3 Wander(Character c, Vector3 goal)
	{
		Vector3 desired = c.Forward;

		desired += (c.Right * Random.Range(-Time.deltaTime * 25,Time.deltaTime * 25));

		return desired + c.Velocity;
	}
}