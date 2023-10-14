using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*This script holds a variety of general functions. 
These functions include moving, destroying, or spawning objects.
This script is placed on the 'EventSystem' game object.
*/
public class EventController : MonoBehaviour
{
	[SerializeField] private GameObject evtTarget;
	
	//Changes the GameObject the event functions work on
	public void setTarget(GameObject newObj)
	{
		evtTarget = newObj;
	}
	
	//Places the given object at the given location
	public void objectPlacer(Transform loc)
	{
		evtTarget.transform.localPosition = loc.localPosition;
	}
	
	//'Walks' given object from given point to desired point
	public void objectWalker(InputAction.CallbackContext context)
	{
	}
	
	//Destroy the given objects
	public void destroyObject(GameObject obj)
	{
		Destroy(obj);
	}
	
	//Spawn objects at the given location
	public void spawnObject(Transform loc)
	{
		Instantiate(evtTarget, loc.position, loc.rotation);
	}
	
	//Object animation
	//Probaly not necessary?
	public void objectAnimation(float animVal)
	{
		Animator charAnim = evtTarget.GetComponent<Animator>();
		charAnim.SetFloat("Events", animVal);
	}
}
