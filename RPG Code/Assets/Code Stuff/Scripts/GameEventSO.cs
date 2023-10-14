using UnityEngine;
using UnityEngine.Events;

/* This script defines functions to control global variables.
*/
[CreateAssetMenu(fileName = "GameEventSO", menuName = "ScriptableObject/GameEventSO")]
public class GameEventSO : ScriptableObject {
	public UnityAction<string, bool> eventWithEventFlag;
	public UnityAction<float> eventWithAnimEvent;
	public UnityAction<float> eventWithAnimFacing;
	
	//Event flags
	//Event Flag Trigger
    public void RaiseEvent(string flagName, bool flagSet) {
        eventWithEventFlag?.Invoke(flagName, flagSet);
    }
	
	//Raises a specific Event Flag
	public void RaiseFlag(string flagName)
	{
		RaiseEvent(flagName, true);
	}
	
	//Lowers a specific Event Flag
	public void LowerFlag(string flagName)
	{
		RaiseEvent(flagName, false);
	}
	
	//Animation control
	public void ChangeEventAnim(float animVal)
	{
		eventWithAnimEvent?.Invoke(animVal);
	}
	
	//Facing values (for 2D games)
	public void ChangeFacingAnim(float animVal)
	{
		eventWithAnimFacing?.Invoke(animVal);
	}
}
