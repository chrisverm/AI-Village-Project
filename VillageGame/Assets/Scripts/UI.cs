using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour 
{
	public GUIText villagerIndicator;
	public GUIText werewolfIndicator;
	
	public GUIText title;
	
	public float progressWin = 0;
	public float progressLose = 0;
	public Vector2 pos;
	public Vector2 pos2;
	public Vector2 size;
	
	public Texture2D progressBarFullWin;
	public Texture2D progressBarFullLose;
	
	public Texture2D progressBarWinNub;
	public Texture2D progressBarLoseNub;
	
	private int savedVillagers, deadVillagers;
	public int SavedVillagers { get { return savedVillagers; } set { savedVillagers = value; } }
	public int DeadVillagers { get { return deadVillagers; } set { deadVillagers = value; } }
	
	// Use this for initialization
	void Start () 
	{
		SavedVillagers = 0;
		DeadVillagers = 0;
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(pos.x, pos.y, size.x * Mathf.Clamp01(progressWin), 32), progressBarFullWin);
		GUI.DrawTexture(new Rect(pos.x + (size.x * Mathf.Clamp01(progressWin)), pos.y, 16, 32), progressBarWinNub);
		
		GUI.DrawTexture(new Rect(pos2.x, pos2.y, size.x * Mathf.Clamp01(progressLose), 32), progressBarFullLose);
		GUI.DrawTexture(new Rect(pos2.x + (size.x * Mathf.Clamp01(progressLose)), pos2.y, 16, 32), progressBarLoseNub);
	}
	
	// Update is called once per frame
	void Update () 
	{
		progressWin = SavedVillagers / 20.0f;
		progressLose = DeadVillagers / 20.0f;
	}
}