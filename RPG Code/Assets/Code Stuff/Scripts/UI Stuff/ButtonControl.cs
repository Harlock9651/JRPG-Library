using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/*This script allows players to trigger menu elements and indicates which one is currently selected.
This script is attached to any of the selectable elements in the menu.
*/
public class ButtonControl : MonoBehaviour, ISelectHandler, IDeselectHandler
{
	[SerializeField] private Image cursor;
	[SerializeField] private Sprite selected;
	[SerializeField] private Sprite deselected;
	
	//Sets the cursor to whichever 'currently selected' image there is
	public void OnSelect(BaseEventData eventData)
	{
	   cursor.sprite = selected;
	}
   
   //Sets the cursor to whichever 'currently NOT selected' image there is
    public void OnDeselect(BaseEventData eventData)
	{
	   cursor.sprite = deselected;
	}
   
   //Allow player to to activate the button via keyboard or controller rather than clicking
	public void PressButton(InputAction.CallbackContext context)
	{
		if(context.performed) //Make sure to only press the button once
		{
			Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
			button.onClick.Invoke();
		}
	}
}
