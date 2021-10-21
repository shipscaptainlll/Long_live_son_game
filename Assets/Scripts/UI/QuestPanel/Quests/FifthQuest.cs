using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FourthQuest : MonoBehaviour, IQuest
{
    public RShardResourceCounter rShardResourceCounter;
    public InteractionController interactionController;
    public Text rewardCounter;
    public int questID;
    public int nextQuestID;
    public int rewardAmmount;

    public event Action<int, int> questCompleted = delegate { };



    //Fourth quest, main objective - to start rock ore auto mining
    void Start()
    {
        //Initialize this quest id, according to List<> in quest manager
        questID = 3;
        //Initialize next quest id, according to List<> in quest manager
        nextQuestID = -4;
        //Initialize shards ammount reward for completing quest
        rewardAmmount = 15;
        //Subscribe for interaction controller, that will notify this script when character start rock ore auto mining
        interactionController.startedRockoreAutomining += CompleteObjective;
        //Give reward counter on panel the ammount you choose from script, more conveniet to do it from one place
        refreshRewardCounter();
    }

    //Method that calls all methods, when you successfully complete quest
    public void CompleteObjective()
    {
        getReward();
        //Notify QuestPanel(subscriber), that this quest was successfully completed
        questCompleted(questID, nextQuestID);
        //Unsubscribe for interaction controller, that will notify this script when character start rock ore auto mining
        interactionController.startedRockoreAutomining -= CompleteObjective;
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
