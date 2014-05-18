using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class NPC : Character 
{
	const float MUT_PROB = 0.2f;

	protected bool rational;
	protected Behavior behavior;
	protected Behavior pastBehavior;
	protected object behaviorData;
	protected DecisionTree decisionTree;

	private byte speedGene;

	public byte SpeedGene { get { return speedGene; } }

	protected NPC closestEnemy;

	protected float fitness;

	public float Fitness
	{
		get { return fitness; }
	}

	public Path path;
	public int node;

	public string behavDist;

	public NPCText text;

	// Use this for initialization
	protected override void Start() 
    {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update() 
    {
		pastBehavior = behavior;

		if (rational)
			Decide();

		velocity += Vector3.ClampMagnitude(Steering.Execute(this, behavior, behaviorData), maxForce);

        base.Update();
	}

	protected virtual void Respawn()
	{

	}

	public void SetGenes(byte speed)
	{
		//if (speedGene == 0) 
		//{
			speedGene = speed;

			maxSpeed = (GenToPhen(speedGene)/ 200.0f);

			if(this is Villager)
			{
				maxSpeed += 0.1f;
			}
			else if(this is Werewolf)
			{
				maxSpeed += 0.3f;
			}
			else 
			{
				Debug.Log ("Ledel ledel ledel leeuu");
			}
			//Debug.Log (maxSpeed);
		//} 
		//else 
		//{
		//	Debug.Log ("Cannot set speedGene more than once");
		//}
	}

	public void Mutate()
	{
		if (Random.Range(0.0f,1.0f) < MUT_PROB)
		{
			BitArray chromBits = Util.Byte2BitAra(speedGene);
			int mutPt = Random.Range(0,8);
			bool locus = !chromBits.Get(mutPt);
			chromBits.Set(mutPt, locus);
			speedGene = Util.BitAra2Byte(chromBits);

		}
	}

	protected float GenToPhen(byte gene)
	{
		float lb = 0.0f;
		float ub = 200.0f;
		float step = (ub - lb) / 256;
		return (gene * step + lb);
	}

	public void SetTree(char c)
	{
		string newTree = c + "0" + (int)Managers.Weather.CurrentCondition;
		decisionTree = Managers.DecDictionary[newTree];
	}

	private void Decide()
	{
		Node n = Decisioner.Decide(this, decisionTree);

		SetBehavior( (Behavior)System.Enum.Parse(typeof(Behavior), n.Func.ToUpper()) );

		switch(n.Args)
		{
		case "Cart":
			behaviorData = Managers.Entity.MainObjs["Cart"].transform.position;
			break;
		case "Mayor":
			behaviorData = Managers.Entity.MainObjs["Mayor"].transform.position;
			break;
		case "Werewolf":
			behaviorData = closestEnemy.Position;
			break;
		case "Villager":
			behaviorData = closestEnemy.Position;
			break;
		case "Path":
			behaviorData = path;
			break;
		case "None":
			behaviorData = Vector3.zero;
			break;
		}
	}

	/// <summary>
	/// If not already the behavior, changes it to the new behavior and calls OnBehaviorChanged();
	/// </summary>
	/// <param name="newBehavior"> The new behavior.</param>
	protected void SetBehavior(Behavior newBehavior)
	{
		if (newBehavior != behavior)
		{
			behavior = newBehavior;
			OnBehaviorChanged(newBehavior);
		}
	}

	/// <summary>
	/// Called when the behavior changes 
	/// (updates text).
	/// </summary>
	/// <param name="newBehavior"> The new behavior.</param>
	protected virtual void OnBehaviorChanged(Behavior newBehavior)
	{
		text.ClearText();
		text.AddText(newBehavior.ToString().ToLower() + " : " + behavDist);
        text.AddText("My Speed Gene is : " + SpeedGene);
		
	}
	
	protected Werewolf GetClosestWerewolf()
	{
		Werewolf closest = null;
		float dist = float.MaxValue;
		float curDist = 0;
		
		foreach (Werewolf werewolf in Managers.Entity.Werewolves) 
		{
			if (werewolf.gameObject.activeInHierarchy)
			{
				curDist = Vector3.Distance(this.Position, werewolf.Position);
				if (curDist < dist)
				{
					closest = werewolf;
					dist = curDist;
				}
			}
		}
		
		return closest;
	}
	
	protected Villager GetClosestVillager()
	{
		Villager closest = null;
		float dist = float.MaxValue;
		float curDist = 0;
		
		foreach (Villager villager in Managers.Entity.Villagers) 
		{
			if (villager.gameObject.activeInHierarchy)
			{
				curDist = Vector3.Distance(this.Position, villager.Position);
				if (curDist < dist)
				{
					closest = villager;
					dist = curDist;
				}
			}
		}
		
		return closest;
	}

	/// <summary>
	/// Play the audio for the specified category
	/// </summary>
	/// <param name="audioCategory">Audio category to be played</param>
	public void Play(string audioCategory)
	{
		AudioController.PlayMusic(audioCategory);
	}
}