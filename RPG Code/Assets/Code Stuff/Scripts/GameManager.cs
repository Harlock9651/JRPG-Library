using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* This script defines the GameManager and defines functions/variables that are imparitive to the proper functioning of the game.
*/
public class GameManager : MonoBehaviour 
{
	//Assures that the SAME Game Manager exists on every scene
	private static GameManager instance;     
    public static GameManager Instance {
        get 
		{
            if(instance==null) 
			{Debug.LogError("Game Manager is Null");}
 
            return instance;
        }
    }
	private void Awake()
	{
		if (!instance)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			//Duplicate GameManager created every time the scene is loaded
			Destroy(gameObject);
		}
	}
	
	
	//Enums
	public enum Status{};
	public enum Element{};
	
	//Scene management
	public int curScene;
	public Vector3 curSpot;
	public int facing;
	public StateType nextState;
	public AudioClip nextClip;
	
	//Party Stuff
	[SerializeField] private int maxPartySize;
	public PartyMemberObject[] curParty;
	public PartyMemberObject[] wholeParty;
	public int leadIndex;
	
	public GameObject player;
	
	[SerializeField] private GlobalBool canMove;
	
	public StateType curState;
	
	//Random Encounter stuff
	public EncounterObject curEnc = null; //What encounter is loaded
	[SerializeField] private int rEncCounter;
	public int RencCounter => rEncCounter;
	public float rEncMod = 1; //Modifies changes to the random encounter counter
	
	//Battle Stuff
	public int btlSpd = 10;
	
	//Game states
	public enum StateType
	{
		DEFAULT,
		OVERWORLD,
		BATTLE,
		EVENT,
		SHOPPING,
		MENU,
		GAMEOVER
	}
	
	//Sets the current lead character to the previous active party member
	public void PrevLeader()
	{
		if(curState == StateType.OVERWORLD)
		{
			if(leadIndex == curParty.Length-1)
			{leadIndex = 0;}
			else{leadIndex+=1;}
			ChangeLeader();
		}
	}
	
	//Sets the current lead character to the next active party member
	public void NextLeader()
	{
		if(curState == StateType.OVERWORLD)
		{
			if(leadIndex == 0)
			{leadIndex = curParty.Length-1;}
			else{leadIndex-=1;}
			ChangeLeader();
		}
	}
	
	//Sets the party object animator contoller to that of the proper lead character 
	public void ChangeLeader()
	{
		if(player != null)
			player.GetComponent<PlayerController>().SetLeader(curParty[leadIndex].AnimCont);
	}
	
	//Start is called before the first frame update
	void Start()
	{
		//!TSSet framerate (should probaby set based on scene type)
		Application.targetFrameRate = 60;
		
		switch(curState)
		{
			case StateType.OVERWORLD:
				ChangeLeader();
				break;
		}
	}
	
	//Creates a simple countdown counter
	public static class Timers
	{
		public static IEnumerator EncounterRefresh(float cDown)
		{
			float coolDownTime = Time.time + cDown;
			
			while(Time.time < cDown)
				yield return null;
		}
	}
	
	//Allows the player to move
	public void SetCanMove(bool moveVal)
	{
		canMove.value = moveVal;
	}
	
	//Increases the random encounter counter by a set amount
	public void IncREncCount(int inc)
	{
		rEncCounter += (int)(inc*rEncMod);
	}
	
	//Resets the current random encounter rate count to zero
	public void REncCountReset()
	{
		rEncCounter = 0;
	}
}
