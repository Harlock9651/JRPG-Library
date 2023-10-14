using UnityEngine;
using UnityEngine.Events;

/*This script allows functions to be called by Responses to Dialogue events.
*/
[System.Serializable]
public class ResponseEvent
{
	[HideInInspector] public string name;
	[SerializeField] private UnityEvent onPickedResponse;
	
	public UnityEvent OnPickedResponse => onPickedResponse;
}
