using UnityEngine;
using UnityEngine.Events;

/*This Script creates the Ability object type.
  WIP
*/
[CreateAssetMenu(menuName = "Ability/AbilityObject")]
public class AbilityObject : ScriptableObject
{
	/*public enum Status {Normal, Weak, Faint, Poison, Paralyzed, Wrapped, Confused, Berserk,
	Blind, Slow, Stop, Frozen, LockOn, FutSight, Haste, Regen, Blink, Invisible};
	
	public enum Elemental {Physical, Magical, Piercing, Slashing, Bashing, Fire, Water, Ice,
	Lighting, Psychic, Poison, Earth, Air, Holy, Dark, nElem};*/
	
	//Changeable elements
	/*[SerializeField] private float atkMult;
	[SerializeField] private int statUsed;
	[SerializeField] private GameManager.Element[] elem;
	[SerializeField] private GameManager.Status[] statBuff;*/
	[SerializeField] private UnityEvent[] ablFunc;
	
	//Getters
	/*public float AtkMult => atkMult;
	public int StatUsed => statUsed;
	public GameManager.Element[] Elem => elem;
	public GameManager.Status[] StatBuff => statBuff;*/
	public UnityEvent[] AblFunc => ablFunc;
}
