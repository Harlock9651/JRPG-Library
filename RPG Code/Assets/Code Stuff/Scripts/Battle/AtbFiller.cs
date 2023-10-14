using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script fills in the Active Time Bar [ATB] which determines who gets to attack next.
   It is attached to Enemy ATBs (stored in the EnemySide of the BattleAreaHolder) and Player ATBs (stored in the PlayerSide of the BattleAreaHolder)
WIP
*/
public class AtbFiller : MonoBehaviour
{
	//Modifiable variables
	private PartyMemberObject pMem; //!TS Need to change to account for enemies as well
	[SerializeField] private UnityEngine.UI.Slider slider;
	[SerializeField] private BattleControl bCont; //Reference to the Battle Controller
	
	//Get the speed of the character
	public void SetMember(PartyMemberObject member)
	{
		pMem = member;
	}

    // Update is called once per frame
    void Update()
    {
		//Disable if bar is gone?
		
        //Only increase if allowed to (NEED TO FIX)
		//(NEED TO FIX) Make sure to read the battle speed from the Game Manager
		//(NEED TO FIX) Change speed based off of stats
		slider.value += GameManager.Instance.btlSpd;
		
		//When slider is full, let the battle Controller know
		if(slider.value >= slider.maxValue)
		{
			bCont.AddToTurnOrder(this.transform.parent.gameObject);
		}
    }
}
