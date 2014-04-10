using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ObjectIDPair
{
	public string id;
	public GameObject obj;
}

public class EntityManager : SingletonMonoBehaviour<EntityManager>
{
	[SerializeField] public ObjectIDPair[] kvPairs;
	private Dictionary<string, GameObject> mainObjs;

	public Dictionary<string, GameObject> MainObjs { get { return mainObjs; } }

	// Use this for initialization
	void Start ()
	{
		mainObjs = new Dictionary<string, GameObject>(kvPairs.Length);
		
		for (int i = 0; i < kvPairs.Length; i++)
			mainObjs.Add(kvPairs[i].id, kvPairs[i].obj);
	}
	
	// Update is called once per frame
	void Update () { }
}
