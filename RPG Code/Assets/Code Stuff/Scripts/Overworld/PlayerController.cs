using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* This script is a basic controller for the Overworld. 
It allows the Player to walk around the world, run, and open the menu. 
It also keeps track of which animator controller the player character should be using at any given time.
This script is placed on the 'Player' object
*/
public class PlayerController : MonoBehaviour
{
	//GameManager
	[SerializeField] private GameManager gManager;
	
	//Dialogue Stuff
	[SerializeField] private DialogueUI dialogueUI;
	
	//Animation Stuff
	private Animator charAnim;
	
	//Current party?
	
	//Movement Stuff
	[SerializeField] private float moveSpeed;
	[SerializeField] private float runMult;
	private bool isRunning; 
	[SerializeField] private GlobalBool canMove;
	private Rigidbody2D body;	
	private Vector2 moveInput;	
	
	//Menu stuff
	[SerializeField] private GameObject mainMenu;
	
	// Called when the object is created
    void Awake()
    {
		charAnim = GetComponent<Animator>();
		body = GetComponent<Rigidbody2D>();
		
		if(gManager == null)
		{
			gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		}
    }
	
	// Update is called once per frame
    void FixedUpdate()
    {
		if(canMove.value && !dialogueUI.IsOpen)
		{Move();}
		else
		{charAnim.SetBool("Moving", false);}
    }
	
	//Moves the player
	private void Move()
	{	
		//Set animation stuff
		GetComponent<MovementAnimations>().WalkAnimation2D(moveInput);
		
		//Actually move
        Vector2 moveVelocity = (transform.right * moveInput.x + transform.up * moveInput.y) * Time.fixedDeltaTime * moveSpeed;
		if(isRunning){moveVelocity *= runMult;}
		body.MovePosition(new Vector2(transform.position.x + moveVelocity.x, transform.position.y + moveVelocity.y));
		//controller.Move(moveVelocity);
	}
	
	//Update the move input when the player moves
	//Called by the 'Movement' buttons of the 'PlayerController' input action set
	public void GetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
	
	//Toggles running when the run button is held or let go
	//Called by the 'Run' button of the 'PlayerController' input action set
	public void SetRun(InputAction.CallbackContext context)
	{
		switch (context.phase)
		{
			case InputActionPhase.Started:
				isRunning = true;
				break;
			case InputActionPhase.Canceled:
				isRunning = false;
				break;
		}
	}
	
	//Changes the animator controller  to the proper leader
	public void SetLeader(RuntimeAnimatorController newLeader)
	{
		charAnim.runtimeAnimatorController = newLeader;
	}
	
	//Open the main menu
	//Called by the 'Menu' button of the 'PlayerController' input action set
	public void OpenMainMenu()
	{
		if(GameManager.Instance.curState == GameManager.StateType.OVERWORLD)
		{
			GameManager.Instance.SetCanMove(false);
			GameManager.Instance.curState = GameManager.StateType.MENU;
			mainMenu.SetActive(true);
		}
	}
}
