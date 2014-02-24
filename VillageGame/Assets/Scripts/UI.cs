using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour 
{
    private bool dirty;
    private bool displayIntro;

    public GUIText villagerIndicator;
    public GUIText werewolfIndicator;

    public GUIText title;
    public GUIText explination;
    public GUIText villagers;
    public GUIText werewolves;
    public GUIText removal;

    private int savedVillagers, deadVillagers;
    public int SavedVillagers { get { return savedVillagers; } set { savedVillagers = value; dirty = true; } }
    public int DeadVillagers { get { return deadVillagers; } set { deadVillagers = value; dirty = true; } }
	/*
	// Use this for initialization
	void Start () 
    {

        SavedVillagers = 0;
        DeadVillagers = 0;

        werewolves.color = Color.red;
        villagers.color = Color.green;

        explination.text = "You are Mayor VanHelsing! The town is under attack from Werewolves" + "\n"
                        + "You must lead the villagers to the cart next you!" + "\n" +
                        "The werewolves are trying to eat the villagers! So try to chase off the" + "\n" +
                        "werewolves when you can!";
        villagers.text = "Villagers are green!";
        werewolves.text = "Werewolves are red!";
        removal.text = "Press ENTER to remove these messages!";
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (dirty)
        {
            villagerIndicator.text = "Villagers Saved: " + savedVillagers;
            werewolfIndicator.text = "Villagers Killed: " + deadVillagers;	
        }

        if (displayIntro)
        {
            if (Input.anyKey)
            {
                displayIntro = false;
                explination.text = "";
                villagers.text = "";
                werewolves.text = "";
                removal.text = "";
            }
        }
	}
	*/
}
