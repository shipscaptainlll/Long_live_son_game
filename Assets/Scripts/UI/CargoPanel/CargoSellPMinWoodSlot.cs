using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargoSellPMinWoodSlot : MonoBehaviour
{
    public Button sellButton;
    public Button stopSellButton;
    public bool isSelling;
    public Text sellForCounter;
    public float sellForCount;
    public Text sellAmmountCounter;
    public float sellAmmountCount;
    public float sellAmmountCountAccepted;
    public float sellForCountAccepted;
    public float costForUnit;
    public Slider chooseAmmountSlider;
    public LogResourceCounter logResourceCounter;
    public GoldResourceCounter goldResourceCounter;

    // Start is called before the first frame update
    void Start()
    {
        sellButton.onClick.AddListener(sellForPerMinute);
        stopSellButton.onClick.AddListener(stopSelling);
        chooseAmmountSlider.onValueChanged.AddListener(delegate { calculateCost(); });
        costForUnit = 0.12f;
        isSelling = false;
    }

    void sellForPerMinute()
    {
        isSelling = true;
        sellAmmountCountAccepted = sellAmmountCount;
        sellForCountAccepted = sellForCount;
        StartCoroutine(sellEachSecond());
        logResourceCounter.AddToMineSpeedCounter(-sellAmmountCountAccepted);
        goldResourceCounter.AddToMineSpeedCounter(sellForCountAccepted);
        chooseAmmountSlider.interactable = false;
        refreshCounters();
        changeButtons();
    }
    public void stopSelling()
    {
        Debug.Log(sellAmmountCountAccepted);
        Debug.Log(sellForCountAccepted);
        logResourceCounter.AddToMineSpeedCounter(sellAmmountCountAccepted);
        goldResourceCounter.AddToMineSpeedCounter(-sellForCountAccepted);
        isSelling = false;
        chooseAmmountSlider.interactable = true;
        chooseAmmountSlider.value = 0;
        changeButtons();
    }

    public void changeButtons()
    {
        if (sellButton.gameObject.active == true)
        {
            sellButton.gameObject.SetActive(false);
            stopSellButton.gameObject.SetActive(true);
        } else if (stopSellButton.gameObject.active == true)
        {
            sellButton.gameObject.SetActive(true);
            stopSellButton.gameObject.SetActive(false);
        }
    }

    

    public IEnumerator sellEachSecond()
    {
        if (isSelling == false)
        {
            yield return null;
        }
        while(logResourceCounter.count >= sellAmmountCountAccepted && isSelling == true)
        {
            logResourceCounter.AddToCounter(-sellAmmountCountAccepted);
            goldResourceCounter.AddToCounter(sellForCountAccepted);
            yield return new WaitForSeconds(1);
        }
        if (logResourceCounter.count < sellAmmountCountAccepted && isSelling == true) { stopSelling(); }
    }

    void calculateCost()
    {
        if (isSelling == false)
        {
            float preSellAmmountCount = Mathf.Round(logResourceCounter.mineSpeedCount * chooseAmmountSlider.value * 2 * 10);
            sellAmmountCount = preSellAmmountCount * 0.1f;
            float preSellForCount = Mathf.Round(costForUnit * sellAmmountCount * 10);
            sellForCount = preSellForCount * 0.1f;
            refreshCounters();
        }
        
    }

    void refreshCounters()
    {
        float nSellAmmountCount = -sellAmmountCount;
        sellAmmountCounter.text = nSellAmmountCount.ToString();
        sellForCounter.text = sellForCount.ToString();
        
    }
}
