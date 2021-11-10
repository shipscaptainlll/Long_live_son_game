using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwentyNinthQuest : MonoBehaviour, IQuest
{
    [SerializeField] RShardResourceCounter RShardResourceCounter;
    [SerializeField] QuestTriggerTwentyNinth QuestTriggerTwentyNinth;
    [SerializeField] Text RewardCounter;
    int questID;
    int nextQuestID;
    int rewardAmmount;
    bool _isFinished;

    public event Action<int, int> questCompleted = delegate { };

    //TwentyFoursthQuest, main objective - to return home
    void Start()
    {
        _isFinished = false;
        questID = 28;
        nextQuestID = -1;
        rewardAmmount = 15;
        refreshRewardCounter();
        QuestTriggerTwentyNinth.CharacterReachedRectangle += CompleteLastObjective;
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
