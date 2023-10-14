using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SAP2D;

/* This script has the object it is attached too walk to a target point.
*/
public class aStarController : MonoBehaviour
{
	//Social Distancing
    void OnTriggerEnter2D(Collider2D other)
	{
		//Make sure this is an A* thing
		SAP2DAgent sapAgent = GetComponent<SAP2DAgent>();
		if(sapAgent != null)
		{
			//Stop if this is the destination
			if(other.transform.parent == sapAgent.Target)
			{
				sapAgent.CanMove = false;
				GetComponent<MovementAnimations>().StopWalk();
			}
		}
	}
}
