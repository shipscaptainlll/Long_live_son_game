using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwentyFourthQuest : MonoBehaviour, IQuest
{
    [SerializeField] RShardResourceCounter RShardResourceCounter;
    [SerializeField] QuestTriggerTwentyFourth QuestTriggerTwentyFourth;
    [SerializeField] Text RewardCounter;
    int questID;
    int nextQuestID;
    int additionalQuestID;
    int rewardAmmount;
    bool _isFinished;

    public event Action<int, int> questCompleted = delegate { };

    //TwentyFoursthQuest, main objective - to reach third floor
    void Start()
    {
        _isFinished = false;
        questID = 23;
        nextQuestID = 25;
        additionalQuestID = 26;
        rewardAmmount = 15;
        refreshRewardCounter();
        QuestTriggerTwentyFourth.CharacterReachedRectangle += CompleteLastObjective;
    }


    void CompleteLastObjective()
    {
        if (_isFinished == false)
        {
            _isFinished = true;
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

    private IEnumerator WaitForFewSeconds()
    {
        yield return new WaitForSeconds(0.5f);
        //Send notification to quest panel, that additional quest should be opened, negative number blocks closing of any quests, since this quest will already be closed
        questCompleted(-questID, additionalQuestID);
        gameObject.SetActive(false);
    }
}
