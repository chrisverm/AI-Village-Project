using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class GenePool {

	const double CROSSOVER_PROB = 0.9;
	static int villagerPopulation, werewolfPopulation;
	static List<byte> villagerGenes, werewolfGenes;

	public static void Initialize(int villagerPop, int werewolfPop)
	{
		villagerPopulation = villagerPop;
		werewolfPopulation = werewolfPop;

		Debug.Log(villagerPopulation);
		Debug.Log(werewolfPopulation);

		villagerGenes = new List<byte>(villagerPopulation);
		werewolfGenes = new List<byte>(werewolfPopulation);
	}

	public static void CreatePopulation()
	{
		for (int i = 0; i < villagerPopulation; i++) 
		{
			villagerGenes.Add((byte)Random.Range(0,255));
		}

		for (int i = 0; i < werewolfPopulation; i++) 
		{
			werewolfGenes.Add((byte)Random.Range(0,255));
		}
	}

	public static byte PopVillagerGene()
	{
		byte gene = villagerGenes[0];
		villagerGenes.RemoveAt(0);

		return gene;
	}

	public static byte PopWerewolfGene()
	{
		byte gene = werewolfGenes[0];
		werewolfGenes.RemoveAt(0);
		
		return gene;
	}

	public static void Breed()
	{
		Debug.Log("BREED, MY MINIONS");
		List<Villager> villagers = Managers.Entity.villagers;
		for (int i = 0; i < villagers.Count; i++)
		{
			if (villagers[i].results == Result.Killed)
				villagers.RemoveAt(i--);
		}

		List<Werewolf> werewolves = Managers.Entity.werewolves;

		villagerGenes.Clear();
		werewolfGenes.Clear();

		for (int i = 0; i < villagerPopulation; i++) 
		{
			villagerGenes.Add(BreedNPC(villagers.Cast<NPC>().ToList()));
		}

		for (int i = 0; i < werewolfPopulation; i++)
		{
			werewolfGenes.Add(BreedNPC (werewolves.Cast<NPC>().ToList()));
		}
	}

	private static byte BreedNPC(List<NPC> npcs)
	{
		byte c1 = SelectNPC(npcs).SpeedGene;
		byte c2 = SelectNPC(npcs).SpeedGene;
		
		if (Random.Range (0.0f, 1.0f) < CROSSOVER_PROB) 
		{
			byte kidChrom = CrossOver(c1, c2);
			kidChrom = Mutate(kidChrom);
			return kidChrom;
		}
		else
		{
			return (Random.Range(0.0f,1.0f) < 0.5f ? c1 : c2);
		}
	}

	private static NPC SelectNPC(List<NPC> nps)
	{
		float totFit = 0;
		for (int i = 0; i < nps.Count; i++)
		{
			totFit += nps[i].Fitness;
		}
		
		float roll = Random.Range (0, totFit - 1);
		
		float accum = nps[0].Fitness;
		int iSel = 0;
		
		while (accum <= roll && iSel < nps.Count - 1)
		{
			iSel++;
			accum += nps[iSel].Fitness;
		}
		
		return nps[iSel];
	}

	private static byte BreedVillager(List<Villager> villagers)
	{
		byte c1 = SelectVillager(villagers).SpeedGene;
		byte c2 = SelectVillager(villagers).SpeedGene;

		if (Random.Range (0.0f, 1.0f) < CROSSOVER_PROB) 
		{
			byte kidChrom = CrossOver(c1, c2);
			kidChrom = Mutate(kidChrom);
			return kidChrom;
		}
		else
		{
			return (Random.Range(0.0f,1.0f) < 0.5f ? c1 : c2);
		}
	}

	private static Villager SelectVillager(List<Villager> villagers)
	{
		float totFit = 0;
		for (int i = 0; i < villagers.Count; i++)
		{
			totFit += villagers[i].Fitness;
		}
		
		float roll = Random.Range (0, totFit - 1);
		
		float accum = villagers [0].Fitness;
		int iSel = 0;
		
		while (accum <= roll && iSel < villagers.Count - 1)
		{
			iSel++;
			accum += villagers[iSel].Fitness;
		}
		
		return villagers [iSel];
	}

	private static byte CrossOver(byte p1, byte p2)
	{
		BitArray p1Bits = Util.Byte2BitAra (p1);
		BitArray p2Bits = Util.Byte2BitAra (p2);

		BitArray newBits = new BitArray (8);
		int xOverPt = Random.Range (0, 7);

		for (int i = 0; i < 8; i++)
		{
			if (i < xOverPt)
			{
				newBits.Set (i, p1Bits.Get (i));
			}
			else
			{
				newBits.Set (i, p2Bits.Get (i));
			}
		}
		byte newByte = Util.BitAra2Byte (newBits);
		return newByte;
	}

	private static byte Mutate(byte gene)
	{
		if (Random.Range(0.0f,1.0f) < 0.1)
		{
			BitArray chromBits = Util.Byte2BitAra(gene);
			int mutPt = Random.Range(0,8);
			bool locus = !chromBits.Get(mutPt);
			chromBits.Set(mutPt, locus);
			gene = Util.BitAra2Byte(chromBits);
		}

		return gene;
	}

}
