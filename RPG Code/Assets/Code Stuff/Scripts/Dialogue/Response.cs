using UnityEngine;

/*Create Response object type
Responses are choices Players can make in a dialogue (For example, 'Yes' or 'No').
*/
[System.Serializable]
public class Response
{
	//Changeable elements
	[SerializeField] private string responseText;
	[SerializeField] private DialogueObject dialogueObject;
	
	//Getters
	public string ResponseText => responseText;
	public DialogueObject DialogueObject => dialogueObject;
}
