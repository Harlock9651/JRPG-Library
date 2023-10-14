using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script controls and updates the UI during a battle.
This script is set to the 'BattleController'
*/
public class BattleUI : MonoBehaviour
{
	[SerializeField] private Transform enemyInfoHolder;
	[SerializeField] private GameObject enemyGuiElement;
	[SerializeField] private Transform playerInfoHolder;
	[SerializeField] private GameObject playerGuiElement;
	
	//Deletes all information from a given transform
	private void ClearOutInfo(Transform holder)
	{
		//Clear out old elements (prevents duplicates)
		for (int i=0; i<holder.childCount; i++)
		{
			Destroy(holder.GetChild(i).gameObject);
		}
	}
	
	//Enemy Info 
	//Call at begining of battle, when enemy is added, or when enemy dies
	public void FillEnemyInfo(List<Tuple<string,int>> enemyList)
	{
		//Clear out old info
		ClearOutInfo(enemyInfoHolder);
				
		//Go through existing enemies
		for(int i=0; i<enemyList.Count; i++)
		{
			GameObject newEnemyInfo = Instantiate(enemyGuiElement) as GameObject;
			newEnemyInfo.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = enemyList[i].Item1;
			if(enemyList[i].Item2 > 1)
				newEnemyInfo.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "x"+enemyList[i].Item2;
			else
				newEnemyInfo.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "";
			
			//Add element to scene
			newEnemyInfo.transform.SetParent(enemyInfoHolder, false);
			newEnemyInfo.gameObject.SetActive(true);
		}
	}
	
	//Initializes a given Active Time Bar (ATB)
	public void SetATB(Transform atbHolder, int spd)
	{
		//Make sure slider is enabled (For enemies)
		atbHolder.gameObject.SetActive(true);
		
		//Transform atbHolder = newPlayerInfo.transform.Find("ATB");
		UnityEngine.UI.Slider atbSlider = atbHolder.Find("Slider").GetComponent<UnityEngine.UI.Slider>();
		int atbLength = ((60-spd)*160);//(60-partyList[i].Stats.SPD)*160);
		atbSlider.maxValue = atbLength; //Set total value of character's ATB bar
		//Set starting value of ATB bar
		int randVal = UnityEngine.Random.Range(0, atbLength-1);
		atbSlider.value = randVal;
		//Link character to slider (Is this necessary?)
		/*atbHolder.GetComponent<AtbFiller>().SetMember(partyList[i]);*/
		
	}
	
	//Player Info
	public void FillPlayerInfo(PartyMemberObject[] partyList)
	{
		//Clear out old info
		ClearOutInfo(playerInfoHolder);
		
		//Go through players
		for(int i=0; i<partyList.Length; i++)
		{
			GameObject newPlayerInfo = Instantiate(playerGuiElement) as GameObject;
			newPlayerInfo.transform.Find("P Name").GetComponent<UnityEngine.UI.Text>().text = partyList[i].CharName;//Set name
			
			/*Set HP*/
			Transform hpHolder = newPlayerInfo.transform.Find("HP Text");
			hpHolder.Find("CurHP").GetComponent<UnityEngine.UI.Text>().text = partyList[i].Stats.CUR_HP.ToString();
			hpHolder.Find("MaxHP").GetComponent<UnityEngine.UI.Text>().text = partyList[i].Stats.MAX_HP.ToString();
			
			/*Set ATB*/
			SetATB(newPlayerInfo.transform.Find("ATB"), partyList[i].Stats.SPD);
			
			/*Add element to scene*/
			newPlayerInfo.transform.SetParent(playerInfoHolder, false);
			newPlayerInfo.gameObject.SetActive(true);
		}
	}
}
