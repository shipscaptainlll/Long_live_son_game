using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades_Manager : MonoBehaviour
{
	// Start is called before the first frame update
	public Button BPickAxe;
	public UBPickAxe PPickAxe;
	public Button BAxe;
	public UBAxe PAxe;
	public Button BScissors;
	public UBScissors PScissors;
	public Button BBucket;
	public UBBucket PBucket;
	public RShardResourceCounter rShardResourceCounter;

	void Start()
	{
		Button button_BPickAxe = BPickAxe.GetComponent<Button>();
		Button button_BScissors = BScissors.GetComponent<Button>();
		Button button_BBucket = BBucket.GetComponent<Button>();
		Button button_BAxe = BAxe.GetComponent<Button>();
		button_BPickAxe.onClick.AddListener(PickAxeOnClick);
		button_BScissors.onClick.AddListener(ScissorsOnClick);
		button_BBucket.onClick.AddListener(BucketOnClick);
		button_BAxe.onClick.AddListener(AxeOnClick);
	}

	void PickAxeOnClick()
	{
		//Debug.Log("Hello");
		if (rShardResourceCounter.count >= PPickAxe.upgradeCost && PPickAxe.toolLevel <= 24)
        {
			
			rShardResourceCounter.SubtractFromCounter(PPickAxe.upgradeCost);
			PPickAxe.UpgradeTool();
		} 
		
	}

	void ScissorsOnClick()
	{
		//Debug.Log("Hello");
		if (rShardResourceCounter.count >= PScissors.upgradeCost && PScissors.toolLevel <= 9)
		{
			rShardResourceCounter.SubtractFromCounter(PScissors.upgradeCost);
			PScissors.UpgradeTool();
		}
		
	}

	void BucketOnClick()
	{
		//Debug.Log("Hello");
		if (rShardResourceCounter.count >= PBucket.upgradeCost && PBucket.toolLevel <= 9)
		{
			rShardResourceCounter.SubtractFromCounter(PBucket.upgradeCost);
			PBucket.UpgradeTool();
		}

	}

	void AxeOnClick()
	{
		if (rShardResourceCounter.count >= PAxe.upgradeCost && PAxe.toolLevel <= 24)
		{
			rShardResourceCounter.SubtractFromCounter(PAxe.upgradeCost);
			PAxe.UpgradeTool();
		}

	}
}
