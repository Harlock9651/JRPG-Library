using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	This script determines where the player will be placed and facing when a scene loads.
	This script is placed on the 'Player' game object.
*/
public class PlayerPlacer : MonoBehaviour
{
	[SerializeField]private GlobalBool canMove;
	private Animator charAnim;

    //Start is called before the first frame update
    void Start()
    {
		charAnim = GetComponent<Animator>();
        GameManager.Instance.player = this.gameObject;
		this.transform.localPosition = GameManager.Instance.curSpot;
		
		//Set player stuff
		GameManager.Instance.ChangeLeader();
		canMove.value = true;
		SetFacing();
		
		//Reset random encounter counter
		GameManager.Instance.REncCountReset();
    }
	
	//Determines which way the player should be facing
	private void SetFacing()
	{
		switch(GameManager.Instance.facing)
		{
			case 0: //Up
				charAnim.SetFloat("Vertical", 1);
				return;
			case 1: //Down
				charAnim.SetFloat("Vertical", -1);
				return;
			case 2: //Left
				charAnim.SetFloat("Horizontal", -1);
				return;
			case 3: //Right
				charAnim.SetFloat("Horizontal", 1);
				return;
			default:
				charAnim.SetFloat("Vertical", -1);
				return;
		}
	}
}
