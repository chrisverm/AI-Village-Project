using UnityEngine;
using System.Collections;

public enum Result
{
	Killed = 0,
	Survived = 1,
	Saved = 2
}

public class Villager : NPC
{
    public VillagerAudio villagerAudio;
	public Result results;

	protected override void Start()
	{
		base.Start();
		//maxSpeed = 0.4f;
		maxForce = 0.035f;

		node = path.ClosestNode(Position);

		rational = true;
		results = Result.Survived;
	}

	protected override void Update()
	{
		closestEnemy = GetClosestWerewolf();

		base.Update();

        if (behavior == Behavior.FLEE)
            villagerAudio.SafePlayHelp();

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
			KillVillager();
		}

		if (Vector3.Distance(Position, Managers.Entity.MainObjs["Cart"].transform.position) < 10)
		{
			SaveVillager();
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject == Managers.Entity.MainObjs["Cart"])
		{
			SaveVillager();
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
		villagerAudio.SafePlayGibberish();
	}

	private void SaveVillager()
	{
		Debug.Log("Villager Saved");
		Managers.Game.SaveVillager();
		results = Result.Saved;

		Respawn();
	}

	private void KillVillager()
	{
		Debug.Log("Villager Killed");
		Managers.Game.KillVillager();
		villagerAudio.SafePlayDeath();
		results = Result.Killed;

		Respawn();
	}

	protected override void Respawn()
	{
		Debug.Log("Removing Villager");

		if (path.name == "New Path")
			Destroy(path.gameObject);

        villagerAudio.transform.parent = null;
        gameObject.SetActive(false);
	}

    void OnDestroy()
    {
        if (villagerAudio.transform.parent != transform)
        {
            Destroy(villagerAudio);
        }
    }
}
