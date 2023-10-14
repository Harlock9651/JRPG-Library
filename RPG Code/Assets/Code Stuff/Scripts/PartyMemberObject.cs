using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

/*Creates the Party Member object type
Party Members are the individiual characters that can be in the active party
*/
[CreateAssetMenu(menuName = "Party/PartyObject")]
public class PartyMemberObject : ScriptableObject
{
	//Changeable elements
	[SerializeField] private string charName;
	[SerializeField] private StatsObject stats;
	[SerializeField] private RuntimeAnimatorController animCont;
	[SerializeField] private Sprite profPic;
	[SerializeField] private GameObject battleObj;
	
	//Getters
	public string CharName => charName;
	public StatsObject Stats => stats;
	public RuntimeAnimatorController AnimCont => animCont;
	public Sprite ProfPic => profPic;
	public GameObject BattleObj => battleObj;
}
