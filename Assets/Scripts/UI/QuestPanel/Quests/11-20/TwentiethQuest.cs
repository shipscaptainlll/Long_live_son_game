using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwentiethQuest : MonoBehaviour, IQuest
{
    [SerializeField] MouseRotation MouseRotation;
    [SerializeField] RShardResourceCounter rShardResourceCounter;
    [SerializeField] Text rewardCounter;
    [SerializeField] int rewardAmmount;
    int questID;
    int nextQuestID;

    public event Action<int, int> questCompleted = delegate { };

    //TwentiethQuest, main objective - to find some trees on second floor
    void Start()
    {
        questID = 19;
        nextQuestID = -20;
        rewardAmmount = 15;
        MouseRotation.TreeDetected += CompleteObjective;
        refreshRewardCounter();
    }

    void CompleteObjective()
    {
        getReward();
        questCompleted(questID, nextQuestID);
        MouseRotation.TreeDetected -= CompleteObjective;
    }

    public void getReward()
    {
        rShardResourceCounter.AddToCounter(rewardAmmount);
    }

    void refreshRewardCounter()
    {
        rewardCounter.text = rewardAmmount.ToString();
    }

}
