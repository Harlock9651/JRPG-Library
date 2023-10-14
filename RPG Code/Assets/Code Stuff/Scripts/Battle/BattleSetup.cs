using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*This script organizes the Battle Screen before the battle starts.
This script is placed on the 'Canvas' element of the 'BattleScene'
*/
public class BattleSetup : MonoBehaviour
{
	[SerializeField] private Image btlBG; //The background of the battle
	[SerializeField] private Image[] enemySide; //List of potential enemy locations
	[SerializeField] private Transform[] playerSide; //List of potential Player locations
	[SerializeField] private BattleUI btlUI; //The display where Player and enemy info is written
	
	private List<string> curEnemyList = new List<string>(); //A list to keep track of what enemies are in the fight
	
    // Start is called before the first frame update
    void Start()
    {
		EncounterSetup curEnemy; //Keeps track of individual enemy information
		btlBG.sprite = GameManager.Instance.curEnc.Background;//Sets the background

		//Set the enemies in the scene
		for(int i=0; i<GameManager.Instance.curEnc.Enemies.Length; i++)
		{
			curEnemy = GameManager.Instance.curEnc.Enemies[i];
			enemySide[curEnemy.position].sprite = curEnemy.enemy.BattleSprite;
			enemySide[curEnemy.position].GetComponent<RectTransform>().sizeDelta = curEnemy.enemy.BattleSpriteSize;
			enemySide[curEnemy.position].enabled = true;
			curEnemyList.Add(curEnemy.enemy.EnemyName);
			
			//Set Enemy ATB
			btlUI.SetATB(enemySide[curEnemy.position].transform.Find("ATB"), curEnemy.enemy.Stats.SPD);
		}
		WriteEnemyInfo();
		
		//Set up players in the scene
		btlUI.FillPlayerInfo(GameManager.Instance.curParty);
		for (int i=0; i<GameManager.Instance.curParty.Length; i++)//gManager.curParty.Length; i++)
		{
			switch(i)
			{
				case 0: PlacePlayer(0, GameManager.Instance.curParty[i].BattleObj);//gManager.curParty[i].BattleObj);
					break;
				case 1: PlacePlayer(2, GameManager.Instance.curParty[i].BattleObj);//gManager.curParty[i].BattleObj);
					break;
				case 2: PlacePlayer(4, GameManager.Instance.curParty[i].BattleObj);//gManager.curParty[i].BattleObj);
					break;
				case 3: PlacePlayer(6, GameManager.Instance.curParty[i].BattleObj);//gManager.curParty[i].BattleObj);
					break;
			}
		}
    }
	
	//Places the player character in the proper spot in the field
	void PlacePlayer(int row, GameObject pMember)
	{
		//Place in front
		//Place in back
		
		Instantiate(pMember.transform, playerSide[row].transform);
	}
	
	//Writes out enemy information to the UI
	public void WriteEnemyInfo()
	{
		//Groups the same species of enemies together
		var enemyList = curEnemyList.GroupBy(x => x)
			.Where(g => g.Count() > 0)
			.Select (y => new Tuple<string, int>(y.Key, y.Count()))
			.ToList();
		
		btlUI.FillEnemyInfo(enemyList);
	}
}
