using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* This script allows the player to initiate a dialogue event
This script is placed on the 'Player' game object
*/
public class DialogueController : MonoBehaviour
{
    //Dialogue Stuff
	[SerializeField] private DialogueUI dialogueUI; //The canvas element that holds the dialogue UI
	public DialogueUI DialogueUI => dialogueUI;
	
	public IInteractable Interactable { get; set; }
	
	//Opens dialogue when player presses the interact button
	public void GetInteract(InputAction.CallbackContext context)
	{
		if(Interactable != null && !dialogueUI.IsOpen)
		{
			Interactable.Interact(this);
			return;
		}
	}
}
