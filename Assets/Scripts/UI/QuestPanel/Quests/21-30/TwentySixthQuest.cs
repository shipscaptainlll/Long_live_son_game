using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwentySixthQuest : MonoBehaviour, IQuest
{
    [SerializeField] RShardResourceCounter RShardResourceCounter;
    [SerializeField] QuestTriggerTwentySixth QuestTriggerTwentySixth;
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
        questID = 25;
        nextQuestID = 27;
        rewardAmmount = 15;
        refreshRewardCounter();
        QuestTriggerTwentySixth.CharacterReachedRectangle += CompleteLastObjective;
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
