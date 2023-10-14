using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used in tandem with the Dialogue System to play certain animations during a conversation.
//This script is attached to desired game objects that have animations to be played during a dialogue.
public class MidDialogueAnimationControl : MonoBehaviour
{
	[SerializeField] private GameEventSO[] animEventSO;
	
	//Has this script listen for given Game Event Scriptable Objects and run a function when they change.
	private void OnEnable()
	{ 
		foreach (GameEventSO animFlag in animEventSO)
		{
			animFlag.eventWithAnimEvent += EventAnim;
			animFlag.eventWithAnimFacing += FaceAnim;
		}
	}

	//Has this script stop listening for given Game Event Scriptable Objects when it is destroyed. For cleanup purposes
	private void OnDisable()
	{
		foreach (GameEventSO animFlag in animEventSO)
		{
			animFlag.eventWithAnimEvent -= EventAnim;
			animFlag.eventWithAnimFacing -= FaceAnim;
		}
	}
	
	//Play a given animation
	private void PlayAnim(string animName, float animVal)
	{
		GameObject gObj = GameObject.Find(this.gameObject.name);
		Animator charAnim = gObj.GetComponent<Animator>();
		charAnim.SetFloat(animName, animVal);
	}
	
	//Has the sprite do a generic hop animation
	public void JumpAnim()
	{
		//Sound
		//Animation
	}
	
	//Play the requested "Event" animation
    private void EventAnim(float animVal)
	{
		PlayAnim("Events", animVal);
    }
	
	//Sets the object to face the given way [0 for down, 1 for up, 2 for left, 3 for right]
	private void FaceAnim(float animVal)
	{
		switch(animVal)
		{
			case 0: //Down
				PlayAnim("Horizontal", 0);
				PlayAnim("Vertical", -1);
				break;
			case 1: //Up
				PlayAnim("Horizontal", 0);
				PlayAnim("Vertical", 1);
				break;
			case 2: //Left
				PlayAnim("Horizontal", -1);
				PlayAnim("Vertical", 0);
				break;
			case 3: //Right
				PlayAnim("Horizontal", 1);
				PlayAnim("Vertical", 0);
				break;
		}
	}
}
