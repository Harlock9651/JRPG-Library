using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.InputSystem;

/* This script controls and updates the various UI elements of the Dialogue system.
This script is attached to the 'Canvas' that holds the 'Dialogue Box'.
*/
public class DialogueUI : MonoBehaviour
{
	//Keeps the object this script is attached to from being deleted when a new scene is loaded
	private static DialogueUI original;
	/*private void Awake()
	{		
		if (original != this) 
		{
			if(original != null)
			{
				//Destroy(original.gameObject);
			}
			DontDestroyOnLoad(gameObject);
			original = this;
		}
	}*/
	
	//Tracks the different UI elements to update
	[SerializeField] private GameObject dialogueBox;
	[SerializeField] private Image charPortraitHolder;
	[SerializeField] private TMP_Text textLabel;
	
	//Tracks whether dialogue is open or not. Other functions can check, but not write. 
	public bool IsOpen {get; private set;} 
	
	//Tracks the inputs used for dialogue actions
	[SerializeField] private InputActionAsset controls;
	private InputActionMap iaMap;
	public InputAction conf; //Goes to next message
	public InputAction skip; //Skips typing of current message
	
	private TypewriterEffect typewriterEffect; //Used to easilly access the typewriter effect	
	private ResponseHandler responseHandler;   //Used to easilly access response handlers
	
	//Start is called before the first frame update	
	private void Start()
	{
		//Set player input
		iaMap = controls.FindActionMap("Dialogue");
		conf = iaMap.FindAction("Confirm");
		skip = iaMap.FindAction("Skip");
		
		//Set typewriterEffect and responseHandler
		typewriterEffect = GetComponent<TypewriterEffect>(); 
		responseHandler = GetComponent<ResponseHandler>();
		
		//Make sure dialogue box is closed initially
		CloseDialogueBox();
	}
	
	//Starts the dialogue process
	public void ShowDialogue(DialogueObject dialogueObject)
	{
		//Let system know the dialogue box is open
		IsOpen = true;
		dialogueBox.SetActive(true);

        //Start stepping through dialogue
        StartCoroutine(StepThroughDialouge(dialogueObject));
	}
	
	//Adds response events
	public void AddResponseEvents(ResponseEvent[] responseEvents)
	{
		responseHandler.AddResponseEvents(responseEvents);
	}
	
	//Display each dialouge in a given dialogueObject one at a time
	private IEnumerator StepThroughDialouge(DialogueObject dialogueObject)
	{
		//Run starting events if necessary !TS This seems to be fine as is, but maybe a flag would be useful
		if(dialogueObject.PreEvent != null)// && !pEventDone)
		{
			dialogueObject.PreEvent?.Invoke();
			//pEventDone;
		}
			
		//Step through different dialogue objects
		for(int j = 0; j < dialogueObject.DialogueList.Length; j++)
		{
			//Set dialogue box values
			charPortraitHolder.sprite = dialogueObject.DialogueList[j].charPortrait; //Update Portrait
			AudioClip sfx = dialogueObject.DialogueList[j].voice; //Update SFX
			//Update text writing values
			float txtSpd = dialogueObject.DialogueList[j].textSpeed;
			float pTime = dialogueObject.DialogueList[j].pauseTime;

			//get anim, play anim, play different anims based on j int

			//Write out the text in the dialogue
			for (int i = 0; i < dialogueObject.DialogueList[j].text.Length; i++)
			{
				//Update text
				string dialogue = dialogueObject.DialogueList[j].text[i];
				//Check if this dialouge runs the typing effect
				if(dialogueObject.DialogueList[j].typeText){yield return RunTypingEffect(dialogue, sfx, txtSpd, pTime);}
				textLabel.text = dialogue;
				
				//Set up responses if any exist
				if(i == dialogueObject.DialogueList[j].text.Length && dialogueObject.HasResponses) break;
				
				yield return null;
				yield return new WaitUntil(() => (conf.triggered)); //Wait for button press before continuing
			}
			//Run post element event if necessary
			if(dialogueObject.DialogueList[j].postEvent != null)
			{
				dialogueObject.DialogueList[j].postEvent?.Invoke();
			}
		}
		
		//If there is a response dialouge, send it. Otherwise close the dialog box.
		if(dialogueObject.HasResponses)
		{
			responseHandler.ShowResponses(dialogueObject.Responses);
		}
		else
		{
			CloseDialogueBox();
			
			//Call the next dialogue if necessary
			if(dialogueObject.NextDialog != null)
			{
				ShowDialogue(dialogueObject.NextDialog);
			}
		}
	}
	
	//Run the typing effect on text
	private IEnumerator RunTypingEffect(string dialogue, AudioClip sfx, float tSpd, float pTime)
	{
		typewriterEffect.Run(dialogue, textLabel, sfx, tSpd, pTime);
		
		//Allow player to skip dialogue
		while(typewriterEffect.IsRunning)
		{
			yield return null;
			//Skip dialogue
			if(skip.triggered)
			{
				typewriterEffect.Stop();
			}
		}
	}
	
	//Close and clear dialogue box
	public void CloseDialogueBox()
	{
		dialogueBox.SetActive(false);
		textLabel.text = string.Empty; 
		IsOpen = false;
    }
}
