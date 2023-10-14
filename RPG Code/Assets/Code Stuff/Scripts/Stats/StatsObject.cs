using UnityEngine;

/*Creates the Stats object type
*/
[CreateAssetMenu(menuName = "Stats/StatsObject")]
[System.Serializable]
public class StatsObject : ScriptableObject
{
	//Changeable elements
	[SerializeField] private int level;
	[SerializeField] private int attack;
	[SerializeField] private int defense;
	[SerializeField] private int cur_hp;
	[SerializeField] private int max_hp;
	[SerializeField] private int vigor;
	[SerializeField] private int mAttack;
	[SerializeField] private int mDefense;
	[SerializeField] private int cur_mp;
	[SerializeField] private int max_mp;
	[SerializeField] private int speed;
	[SerializeField] private Custom[] customStats;
	
	//Getters
	public int LV => level;
	public int ATK => attack;
	public int DEF => defense;
	public int CUR_HP => cur_hp;
	public int MAX_HP => max_hp;
	public int VIG => vigor;
	public int M_ATK => mAttack;
	public int M_DEF => mDefense;
	public int CUR_MP => cur_mp;
	public int MAX_MP => max_mp;
	public int SPD => speed;
	public Custom[] CustomStats => customStats;
	
	[System.Serializable]
	public class Custom : ISerializationCallbackReceiver
	{
		public string statTitle;
		public int statValue;
		
		//Needed for OnValidate to work
		void ISerializationCallbackReceiver.OnBeforeSerialize () {}
		void ISerializationCallbackReceiver.OnAfterDeserialize () {}
	}
}
