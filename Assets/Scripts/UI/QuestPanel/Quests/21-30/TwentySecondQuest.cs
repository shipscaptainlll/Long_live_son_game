using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwentySecondQuest : MonoBehaviour, IQuest
{
    [SerializeField] InteractionController InteractionController;
    [SerializeField] RShardResourceCounter RShardResourceCounter;
    [SerializeField] Text RewardCounter;
    [SerializeField] GameObject FirstObjective;
    [SerializeField] Transform Farms;
    int questID;
    int additionalQuestID;
    int nextQuestID;
    int rewardAmmount;

    public event Action<int, int> questCompleted = delegate { };

    //TwentySecondQuest, main objective - to build a farm in a hole
    void Start()
    {
        foreach (Transform Farm in Farms)
        {
            Farm.GetComponent<IFarmUI>().FarmIsCreated += CompleteLastObjective;
        }
        
        questID = 21;
        nextQuestID = 22;
        rewardAmmount = 15;
        refreshRewardCounter();
    }

    void CompleteLastObjective()
    {
        getReward();
        questCompleted(questID, nextQuestID);
        UnsubscribeFromFarmCreator();
    }
    
    public void getReward()
    {
        RShardResourceCounter.AddToCounter(rewardAmmount);
    }

    void refreshRewardCounter()
    {
        RewardCounter.text = rewardAmmount.ToString();
    }

    void UnsubscribeFromFarmCreator()
    {
        foreach (Transform Farm in Farms)
        {
            Farm.GetComponent<IFarmUI>().FarmIsCreated -= CompleteLastObjective;
        }
    }

}
