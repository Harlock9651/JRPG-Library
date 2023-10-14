using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*Call non-collider functions from a trigger.
Desired functions must be called from extant game objects.*/
public class TriggerEvents : MonoBehaviour
{
	[SerializeField] private UnityEvent[] enterFunctList;
	[SerializeField] private string enterTag;
	[SerializeField] private UnityEvent[] stayFunctList;
	[SerializeField] private string stayTag;
	[SerializeField] private UnityEvent[] exitFunctList;
	[SerializeField] private string exitTag;
	
	//Invokes the provided functions if the object that tripped the trigger has the proper tag
	void CallFunctions(bool rTag, UnityEvent[] functList)
	{
		if(rTag)
		{
			foreach(UnityEvent funct in functList)
			{
				funct.Invoke();
			}
		}
		return;
	}
	
	//For 2D games
	void OnTriggerEnter2D(Collider2D other)
	{
		CallFunctions(other.tag == enterTag, enterFunctList);
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		CallFunctions(other.tag == stayTag, enterFunctList);
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		CallFunctions(other.tag == exitTag, exitFunctList);
	}
	
	//For 3D games
	void OnTriggerEnter(Collider other)
	{
		CallFunctions(other.tag == enterTag, enterFunctList);
	}
	
	void OnTriggerStay(Collider other)
	{
		CallFunctions(other.tag == stayTag, stayFunctList);
	}
	
	void OnTriggerExit(Collider other)
	{
		CallFunctions(other.tag == exitTag, exitFunctList);
	}
}
