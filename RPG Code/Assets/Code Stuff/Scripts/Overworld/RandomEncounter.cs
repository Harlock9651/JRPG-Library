using System.Collections;
using System.Collections.Generic;
using sysRand = System.Random;
using UnityEngine;
using UnityEngine.SceneManagement;

/* This script determines when a random encounter occurs, and which possible encounter configuration is selected. When a random encounter happens, the battle scene is called and the encounter information is sent.
This script is placed on tile types where random enounters can occur.
*/
public class RandomEncounter : MonoBehaviour
{
	private AudioController aController;
	
	[SerializeField] private int battleScene = 1; //Whichever unity scene the battle takes place in
	[SerializeField] private GameManager.StateType pBtlState; //The game state the game should be in once the battle is over
	[SerializeField] private AudioClip pBtlSong; //The audio to play as the battle loads
	
	[SerializeField] private int eRate = 3;//How much to increase the rate checker by. Every 256 is a percentage per step. 192 is what is used on the FFVI overworld, so 3 is set here (3 per frame at 60 fps ~192).
	
	[SerializeField] private SceneTransition sTrans; //Which scene transition to play when loading the battle (!TS I'm debating if this should be based on encounter or area... Area *probably* makes the most sense?)
	
	//Encounter stuff
	[System.Serializable]
	private struct Encounter
	{
		public EncounterObject encObj;
		public double eAccWeight;
	}
	[SerializeField] private Encounter[] encounters;
	private List<Encounter> entries = new List<Encounter>();
	private double accWeight;
	private sysRand rand = new sysRand();
	
	//Convert the provided encounter information to an Encounter variable to be used by the script
	public void AddEntry(EncounterObject item, double weight)
	{
		accWeight += weight;
		entries.Add(new Encounter { encObj = item, eAccWeight = accWeight });
	}
	
	//Get the actual encounter to set up the battle with
	public EncounterObject GetRandomEncounter() 
	{
        double r = rand.NextDouble() * accWeight;

        foreach (Encounter entry in entries) 
		{
            if (entry.eAccWeight >= r)
			{return entry.encObj;}
        }
        return default(EncounterObject); //should only happen when there are no entries
    }
	
	//Start is called before the first frame update
	void Start()
	{
		aController = GameObject.Find("AudioController").GetComponent<AudioController>();
		
		//Fill encounter list
		foreach(Encounter encounter in encounters)
		{AddEntry(encounter.encObj, encounter.eAccWeight);}
	}
	
	private void OnTriggerStay2D(Collider2D other)
    {
		//Just make extra sure the audio stuff is set
		if(aController == null)
		{
			aController = GameObject.Find("AudioController").GetComponent<AudioController>();
		}
		
		EncounterObject newEnc;
		//Make sure collider is player first and is walking
		if(other.tag == "Player" && other.GetComponent<Animator>().GetBool("Moving"))
		{
			//Increment encounter counter
			GameManager.Instance.IncREncCount(eRate);
			//Divide counter value by 256 and compare with a random number
			int tmp1 = GameManager.Instance.RencCounter / 256;
			int tmp2 = Random.Range(0, 255); //Random number between 0 and 255
			
			//When a Random Encounter happens
			if(tmp2 < tmp1)
			{
				//Stop player movement
				GameManager.Instance.SetCanMove(false);
				
				newEnc = GetRandomEncounter();
				//Make sure there is an encounter, otherwise just stop
				if(newEnc == null)
				{	
					GameManager.Instance.SetCanMove(true);
					return;
				}
				
				//!TS Real talk? All this should be it's own function so I can tie it with cutscenes
				//Set music
				aController.IntroToLoop(newEnc.scMusic, newEnc.bMusic); //Start battle music
				GameManager.Instance.nextClip = pBtlSong;//What track to play when the battle is over
				
				//Save location data
				GameManager.Instance.curScene = SceneManager.GetActiveScene().buildIndex;
				GameManager.Instance.curSpot = other.transform.localPosition;
				GameManager.Instance.facing = SetFacing(other.GetComponent<SpriteRenderer>().sprite.name);
				
				//Update game manager
				GameManager.Instance.curEnc = newEnc;
				GameManager.Instance.nextState = pBtlState;
				
				//Swap to battle scene (This should be in its own function so events could call it)
				GameManager.Instance.curState = GameManager.StateType.BATTLE;
				StartCoroutine(sTrans.LoadLevel(battleScene));//SceneManager.LoadScene(battleScene);//!Make sure this calls the scene transition
			}
		}
    }
	
	//Converts the current sprite name to values that can be used by the GameManager and other scripts
	private int SetFacing(string sprName)
	{
		switch(sprName)
		{
			case string a when a.Contains("Up"): return 0;
			case string a when a.Contains("Down"): return 1;
			case string a when a.Contains("Left"): return 2;
			case string a when a.Contains("Right"): return 3;
		}
		return 1;
	}
}
