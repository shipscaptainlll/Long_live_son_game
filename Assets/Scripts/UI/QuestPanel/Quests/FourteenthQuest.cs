using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FourteenthQuest : MonoBehaviour, IQuest
{
    public RShardResourceCounter rShardResourceCounter;
    public ManureResourceCounter ManureResourceCounter;
    public FUPCows FUPCows;
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



    //Fourteenth quest, main objective - to place bought cow at farm and collect enough manure
    void Start()
    {
        //Initialize time during which goal bar will be filled
        updateSpeedSeconds = 1;
        //Initialize ammount of mined ore
        minedAmmount = 0;
        //Initialize goal ammount
        objectiveAmmount = 50;
        //Initialize this quest id, according to List<> in quest manager
        questID = 13;
        //Initialize next quest id, according to List<> in quest manager
        nextQuestID = -14;
        //Initialize shards ammount reward for completing quest
        rewardAmmount = 25;
        //Subscribe for manure main counter, that will notify this script when character collect some manure
        FUPCows.cowWasPlacedAtFarm += CompleteFirstObjective;
        minedAmmount = ManureResourceCounter.count;
        refreshMinedAmmount();
        //Give reward counter on panel the ammount you choose from script, more conveniet to do it from one place
        refreshUI();
    }

    //Hides first objective from UI panel and shows second one
    private void CompleteFirstObjective()
    {
        StartCoroutine(hideFirstObjective());
        //Unsubscribe from gold main counter, that will notify this script when character earn some gold coins
        FUPCows.cowWasPlacedAtFarm -= CompleteFirstObjective;
        ManureResourceCounter.ManureCollected += CountCollectedManure;
    }

    //Calculate percent of collected manure to goal and starts progress bar filling process
    public void CountCollectedManure(float collectedManureAmmount)
    {
        minedAmmount += collectedManureAmmount;
            float pct = minedAmmount / objectiveAmmount;
            refreshMinedAmmount();
            StartCoroutine(changeToPct(pct));
        if (minedAmmount >= objectiveAmmount)
        { //Unsubscribe from gold main counter, that will notifies this script when character earns some gold coins
            ManureResourceCounter.ManureCollected -= CountCollectedManure;
            CompleteObjective();
        }
    }

    //Start hiding the second objective
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

    //Start showing the second objective
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
        getReward();
        //Notify QuestPanel(subscriber), that this quest was successfully completed
        questCompleted(questID, nextQuestID);
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
