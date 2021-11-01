using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwentyFirstQuest : MonoBehaviour, IQuest
{
    [SerializeField] InteractionController InteractionController;
    [SerializeField] RShardResourceCounter RShardResourceCounter;
    [SerializeField] Text RewardCounter;
    [SerializeField] GameObject FirstObjective;
    int questID;
    int nextQuestID;
    int rewardAmmount;

    public event Action<int, int> questCompleted = delegate { };

    //TwentyFirstQuest, main objective - to use boulder blast on one of the walls
    void Start()
    {
        questID = 20;
        nextQuestID = -21;
        rewardAmmount = 15;
        InteractionController.WallDestroyed += CompleteLastObjective;
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
