using UnityEngine;
using UnityEditor;

/*Puts a reset button on DialogueResponseEvents in the Inspector*/
[CustomEditor(typeof(DialogueResponseEvents))]
public class DialogueResponseEventsEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		DialogueResponseEvents responseEvents = (DialogueResponseEvents)target;
		
		//Provides a refresh button
		if(GUILayout.Button("Refresh"))
		{
			responseEvents.OnValidate();
		}
	}
}
