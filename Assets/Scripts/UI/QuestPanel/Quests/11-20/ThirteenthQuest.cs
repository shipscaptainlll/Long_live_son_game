using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirteenthQuest : MonoBehaviour, IQuest
{
    public RShardResourceCounter rShardResourceCounter;
    public GoldResourceCounter goldResourceCounter;
    public CowResourceCounter cowResourceCounter;
    public Text objectiveAmmountText;
    public Text objectiveAmmountText2;
    public Text objectiveAmmountCounter;
    public Text rewardCounter;
    public Image foregroundImage;
    public GameObject firstObjective;
    public GameObject secondObjective;
    public int questID;
    public int nextQuestID;
    public float minedAmmount;
    public int objectiveAmmount;
    public int rewardAmmount;
    public int updateSpeedSeconds;

    public event Action<int, int> questCompleted = delegate { };



    //Thirteenth quest, main objective - to collect enough gold to buy a cow and buy it
    void Start()
    {
        //Initialize time during which goal bar will be filled
        updateSpeedSeconds = 1;
        //Initialize ammount of mined ore
        minedAmmount = 0;
        //Initialize goal ammount
        objectiveAmmount = 5000;
        //Initialize this quest id, according to List<> in quest manager
        questID = 12;
        //Initialize next quest id, according to List<> in quest manager
        nextQuestID = 13;
        //Initialize shards ammount reward for completing quest
        rewardAmmount = 25;
        //Subscribe for gold main counter, that will notify this script when character earn some gold coins
        goldResourceCounter.goldEarned += countEarnedGold;
        minedAmmount = goldResourceCounter.count;
        refreshMinedAmmount();
        //Give reward counter on panel the ammount you choose from script, more conveniet to do it from one place
        refreshUI();
    }

    //Calculate percent of mined ores to goal and starts progress bar filling process
    public void countEarnedGold(float rocksMinedAmmount)
    {
        
            minedAmmount += rocksMinedAmmount;
            float pct = minedAmmount / objectiveAmmount;
            refreshMinedAmmount();
            StartCoroutine(changeToPct(pct));
        if (minedAmmount >= objectiveAmmount)
        { //Unsubscribe for gold main counter, that will notifies this script when character earns some gold coins
            goldResourceCounter.goldEarned -= countEarnedGold;
            StartCoroutine(hideFirstObjective());
            //CompleteObjective();
            //Subscribe for cow resource counter, that will notify this script when character bought first cow
            cowResourceCounter.cowBought += CompleteObjective;
        }
        
    }

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
    //Start filling the progress bar
    public IEnumerator changeToPct(float pct)
    {
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0f;
        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }

        foregroundImage.fillAmount = pct;
    }

    //Method that calls all methods, when you successfully complete quest
    public void CompleteObjective()
    {
        //Unsbscribe for cow resource counter, that will notify this script when character bought first cow
        cowResourceCounter.cowBought -= CompleteObjective;
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
        objectiveAmmountText.text = objectiveAmmount.ToString();
        objectiveAmmountText2.text = objectiveAmmount.ToString();
        rewardCounter.text = rewardAmmount.ToString();
    }

    public void refreshMinedAmmount()
    {
        objectiveAmmountCounter.text = minedAmmount.ToString("0");
    }

}
