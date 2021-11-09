using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwentyFifthQuest : MonoBehaviour, IQuest
{
    [SerializeField] RShardResourceCounter RShardResourceCounter;
    [SerializeField] Text RewardCounter;
    int questID;
    int nextQuestID;
    int rewardAmmount;

    public event Action<int, int> questCompleted = delegate { };

    //TwentyFifthQuest, main objective - to build at least 3 farms
    void Start()
    {
        questID = 24;
        nextQuestID = -25;
        rewardAmmount = 15;
        refreshRewardCounter();
    }

    void CompleteLastObjective()
    {
        getReward();
        questCompleted(questID, nextQuestID);
        
    }
    
    public void getReward()
    {
        RShardResourceCounter.AddToCounter(rewardAmmount);
    }

    void refreshRewardCounter()
    {
        RewardCounter.text = rewardAmmount.ToString();
    }
}
