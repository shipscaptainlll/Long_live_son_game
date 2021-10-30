using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeventeenthQuest : MonoBehaviour, IQuest
{
    public RShardResourceCounter rShardResourceCounter; //Rock shards main counter script
    public FUPGroundTile FUPGroundTile; //Script that will notify this quest script when first groun pile was created
    public Text rewardCounter; //Text on UI panel that shows reward
    public int questID; //ID of the quest needed for quests manager
    public int nextQuestID; //next quest ID of the quest needed for quests manager
    public int rewardAmmount; //Ammount of rock shards you will get as a reward for accomplishing quest

    //Event that notifies main quests controller that quest was completed
    public event Action<int, int> questCompleted = delegate { };
    //SeventeenthQuest quest, main objective - build a pile of soil
    void Start()
    {
        //Initialize this quest id, according to List<> in quest manager
        questID = 16;
        //Initialize next quest id, according to List<> in quest manager
        nextQuestID = 17;
        //Initialize shards ammount reward for completing quest
        rewardAmmount = 15;
        //Subscribe for script that notifies this script when first ground tile is created
        FUPGroundTile.GroundTileAdded += CompleteObjective;
        //Give reward counter on panel the ammount you choose from script, more conveniet to do it from one place
        refreshRewardCounter();
    }

    //Method that calls all methods, when you successfully complete quest
    public void CompleteObjective()
    {
        getReward();
        //Notify QuestPanel(subscriber), that this quest was successfully completed
        questCompleted(questID, nextQuestID);
        //Unsubscribe from script that notifies this script when first ground tile is created
        FUPGroundTile.GroundTileAdded -= CompleteObjective;
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
