using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwentiethQuest : MonoBehaviour, IQuest
{
    [SerializeField] MouseRotation MouseRotation;
    [SerializeField] LogResourceCounter LogResourceCounter;
    [SerializeField] RShardResourceCounter rShardResourceCounter;
    [SerializeField] Text rewardCounter;
    [SerializeField] Text objectiveAmmountText;
    [SerializeField] Text objectiveAmmountCounter;
    [SerializeField] Image foregroundImage;
    [SerializeField] GameObject FirstObjective;
    [SerializeField] GameObject SecondObjective;
    int questID;
    int nextQuestID;
    int rewardAmmount;
    int updateSpeedSeconds;
    float collectedAmmount;
    float collectedLogsGoal;

    public event Action<int, int> questCompleted = delegate { };

    //TwentiethQuest, main objective - to find some trees on second floor and collect 100
    void Start()
    {
        questID = 19;
        nextQuestID = 20;
        rewardAmmount = 15;
        updateSpeedSeconds = 1;
        collectedLogsGoal = 100;
        MouseRotation.TreeDetected += CompleteFirstObjective;
        refreshRewardCounter();
    }

    private void CompleteFirstObjective()
    {
        MouseRotation.TreeDetected -= CompleteFirstObjective;
        StartCoroutine(ChangeObjectivesUI(FirstObjective, SecondObjective));
        LogResourceCounter.LogCollected += CountCollectedLogs;
    }

    void CompleteLastObjective()
    {
        getReward();
        questCompleted(questID, nextQuestID);
        
    }

    public void CountCollectedLogs(int collectedLogs)
    {
        collectedAmmount += collectedLogs;
        float pct = collectedAmmount / collectedLogsGoal;
        RefreshCollectedAmmount();
        StartCoroutine(changeToPct(pct));
        if (collectedAmmount >= collectedLogsGoal)
        {
            CompleteLastObjective();
        }
    }

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

    IEnumerator ChangeObjectivesUI(GameObject hideObjective, GameObject showObjective)
    {
        float elapsed = 0f;
        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            hideObjective.GetComponent<CanvasGroup>().alpha -= 0.1f;
            yield return null;
        }
        hideObjective.GetComponent<CanvasGroup>().alpha = 0;
        StartCoroutine(ShowObjective(showObjective));
    }

    IEnumerator ShowObjective(GameObject showObjective)
    {
        float elapsed = 0f;
        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            showObjective.GetComponent<CanvasGroup>().alpha += 0.1f;
            yield return null;
        }
        showObjective.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void getReward()
    {
        rShardResourceCounter.AddToCounter(rewardAmmount);
    }

    public void RefreshCollectedAmmount()
    {
        objectiveAmmountCounter.text = collectedAmmount.ToString("0");
    }

    void refreshRewardCounter()
    {
        rewardCounter.text = rewardAmmount.ToString();
    }

}
