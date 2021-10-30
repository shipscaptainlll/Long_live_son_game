using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FifteenthQuest : MonoBehaviour, IQuest
{
    public RShardResourceCounter rShardResourceCounter; //Rock shards main counter script
    public Merchant15QuestTrigger Merchant15QuestTrigger; //Script that send notification, when character reached merchang
    public UBManureBag UBManureBag;
    public Text rewardCounter;
    public Image foregroundImage;
    public GameObject firstObjective;
    public GameObject secondObjective;
    public int questID;
    public int nextQuestID;
    public int additionalQuestID;
    public float minedAmmount;
    public int objectiveAmmount;
    public int rewardAmmount;
    public int updateSpeedSeconds;

    public event Action<int, int> questCompleted = delegate { };



    //Fifteenth quest, main objective - is to create a manure bag and than deliver it to merchant
    void Start()
    {
        //Initialize this quest id, according to List<> in quest manager
        questID = 14;
        //Initialize next quest id, according to List<> in quest manager
        nextQuestID = 15;
        //Initialize next additional quest id, according to List<> in quest manager
        additionalQuestID = 16;
        //Initialize shards ammount reward for completing quest
        rewardAmmount = 25;
        //Subscribe for UBManurebag, that will notify this script when character converts manure to manure bag
        UBManureBag.ManureBagCreated += CompleteFirstObjective;
        //Give reward counter on panel the ammount you choose from script, more conveniet to do it from one place
        refreshUI();
    }

    
    //Method that calls hiding of first objective and showing of second on UI panel
    public void CompleteFirstObjective()
    {
        //Unsubscribe from UBManurebag, that will notifies this script when character converts manure to manure bag
        UBManureBag.ManureBagCreated -= CompleteFirstObjective;
        StartCoroutine(hideFirstObjective());
        //Subscribe for script, that will notify this script when character is near merchant
        Merchant15QuestTrigger.CharacterReachedSphere += CompleteLastObjective;
    }

    //Method that gradually hides first objective text from UI panel and then call method that show second objective
    public IEnumerator hideFirstObjective()
    {
        float elapsed = 0f;
        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            firstObjective.GetComponent<CanvasGroup>().alpha -= 0.1f;
            yield return null;
        }
        firstObjective.GetComponent<CanvasGroup>().alpha = 0;
        StartCoroutine(showSecondObjective());
    }

    //Method that gradually shows second objetive
    public IEnumerator showSecondObjective()
    {
        float elapsed = 0f;
        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            secondObjective.GetComponent<CanvasGroup>().alpha += 0.1f;
            yield return null;
        }
        secondObjective.GetComponent<CanvasGroup>().alpha = 1;
    }

    //Method that calls all methods, when you successfully complete quest
    public void CompleteLastObjective()
    {
        //Unsubscribe ffrom script, that notifies this script when character is near merchant
        Merchant15QuestTrigger.CharacterReachedSphere -= CompleteLastObjective;
        getReward();
        //Notify QuestPanel(subscriber), that this quest was successfully completed
        questCompleted(questID, nextQuestID);
        StartCoroutine(WaitForFewSeconds());
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

    //Slows in order to show new quest one by one
    private IEnumerator WaitForFewSeconds()
    {
        yield return new WaitForSeconds(0.5f);
        //Send notification to quest panel, that additional quest should be opened, negative number blocks closing of any quests, since this quest will already be closed
        questCompleted(-questID, additionalQuestID);
        gameObject.SetActive(false);
    }
}
