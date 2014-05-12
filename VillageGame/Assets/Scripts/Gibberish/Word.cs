using System;
using System.Collections.Generic;
using System.Text;

/* Word class implements a Word for the Graph class
 * Contains all the stuff a word needs to work in the Graph
 */
class Word
{
	private string wordSt;		// The word as a string
	private int count = 1;		// The count of this word in corpus
	private int offset;			// Offset of the word in the graph arrays

	public Word(string st, int off)
	{
		wordSt = st;
		offset = off;
		count = 1;
	}

	public string WordSt
	{
		get { return wordSt; }
		set { wordSt = value; }
	}

	public int Offset
	{
		get { return offset; }
		set { offset = value; }
	}

	public int Count
	{
		get { return count; }
		set { count = value; }
	}

	// override ToString
	public override string ToString()
	{
		return wordSt + " - Count: " + count + " Offset: " + offset;
	}
}
