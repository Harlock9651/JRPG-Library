using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* This script controlls the logic of navigating between the various menus.
This script is attached to Main Menu and the various sub-menus.
*/
public class MenuController : MonoBehaviour
{	
	[SerializeField] private GameObject initSelect;
	[SerializeField] private Button startBtn;

	//Called when the object becomes enabled and active.
	private void OnEnable()
	{
		Selection.activeGameObject = initSelect;
		if(startBtn != null)
		{
			startBtn.Select();
		}
	}

	//Determines whether a given submenu can currently be controlled by the player
	//!TS This may be better as a general or event script
    public void ControlActive(GameObject obj, bool isActive)
	{
		obj.SetActive(isActive);
	}
	
	//Changes which sub-menu is currently being controlled by the player
	public void ChangeMenu(GameObject newMenu)
	{
		ControlActive(newMenu, true);
		ControlActive(this.gameObject, false);
	}
	
	//Closes the menus and returns to regular gameplay
	public void CloseMenu()
	{
		ControlActive(this.gameObject, false);
		GameManager.Instance.SetCanMove(true);
		GameManager.Instance.curState = GameManager.StateType.OVERWORLD;
	}
}
