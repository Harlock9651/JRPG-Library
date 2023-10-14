using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This scipt allows player to start a dialogue event with a given object
This script is placed on any game object the player can start a dialogue with
*/
public class DialogueActivator : MonoBehaviour, IInteractable
{
	//The DialogueObject that is opened
	[SerializeField] private DialogueObject dialogueObject;
	
	//Changes the DialogueObject associated with this game object
	public void UpdateDialogueObject(DialogueObject dialogueObject)
	{
		this.dialogueObject = dialogueObject;
	}
	
	//When player enters the trigger area, allow them to open the dialogue
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player") && other.TryGetComponent(out DialogueController player))
		{
			player.Interactable = this;
		}
	}
	
	//When player leaves the trigger area, prevent them from opening the dialogue
	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("Player") && other.TryGetComponent(out DialogueController player))
		{
			if(player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
			{
				player.Interactable = null;
			}
		}
	}
	
	//Allows player to open the dialogue when interact button is pressed
	public void Interact(DialogueController player)
	{
		//Set up Response events (if necessary)
		foreach(DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
		{
			if(responseEvents.DialogueObject == dialogueObject)
			{
				player.DialogueUI.AddResponseEvents(responseEvents.R_Events);
				break;
			}
		}

		player.DialogueUI.ShowDialogue(dialogueObject);
	}
}
