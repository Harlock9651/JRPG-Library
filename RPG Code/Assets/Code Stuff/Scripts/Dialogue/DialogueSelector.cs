using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script allows an object's dialogueObject to change based on event flags.
This allows for characters to say different things once certain story points have been reached.
This script is attached to any object with multiple dialogues
*/
public class DialogueSelector : MonoBehaviour
{	
	//Tracks what dialogues can be set to the gameObject
	/*
		DialogueObject dlogObj: The actual dialogue object to put in the activator.
		string flagName: The name of the event flag that needs to be set for the associated DialogueObject to be put into effect.
		bool flagVal: The value that the associated event flag needs to be set too before the DialougeObject is changed.
	*/
    [System.Serializable]
	public class DialogueSelections
	{
		public DialogueObject dlogObj;
		public string flagName;
		public bool flagVal;
	}
	
	//Modifiable variables
	[SerializeField] private GameEventSO[] flagEventSO; //Event flags this game object listens for
	[SerializeField] private DialogueObject defDlog; //The default dialogue object that is set if no other options are valid.
	[SerializeField] private List<DialogueSelections> dlogList = new List<DialogueSelections>(); //A list of dialogues this object can be set to. Order of the dialogue denotes priority, whatever is on top has highest priority.
	
	//Has this script listen for given Game Event Scriptable Objects and run a function when they change.
	private void OnEnable()
	{ 
		foreach (GameEventSO eventFlag in flagEventSO)
			eventFlag.eventWithEventFlag += FlagChange;
	}

	//Has this script stop listening for given Game Event Scriptable Objects when it is destroyed. For cleanup purposes
	private void OnDisable()
	{
		foreach (GameEventSO eventFlag in flagEventSO)
			eventFlag.eventWithEventFlag -= FlagChange;
	}
	
	//Sets the proper dialogue object to the attached game object.
	public void SetProperDialogue()
	{
		//Check each dialogue selection and determine which to use
		foreach (var dSel in dlogList)
		{
			//Returns the first dialogue with the proper value. Order of the dialogue denotes priority
			if(EventManager.instance.GetEvent(dSel.flagName) == dSel.flagVal)
			{
				Debug.Log("Dialogue Changed");
				this.GetComponent<DialogueActivator>().UpdateDialogueObject(dSel.dlogObj);
				return;
			}
		}
		//If nothing else matches, return the default dialogue option
		this.GetComponent<DialogueActivator>().UpdateDialogueObject(defDlog);
		return;
	}
	
	//Changes the dialogue when the player approaches.
	/*
		Collider other: The collider on the other object that has entered the trigger
	*/
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player") && other.TryGetComponent(out DialogueController player))
		{
			SetProperDialogue();
		}
	}
	
	
	//Updates the dialogue when the event flag changes. This is useful for events that change when the 
	//Player is still in the trigger.
	/*
	string flagName: The name of the eventFlag that has been changed.
	bool flagVal: The new value that the eventFlag has been changed to.
	*/
	//!TS This can most assuredly be optimized
	private void FlagChange(string flagName, bool flagVal)
	{
		//Check each dialogue selection and determine which to use
		foreach (var dSel in dlogList)
		{
			//Returns the first dialogue with the proper value. Order of the dialogue denotes priority
			if(dSel.flagName == flagName && dSel.flagVal == flagVal)
			{
				this.GetComponent<DialogueActivator>().UpdateDialogueObject(dSel.dlogObj);
				return;
			}
		}
	}
}
