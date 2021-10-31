using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EighteenthQuest : MonoBehaviour, IQuest
{
    public RShardResourceCounter rShardResourceCounter;
    public CSeedsResourceCounter CSeedsResourceCounter;
    [SerializeField] InteractionController InteractionController;
    [SerializeField] SeedsToolsPanel SeedsToolsPanel;
    [SerializeField] CarrotResourceCounter CarrotResourceCounter;
    public Text objectiveAmmountText;
    public Text objectiveAmmountCounter;
    public Text rewardCounter;
    public Image foregroundImage;
    public GameObject FirstObjective;
    public GameObject SecondObjective;
    public GameObject ThirdObjective;
    public GameObject FourthObjective;
    public GameObject FifthObjective;
    public int questID;
    public int nextQuestID;
    public float collectedAmmount;
    public int objectiveAmmount;
    public int rewardAmmount;
    public int updateSpeedSeconds;
    float collectedCarrotsAmmount;
    float collectedCarrotsGoal;

    public event Action<int, int> questCompleted = delegate { };
    //Eightteenth quest, main objective - to buy carrot seeds, use bucket and scissors on soil, wait until carrots grow
    void Start()
    {
        updateSpeedSeconds = 1;
        collectedAmmount = 0;
        objectiveAmmount = 10;
        questID = 17;
        nextQuestID = -18;
        rewardAmmount = 25;
        collectedCarrotsAmmount = 0;
        collectedCarrotsGoal = 10;
        CSeedsResourceCounter.cSeedsCollected += CompleteFirstObjective;
        RefreshCollectedAmmount();
        refreshUI();
    }

    private void CompleteFirstObjective()
    {
        CSeedsResourceCounter.cSeedsCollected -= CompleteFirstObjective;
        StartCoroutine(HideObjective(FirstObjective, SecondObjective));
        SeedsToolsPanel.CarrotSeedsChosen += CompleteSecondObjective;
    }

    private void CompleteSecondObjective()
    {
        SeedsToolsPanel.CarrotSeedsChosen -= CompleteSecondObjective;
        StartCoroutine(HideObjective(SecondObjective, ThirdObjective));
        InteractionController.BucketAdded += CompleteThirdObjective;
    }

    private void CompleteThirdObjective()
    {
        InteractionController.BucketAdded -= CompleteThirdObjective;
        StartCoroutine(HideObjective(ThirdObjective, FourthObjective));
        InteractionController.ScissorsAdded += CompleteFourthObjective;
    }

    private void CompleteFourthObjective()
    {
        InteractionController.ScissorsAdded -= CompleteFourthObjective;
        StartCoroutine(HideObjective(FourthObjective, FifthObjective));
        CarrotResourceCounter.CollectedSomeCarrots += CountCollectedCarrots;
    }

    public void CountCollectedCarrots(float collectedCarrots)
    {
        collectedAmmount += collectedCarrots;
        float pct = collectedAmmount / collectedCarrotsGoal;
        RefreshCollectedAmmount();
        StartCoroutine(changeToPct(pct));
        if (collectedAmmount >= collectedCarrotsGoal)
        {
            CompleteLastObjective();
        }
    }

    public void CompleteLastObjective()
    {
        CarrotResourceCounter.CollectedSomeCarrots -= CountCollectedCarrots;
        getReward();
        questCompleted(questID, nextQuestID);
    }

    IEnumerator HideObjective(GameObject hideObjective, GameObject showObjective)
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

    public void getReward()
    {
        rShardResourceCounter.AddToCounter(rewardAmmount);
    }

    public void refreshUI()
    {
        objectiveAmmountText.text = objectiveAmmount.ToString();
        rewardCounter.text = rewardAmmount.ToString();
    }

    public void RefreshCollectedAmmount()
    {
        objectiveAmmountCounter.text = collectedAmmount.ToString("0");
    }

}
