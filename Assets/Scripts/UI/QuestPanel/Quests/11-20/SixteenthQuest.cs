using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SixteenthQuest : MonoBehaviour, IQuest
{
    public RShardResourceCounter rShardResourceCounter;
    public MBagResourceCounter MBagResourceCounter;
    public TreeUpgradePanel TreeUpgradePanel;
    public Text objectiveAmmountText;
    public Text objectiveAmmountText2;
    public Text objectiveAmmountCounter;
    public Text rewardCounter;
    public Image foregroundImage;
    public GameObject firstObjective;
    public GameObject secondObjective;
    public int questID;
    public int nextQuestID;
    public float collectedAmmount;
    public int objectiveAmmount;
    public int rewardAmmount;
    public int updateSpeedSeconds;

    public event Action<int, int> questCompleted = delegate { };
    //Sixteenth quest, main objective - to collect enough manure bags to grow tree to certain level
    void Start()
    {
        //Initialize time during which goal bar will be filled
        updateSpeedSeconds = 1;
        //Initialize ammount of mined ore
        collectedAmmount = 0;
        //Initialize goal ammount
        objectiveAmmount = 10;
        //Initialize this quest id, according to List<> in quest manager
        questID = 15;
        //Initialize next quest id, according to List<> in quest manager
        nextQuestID = -19;
        //Initialize shards ammount reward for completing quest
        rewardAmmount = 25;
        //Subscribe for manure bags main counter, that will notify this script when character collected a new bag of manure
        MBagResourceCounter.ManureBagsCountChanged += countEarnedManureBags;
        refreshCollectedAmmount();
        //Give reward counter on panel the ammount you choose from script, more conveniet to do it from one place
        refreshUI();
    }

    //Calculate percent of mined ores to goal and starts progress bar filling process
    public void countEarnedManureBags(int manureBagsCollectedAmmount)
    {
        
            collectedAmmount = manureBagsCollectedAmmount;
            float pct = collectedAmmount / objectiveAmmount;
            refreshCollectedAmmount();
            StartCoroutine(changeToPct(pct));
        if (collectedAmmount >= objectiveAmmount)
        { //Unsubscribe from manure bags main counter, that will notify this script when character collected a new bag of manure
            MBagResourceCounter.ManureBagsCountChanged -= countEarnedManureBags;
            StartCoroutine(hideFirstObjective());
            //CompleteObjective();
            //Subscribe for tree upgrade script, that will notify this script when character upgraded tree to a certain level
            TreeUpgradePanel.TreeLevelChanged += CompleteObjective;
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
    public void CompleteObjective(int mainTreeLevel)
    {
        if (mainTreeLevel >= 2)
        {
            //Unsubscribe from tree upgrade script, that will notify this script when character upgraded tree to a certain level
            TreeUpgradePanel.TreeLevelChanged -= CompleteObjective;
            getReward();
            //Notify QuestPanel(subscriber), that this quest was successfully completed
            questCompleted(questID, nextQuestID);
        }
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

    public void refreshCollectedAmmount()
    {
        objectiveAmmountCounter.text = collectedAmmount.ToString("0");
    }

}
