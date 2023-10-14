using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script keeps track of the various attack formula logic used in the game.
*/
public class AtkFormulae : MonoBehaviour
{	
	/*Enemy attack formula*/
	//aAtk: The damage value of the attack/weapon the enemy is using
	//tDef: The applicable defensive value of the target
	//aStr: The applicable offensive stat of the attacking enemy
	//aLvl: The level of the attacking enemy
    public int enemyAtk(int aAtk, int tDef, int aStr, int aLvl)
	{
		int baseDmg = aAtk - tDef;
		int bonusDmg = aStr + UnityEngine.Random.Range(0, ((aLvl+aStr)/4));
		return baseDmg * bonusDmg;
	}
	
	/*Player attack formula*/
	//aAtk: The damage value of the attack/weapon the player is using
	//tDef: The applicable defensive value of the target
	//aStr: The applicable offensive stat of the attacking player
	//aLvl: The level of the attacking player
    public int playerAtk(int aAtk, int tDef, int aStr, int aLvl)
	{
		int baseDmg = aAtk - tDef;
		int bonusDmg = aStr + UnityEngine.Random.Range(0, ((aLvl+aStr)/8));
		return baseDmg * bonusDmg;
	}
}
