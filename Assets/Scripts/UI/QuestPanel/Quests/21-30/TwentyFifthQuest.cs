using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwentyFifthQuest : MonoBehaviour, IQuest
{
    [SerializeField] RShardResourceCounter RShardResourceCounter;
    [SerializeField] Text RewardCounter;
    [SerializeField] GameObject FirstObjective;
    [SerializeField] Transform Farms;
    int questID;
    int nextQuestID;
    int rewardAmmount;
    int _counter;
    int _questLimit;

    public event Action<int, int> questCompleted = delegate { };

    //TwentyFifthQuest, main objective - to build at least 3 farms
    void Start()
    {
        foreach (Transform Farm in Farms)
        {
            Farm.GetComponent<IFarmUI>().FarmIsCreated += AddToCounter;
        }
        _counter = 0;
        _questLimit = 3;
        questID = 24;
        nextQuestID = -1;
        rewardAmmount = 15;
        refreshRewardCounter();
    }

    void AddToCounter()
    {
        _counter++;
        if (_counter == _questLimit)
        {
            CompleteLastObjective();
        }
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
