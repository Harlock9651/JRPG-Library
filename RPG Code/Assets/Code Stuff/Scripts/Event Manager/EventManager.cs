using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/*This script controls and monitors the events that happen within the game.
It is attached to the ManagerHolder.*/
public class EventManager : MonoBehaviour
{
	//Create KeyValuePairs to fill a Dictionary using a List in the inspector
	/*
		string key: The name that the value is stored under
		bool val: The value that is stored
	*/
	[Serializable]
	public class KeyValuePair
	{
		public string key;
		public bool val;
	}
		
	//Modifiable variables
	[SerializeField] private GameEventSO[] flagEventSO; //An array of GameEventSOs that control the values of the event flags tracked within the EventManager.
	[SerializeField] private List<KeyValuePair> events = new List<KeyValuePair>(); //A  list of strings and booleans. This list is saved into a dictionary and holds all of the event flags used in the game.
	private Dictionary<string, bool> eventsDict = new Dictionary<string, bool>(); //A Dictionary that stores all of the event flags in the game.
	
	//Keeps the object this script is attached to from being deleted when a new scene is loaded
	public static EventManager instance;
	private void Awake()
	{
		if (instance != this)
		{
			if(instance != null)
			{
				Destroy(instance.gameObject);
			}
			DontDestroyOnLoad(gameObject);
			instance = this;
		}			
	}
	
	//Has this script listen for given Game Event Scriptable Objects and run a function when they change.
	private void OnEnable()
	{ 
		foreach(GameEventSO fEvent in flagEventSO)
			fEvent.eventWithEventFlag += SetEvent;
	}

	//Has this script stop listening for given Game Event Scriptable Objects when it is destroyed. For cleanup purposes
	private void OnDisable()
	{
		foreach(GameEventSO fEvent in flagEventSO)
			fEvent.eventWithEventFlag -= SetEvent;
	}
	
	//Stores everything saved in the events List into a Dictionary for easier access.
	private void Start()
	{
		foreach (var kvp in events)
		{
			eventsDict.Add(kvp.key, kvp.val);
		}
	}
	
	//This function takes the name/key of the event flag the user wants to change as well as the value to change it to. 
	//If the requested event flag doesn't exist, an error message is displayed.
	/*
	string eventName: The name of the event to be changed.
	bool newVal: The value to set the requested event to.
	*/
	public void SetEvent(string eventName, bool newVal)
	{
		if(eventsDict.ContainsKey(eventName))
			eventsDict[eventName] = newVal;
		else
		{
			Debug.LogError("Cannot change "+ eventName + ". Event does not exist.");
			Debug.Log(eventsDict.Keys);
		}
	}
	
	// This function takes the name/key of the event flag the user wants to know about and returns the current value. 
	//If the requested event flag doesn't exist, an error message is displayed and false is returned.
	/*
	string eventName: The name of the event to be checked.
	*/
	public bool GetEvent(string eventName)
	{
		if(eventName == ""){return true;} //Count for no event
		if(!eventsDict.ContainsKey(eventName))
		{
			Debug.LogError("Cannot get "+ eventName + " value. Event does not exist.");
			return(false);
		}
		return eventsDict[eventName];
	}
}
