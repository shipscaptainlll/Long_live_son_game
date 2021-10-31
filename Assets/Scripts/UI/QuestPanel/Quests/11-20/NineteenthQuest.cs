using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NineteenthQuest : MonoBehaviour, IQuest
{
    [SerializeField] RShardResourceCounter rShardResourceCounter;
    [SerializeField] AppleResourceCounter AppleResourceCounter;
    [SerializeField] Text rewardCounter;
    [SerializeField] int rewardAmmount;
    int questID; 
    int nextQuestID;

    public event Action<int, int> questCompleted = delegate { };
    //NineteenthQuest quest, main objective - grow one apple tree
    void Start()
    {
        questID = 18;
        nextQuestID = -19;
        rewardAmmount = 15;
        AppleResourceCounter.ApplesCollected += CompleteObjective;
        refreshRewardCounter();
    }

    //Method that calls all methods, when you successfully complete quest
    public void CompleteObjective()
    {
        getReward();
        questCompleted(questID, nextQuestID);
    }

    //Add shards to main shards counter as reward for completing quest
    public void getReward()
    {
        rShardResourceCounter.AddToCounter(rewardAmmount);
    }

    //refresh reward counter through script in order to not change it manually in unity
    public void refreshRewardCounter()
    {
        rewardCounter.text = rewardAmmount.ToString();
    }

}
