using UnityEngine;

/*Creates the Enemy object type
*/
[CreateAssetMenu(menuName = "Enemy/EnemyObject")]
public class EnemyObject : ScriptableObject
{
	//Changeable elements
	[SerializeField] private string enemyName;
	[SerializeField] private StatsObject stats;
	[SerializeField] private Sprite battleSprite;
	[SerializeField] private Vector2 battleSpriteSize = new Vector2(150, 150);//Change this to default enemy battle size
	[SerializeField] private AbilityObject[] abilities;
	
	//Getters
	public string EnemyName => enemyName;
	public StatsObject Stats => stats;
	public Sprite BattleSprite => battleSprite;
	public Vector2 BattleSpriteSize => battleSpriteSize;
	public AbilityObject[] Abilities => abilities;
}
