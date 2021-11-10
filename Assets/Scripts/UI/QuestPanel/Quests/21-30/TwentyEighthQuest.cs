using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwentyEighthQuest : MonoBehaviour, IQuest
{
    [SerializeField] RShardResourceCounter RShardResourceCounter;
    [SerializeField] QuestTriggerTwentyEighth QuestTriggerTwentyEighth;
    [SerializeField] Text RewardCounter;
    int questID;
    int nextQuestID;
    int rewardAmmount;
    bool _isFinished;

    public event Action<int, int> questCompleted = delegate { };

    //TwentyFoursthQuest, main objective - to reach mages tower
    void Start()
    {
        _isFinished = false;
        questID = 27;
        nextQuestID = 28;
        rewardAmmount = 15;
        refreshRewardCounter();
        QuestTriggerTwentyEighth.CharacterReachedSphere += CompleteLastObjective;
    }


    void CompleteLastObjective()
    {
        if (_isFinished == false)
        {
            _isFinished = true;
            getReward();
            questCompleted(questID, nextQuestID);
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
}
