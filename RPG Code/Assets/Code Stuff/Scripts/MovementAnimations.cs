using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script has functions to control movement related animations of the attached object
*/
public class MovementAnimations : MonoBehaviour
{
	//Animation Stuff
	private Animator charAnim;
	private SpriteRenderer sprRend;
	[SerializeField] private bool flipSide;
	
	
    //Called when object is created
    void Awake()
    {
        sprRend = GetComponent<SpriteRenderer>();
		charAnim = GetComponent<Animator>();
    }
	
	//Play the idle animation
	public void StopWalk()
	{
		charAnim.SetBool("Moving", false);
	}

	//Play the proper walk animations
    public void WalkAnimation2D(Vector2 moveInput)
	{
		//Set animation stuff
		if(moveInput.x == 0 && moveInput.y == 0)
		{StopWalk();}
		else
		{
			charAnim.SetBool("Moving", true);
			charAnim.SetFloat("Horizontal", moveInput.x);
			charAnim.SetFloat("Vertical", moveInput.y);
		}
		//Flip animation if necessary
		if(flipSide)
		{
			if(moveInput.x > 0)
			{sprRend.flipX = true;}
			else if(moveInput.x < 0)
			{sprRend.flipX = false;}
		}
	}
}
