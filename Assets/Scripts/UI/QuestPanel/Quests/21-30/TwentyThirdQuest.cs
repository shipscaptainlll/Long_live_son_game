using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwentyThirdQuest : MonoBehaviour, IQuest
{
    [SerializeField] RShardResourceCounter RShardResourceCounter;
    [SerializeField] Transform Farms;
    [SerializeField] Text RewardCounter;
    bool isFinished;
    int questID;
    int nextQuestID;
    int additionalQuestID;
    int rewardAmmount;

    public event Action<int, int> questCompleted = delegate { };

    //TwentyThirdQuest, main objective - to hire some farmers there
    void Start()
    {
        isFinished = false;
        questID = 22;
        nextQuestID = 23;
        additionalQuestID = 24;
        rewardAmmount = 15;
        refreshRewardCounter();
        for (int i = 0; i < 7; i++)
        {
            Farms.GetChild(i).Find("Borders").Find("Farmer").GetComponent<FarmsPeople>().SomeoneIsHired += CompleteLastObjective;
        }
    }

    void CompleteLastObjective()
    {
        if (isFinished == false)
        {
            isFinished = true;
            getReward();
            questCompleted(questID, nextQuestID);
            StartCoroutine(WaitForFewSeconds());
        }
    }
    
    public void getReward()
    {
        RShardResourceCounter.AddToCounter(rewardAmmount);
    }

    void refreshRewardCounter()
    {
        RewardCounter.text = rewardAmmount.ToString();
    }

    void UnsubscribeFromQuest()
    {
        for (int i = 0; i < 7; i++)
        {
            Farms.GetChild(i).Find("Borders").Find("Farmer").GetComponent<FarmsPeople>().SomeoneIsHired -= CompleteLastObjective;
        }
    }

    private IEnumerator WaitForFewSeconds()
    {
        yield return new WaitForSeconds(0.5f);
        //Send notification to quest panel, that additional quest should be opened, negative number blocks closing of any quests, since this quest will already be closed
        questCompleted(-questID, additionalQuestID);
        gameObject.SetActive(false);
        UnsubscribeFromQuest();
    }
}
