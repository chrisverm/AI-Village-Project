using System;
using System.IO;

using UnityEngine;

public class Node
{
	private string func;
	private string args;
	
	public Node TruePtr;
	public Node FalsePtr;
	
	public string Func { get { return func; } }
	public string Args { get { return args; } }
	public bool IsLeaf { get { return FalsePtr == null; } } // arbitrary choice, could be FalsePtr or TruePtr
	
	public Node(string func, string args)
	{
		this.func = func;
		this.args = args;
		
		TruePtr = null;
		FalsePtr = null;
	}
	
	public static implicit operator string(Node node)
	{
		return "node [ func: " + node.func + ", args: " + node.args + " ]";
	}
}

public class DecisionTree
{
	private Node root;
	
	public Node Root { get { return root; } }
	
	public DecisionTree(string filepath)
	{
		root = null;
		
		ReadTreeFromFile(filepath);
	}
	
	private void ReadTreeFromFile(string filepath)
	{
		StreamReader inStream;
		try
		{
			inStream = new StreamReader(filepath);
			root = RecursiveReadTree(inStream);
			inStream.Close();
		}
		catch (FileNotFoundException ex)
		{
			Debug.Log("Where's your file, yo? Take this instead: " + ex);
			return;
		}
	}
	
	private Node RecursiveReadTree(StreamReader inStream)
	{
		try
		{
			string fullLine = inStream.ReadLine();
			string nodeType = fullLine.Substring(0, 1);
			string restLine = fullLine.Substring(1, fullLine.Length - 1);
			string[] split = restLine.Split('|');
			
			Node node = new Node(split[0], split[1]);
			
			if (nodeType == "I")
			{
				node.TruePtr = RecursiveReadTree(inStream);
				node.FalsePtr = RecursiveReadTree(inStream);
			}
			
			return node;
		}
		catch (NullReferenceException ex)
		{
			Console.WriteLine(ex);
			return null;
		}
	}
}