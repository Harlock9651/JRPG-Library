using UnityEngine;

/*Create a character dialogue template object type
These templates allow recurring dialogue settings (such as a recurring character) to be easily stored and reused
*/
[CreateAssetMenu(menuName = "CharacterTemplate/CharacterTemplateObject")]
public class CharacterTemplateObject : ScriptableObject
{
	//Changeable elements
	[SerializeField] private string charName;
	[SerializeField] private Sprite charPortrait;
	[SerializeField] private AudioClip voice;
	[SerializeField] private float textSpeed = 25f;
	[SerializeField] private float pauseTime = 0.6f;
	
	//Getters
	public string CharName => charName;
	public Sprite CharPortrait => charPortrait;
	public AudioClip Voice => voice;
	public float TextSpeed => textSpeed;
	public float PauseTime => pauseTime;
}
