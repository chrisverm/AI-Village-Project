using UnityEngine;
using System.Collections;

public static class Util  
{
	public static BitArray Byte2BitAra (byte b)
	{
		byte[] bAra = new byte[1];	
		bAra [0] = b;
		return new BitArray (bAra);
	}

	public static byte BitAra2Byte (BitArray bA)
	{
		byte[] bAra = new byte[1];
		bA.CopyTo (bAra, 0);
		return bAra [0];
	}
}