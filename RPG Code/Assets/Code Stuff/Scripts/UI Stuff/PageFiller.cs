using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script fills a given holder with user defined element types. Presently, this script is planned to be used with the various menus that require party information (Main Menu, status menu, shop equipment menus, swap party menu, etc.)
This script simply instantiates and places the user defined elements, providing whatever information said elements need. The actual usage/organization of said information needs to be determined by the element itself (via unique scripts, prefabs, etc.)
*/
public class PageFiller : MonoBehaviour
{
    [SerializeField] private GameObject guiElement;
	[SerializeField] private UnityEngine.Object[] elemList;
	[SerializeField] private Transform holder;
	[SerializeField] private string gmList;
	
	//!TS surely I can make this more generalized... That or have *this* script just be for the various party member things
	void SetElemList()
	{
		elemList  = (PartyMemberObject[])GameManager.Instance.GetType().GetField("curParty").GetValue(GameManager.Instance);
	}
	
	//Called when the object becomes enabled and active.
	void OnEnable()
	{
		//Clear out old elements (prevents duplicates)
		for (int i=0; i<holder.childCount; i++)
		{
			Destroy(holder.GetChild(i).gameObject);
		}
	
		//Fill in the list with new elements
		if(gmList != null)
			SetElemList();
		AddElements();
	}
	
	//Create the elements and place them in the holder
	//[NOTE] Since these elements are user defined, specific use cases need to be defined below as seperate switch cases
	void AddElements()
	{
		foreach (UnityEngine.Object elem in elemList)
		{
			GameObject tmp = guiElement;
			switch(guiElement.name)
			{
				case "CharSummaryElement":
					tmp.GetComponent<CharSummaryFiller>().curChar = (PartyMemberObject)elem;
					break;
			}
			
			//Create the new element
			GameObject gridElem = Instantiate(tmp) as GameObject;
			
			//Place elements
			gridElem.transform.SetParent(holder, false);
			gridElem.transform.localScale = new Vector3(1, 1, 1);
			
			//Make visible
			gridElem.gameObject.SetActive(true);
		}
	}
}
