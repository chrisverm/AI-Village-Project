using UnityEngine;
using System.Collections;

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
		return genes[popIndex++];
	}

	public static void Breed()
	{

	}

}
