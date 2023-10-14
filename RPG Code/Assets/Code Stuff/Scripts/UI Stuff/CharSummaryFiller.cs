using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*This script fills in the information about party the active party members 
This script is used by the 'CharSummaryElements'. 'CharSummaryElements' are created any time information about party members is required such as when the Main Menu is opened to check party status.
*/
public class CharSummaryFiller : MonoBehaviour
{
	//Ideally I'd use something like this to fill in just what I need
	/*[System.Serializable]
	public class TextList
	{
		public Text txtHolder;
		public string txt;
	}*/
	
	//Character info
	public PartyMemberObject curChar;
	
	//Reference to GUI elements
	[SerializeField] private Image profPic;
	[SerializeField] private Text nameHolder;
	[SerializeField] private Text lvHolder;
	[SerializeField] private Text cHpHolder;
	[SerializeField] private Text mHpHolder;
	[SerializeField] private Text cMpHolder;
	[SerializeField] private Text mMpHolder;
	
	//Places the given text into the provided slot
	private void fillText(Text txtHolder, string val)
	{
		if(txtHolder != null)
			txtHolder.text = val;
	}
	
	//Fills out the party member information in the proper spots
	void fillCharacterInfo()
	{
		profPic.sprite = curChar.ProfPic;
		fillText(nameHolder, curChar.CharName);
		fillText(lvHolder, curChar.Stats.LV.ToString());
		fillText(cHpHolder, curChar.Stats.CUR_HP.ToString());
		fillText(mHpHolder, curChar.Stats.MAX_HP.ToString());
		fillText(cMpHolder, curChar.Stats.CUR_MP.ToString());
		fillText(mMpHolder, curChar.Stats.MAX_MP.ToString());
	}
	
	//Called when the object becomes enabled and active.
	void OnEnable()
	{
		fillCharacterInfo();
	}
}
