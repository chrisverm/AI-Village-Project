using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GenePool {

	const double CROSSOVER_PROB = 0.9;
	static int population;
	static int popIndex;
	static byte[] genes;


	public static void Initialize(int pop)
	{
		population = pop;
		popIndex = 0;
	}

	public static void CreatePopulation()
	{
		genes = new byte[population];
		for (int i = 0; i < population; i++) 
		{
			genes[i] = (byte)Random.Range(0,255);
		}
	}

	public static byte PopGene()
	{
		return genes[popIndex++ % genes.Length];
	}
	
	public static Villager BreedVillager()
	{

		Villager p1 = null;
		Villager p2 = null;

		byte c1 = 0;//null;
		byte c2 = 0;//null;

		if (Random.Range (0.0f, 1.0f) < CROSSOVER_PROB) 
		{
			byte kidChrom = CrossOver(c1, c2);
			Villager newVillager = new Villager();
			newVillager.SetGenes(kidChrom);
			newVillager.Mutate();
			return newVillager;
		}
		else
		{
			return (Random.Range(0.0f,1.0f) < 0.5f ? p1 : p2);
		}

	}

	private static byte CrossOver(byte p1, byte p2)
	{
		BitArray p1Bits = Util.Byte2BitAra (p1);
		BitArray p2Bits = Util.Byte2BitAra (p2);

		BitArray newBits = new BitArray (8);
		int xOverPt = Random.Range (0, 7);

		for (int i = 0; i <8; i++)
		{
			if(i < xOverPt)
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

}
