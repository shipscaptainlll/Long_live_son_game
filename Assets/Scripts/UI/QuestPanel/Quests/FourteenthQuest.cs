using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FourteenthQuest : MonoBehaviour, IQuest
{
    public RShardResourceCounter rShardResourceCounter;
    public FUPCows fUPCows; //Determine farm upgrad panel script that notifies us when cow was placed at farm
    public Text rewardCounter;
    public int questID;
    public int nextQuestID;
    public int rewardAmmount;

    public event Action<int, int> questCompleted = delegate { };



    //FourteenthQuest quest, main objective - set your cow to your farm
    void Start()
    {
        //Initialize this quest id, according to List<> in quest manager
        questID = 13;
        //Initialize next quest id, according to List<> in quest manager
        nextQuestID = -14;
        //Initialize shards ammount reward for completing quest
        rewardAmmount = 25;
        //Subscribe for farm upgrade panel script, that will notify this script when character places cow at farm
        fUPCows.cowWasPlacedAtFarm += CompleteObjective;
        //Give reward counter on panel the ammount you choose from script, more conveniet to do it from one place
        refreshUI();
    }

    //Method that calls all methods, when you successfully complete quest
    public void CompleteObjective()
    {
        //Unsubscribe from farm upgrade panel script, that notifies this script when character places cow at farm
        fUPCows.cowWasPlacedAtFarm -= CompleteObjective;
        getReward();
        //Notify QuestPanel(subscriber), that this quest was successfully completed
        questCompleted(questID, nextQuestID);
        //Unsubscribe from camera event, because quest is already completed
        
    }

    //Add shards to main shards counter as reward for completing quest
    public void getReward()
    {
        rShardResourceCounter.AddToCounter(rewardAmmount);
    }

    //refresh reward counter through script in order to not change it manually in unity
    public void refreshUI()
    {
        rewardCounter.text = rewardAmmount.ToString();
    }
}
