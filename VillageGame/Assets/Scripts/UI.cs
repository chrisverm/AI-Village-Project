using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour 
{
    public GUITexture winBar;
    public GUITexture winNub;

    public GUITexture loseBar;
    public GUITexture loseNub;
	
	public GUIText results;
	
	public float progressWin = 0;
	public float progressLose = 0;
	
	private int savedVillagers, deadVillagers;
    public int SavedVillagers { get { return savedVillagers; } set { savedVillagers = value; boop(); } }
    public int DeadVillagers { get { return deadVillagers; } set { deadVillagers = value; boop();  } }
	
	// Use this for initialization
	void Start () 
	{
		results.enabled = false;
		
		SavedVillagers = 0;
		DeadVillagers = 0;
	}

    void boop()
    {
        Rect temp;

        temp = winBar.pixelInset;
        temp.width = (savedVillagers/ progressWin) * 80;
        winBar.pixelInset = temp;

        temp = winNub.pixelInset;
        temp.x = 50+(savedVillagers / progressWin) * 80;
        winNub.pixelInset = temp;

        temp = loseBar.pixelInset;
        temp.width = (deadVillagers / progressLose) * 80;
        loseBar.pixelInset = temp;

        temp = loseNub.pixelInset;
        temp.x = 50+(deadVillagers / progressLose) * 80 - 1;
        loseNub.pixelInset = temp;
    }
	
	/// <summary>
	/// Shows the results screen with the given text.
	/// </summary>
	/// <param name='text'>
	/// Results text to show.
	/// </param>
	public void ShowResults(string text)
	{
		results.text = text;
		results.enabled = true;
	}
	
	/// <summary>
	/// Hides the results screen.
	/// </summary>
	public void HideResults()
	{
		results.enabled = false;
	}
}