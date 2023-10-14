using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script allows an object to call the Scene Transition Script to transport the player somewhere else.
   The 'sTrans' variable links to the LevelLoader object that teleports the player. The LevelLoader object determines what animation is played when the player is transported.
   The 'isLocal' boolean determines whether the player is transported within the scene (true) or to a different scene (false).
   When teleporting to a different scene, the 'newScene' int is the index of the scene that is to be switched to. This value can be seen in the 'Scenes In Build' list in the Build Settings.
   When teleporting within the same scene, the 'newSpot' Transform is where the player will be transported to.
   The 'stayCount' float determines how long the player must stay in the trigger before they are transported.
   The 'canUse' bool determines whether or not the transition trigger is useable at a given time.
*/

public class TransitionTrigger : MonoBehaviour
{
	[SerializeField] private SceneTransition sTrans; //The LevelLoader object that teleports the player and determines what transition animation is played.
	[SerializeField] private bool isLocal; //Determines whether the player is transported within the scene (true) or to a different scene (false).
	[SerializeField] private int newScene; //The index of the scene that is to be switched to when teleporting to a different scene.
	[SerializeField] private Vector3 newSpotLd; //The point the player will be spawned at when the level loads
	[SerializeField] private int facing; //Which way the player will face after teleporting.
	[SerializeField] private Transform newSpotLocal; //Where the player will be transported to when teleporting within the same scene.
	[SerializeField] private float stayCount; //Determines how long the player must stay in the trigger before they are transported.

	public bool canUse; //Determines whether this trigger can be used.

	public float curCount = 0; //Tracks how long the player has been in the trigger

	//Check if player is within trigger
	void InTrigger(string tag, GameObject other)
	{
		if(tag == "Player")
		{
			//Keep track of how long the player has been in the trigger
			if(curCount < stayCount)
			{
				curCount += Time.time;
				return;
			}
			//Once player is in trigger for requisite amount of time. 
			//First make sure the trigger can be used
			else if(canUse)
			{
				//Teleport player within scene
				if(isLocal)
				{
					canUse = false;
					StartCoroutine(sTrans.ChangeLocation(other, newSpotLocal, this));
				}
				//Teleport player to another scene
				else
				{
					GameManager.Instance.curSpot = newSpotLd;
					GameManager.Instance.facing = facing;
					StartCoroutine(sTrans.LoadLevel(newScene));
				}
			}
		}
	}

	//For 2D games
	void OnTriggerStay2D(Collider2D other)
	{
		InTrigger(other.tag, other.gameObject);
	}
	
	//For 3D games
	void OnTriggerStay(Collider other)
	{
		InTrigger(other.tag, other.gameObject);
	}
	
	//For 2D games
	//Reset the curCount when the player exits the trigger.
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			curCount = 0;
		}
	}
	
	//For 3D games
	//Reset the curCount when the player exits the trigger.
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			curCount = 0;
		}
	}
}
