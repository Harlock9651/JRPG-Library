using UnityEngine;
using System.Collections;

/*Creates the Encounter object type
An Encounter is a collection of enemy objects organized in a specific way. These objects are what are used to fill up the battle scene.
*/
[CreateAssetMenu(menuName = "Encounter/EncounterObject")]
public class EncounterObject : ScriptableObject
{
	//Changeable elements
	[SerializeField] private EncounterSetup[] enemies; //The enemies within this encounter
	//Special animation?
	[SerializeField] private Animator sceneTrans;
	//sfx
	[SerializeField] private AudioClip sceneChangeMusic;
	//music
	[SerializeField] private AudioClip battleMusic;
	[SerializeField] private AudioClip[] victoryMusic;
	//background
	[SerializeField] private Sprite background;
	
	//Getters
	public EncounterSetup[] Enemies => enemies;
	public Animator SceneTrans => sceneTrans;
	public AudioClip scMusic => sceneChangeMusic;
	public AudioClip bMusic => battleMusic;
	public AudioClip[] vMusic => victoryMusic;
	public Sprite Background => background;
}

//The elements within the encounter type
[System.Serializable]
public class EncounterSetup
{
	public EnemyObject enemy;
	public int position;
}