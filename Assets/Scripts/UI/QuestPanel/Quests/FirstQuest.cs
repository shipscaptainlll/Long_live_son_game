using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstQuest : MonoBehaviour, IQuest
{
    public MouseRotation mouseRotation;
    public RShardResourceCounter rShardResourceCounter;
    public Text rewardCounter;
    public int questID;
    public int nextQuestID;
    public int rewardAmmount;

    public event Action<int, int> questCompleted = delegate { };



    //First quest, main objective - to find ore
    void Start()
    {
        //Initialize this quest id, according to List<> in quest manager
        questID = 0;
        //Initialize next quest id, according to List<> in quest manager
        nextQuestID = 1;
        //Initialize shards ammount reward for completing quest
        rewardAmmount = 15;
        //Subscribe for camera event, that will notify this script when character sees rock ore
        mouseRotation.rockOreDetected += CompleteObjective;
        //Give reward counter on panel the ammount you choose from script, more conveniet to do it from one place
        refreshRewardCounter();
    }

    //Method that calls all methods, when you successfully complete quest
    public void CompleteObjective()
    {
        getReward();
        //Notify QuestPanel(subscriber), that this quest was successfully completed
        questCompleted(questID, nextQuestID);
        //Unsubscribe from camera event, because quest is already completed
        mouseRotation.rockOreDetected -= CompleteObjective;
    }

    //Add shards to main shards counter as reward for completing quest
    public void getReward()
    {
        rShardResourceCounter.AddToCounter(rewardAmmount);
    }

    //refresh reward counter through script in order to not change it manually in unity
    public void refreshRewardCounter()
    {
        rewardCounter.text = rewardAmmount.ToString();
    }

}
