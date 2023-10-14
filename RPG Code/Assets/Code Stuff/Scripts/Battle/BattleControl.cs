using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/* This script controls the flow of the battle. Namely, who's turn it is and whether or not the player party is trying to flee.
This script is attached to the 'BattleController'
*/
public class BattleControl : MonoBehaviour
{
	private GameManager gManager;
	private AudioController aController;
	[SerializeField] private float fadeTime; //How long the fadeout at the end of the battle should last
	[SerializeField] private List<GameObject> turnOrder; //Tracks whose turn it is
	private bool tryRunning = false; //Checks whether hte player is trying to run

	// Start is called before the first frame update
	void Start()
	{
		gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		aController = GameObject.Find("AudioController").GetComponent<AudioController>();
	}
	
	// Update is called once per frame
    void Update()
    {
		//Check if running
		if(tryRunning) //&& timer < 0
		{
			//Decide if run is successful
			StartCoroutine(BattleEnd());
			
			//Otherwise just reset the timer
		}
	}
	
	//!This should probably be an ending battle function. It might need to be placed somewhere else.
	private IEnumerator BattleEnd()
	{
		//Fade out music and play correct music
		yield return StartCoroutine(AudioController.AudioFadeOut.StartFade(aController.LoopSource, fadeTime, 0));
		aController.PlaySong(aController.LoopSource, gManager.nextClip, 1.0f);
		
		//Load previous scene
		SceneManager.LoadScene(gManager.curScene);
		gManager.curState = gManager.nextState;
	}

	//Allows player to try and flee the battle.
	//Called when the 'Run' button is pressed
	public void TryRun(InputAction.CallbackContext context)
	{
		switch (context.phase)
		{
			case InputActionPhase.Started:
				tryRunning = true;
				break;
			case InputActionPhase.Canceled:
				tryRunning = false;
				break;
		}
	}
	
	//Puts an enemy or ally on the turn waiting list
	public void AddToTurnOrder(GameObject combatant)
	{
		//Make sure this object isn't already in the list
		if(!turnOrder.Contains(combatant))
		{
			turnOrder.Add(combatant);
		}
		else{return;}
		
		//This stuff might need to be checked somewhere else and called from here and update
		//If it's a player, set turn if one isn't set already (and play a little ding sound)
		
		//If it's an enemy, see if they can go.
		//If not, wait
		
		//Otherwise let the enemy take thier action
	}
}
