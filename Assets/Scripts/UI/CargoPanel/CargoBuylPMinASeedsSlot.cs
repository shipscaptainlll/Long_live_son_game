using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargoBuylPMinASeedsSlot : MonoBehaviour
{
    public Button buyButton;
    public Button stopBuyButton;
    public bool isBuying;
    public Text buyForCounter;
    public float buyForCount;
    public Text buyAmmountCounter;
    public float buyAmmountCount;
    public float buyAmmountCountAccepted;
    public float buyForCountAccepted;
    public float costForUnit;
    public Slider chooseAmmountSlider;
    public ASeedsResourceCounter aSeedsResourceCounter;
    public GoldResourceCounter goldResourceCounter;

    // Start is called before the first frame update
    void Start()
    {
        buyButton.onClick.AddListener(buyForPerMinute);
        stopBuyButton.onClick.AddListener(stopBuying);
        chooseAmmountSlider.onValueChanged.AddListener(delegate { calculateCost(); });
        costForUnit = 0.051f;
        isBuying = false;
    }

    void buyForPerMinute()
    {
        isBuying = true;
        buyAmmountCountAccepted = buyAmmountCount;
        buyForCountAccepted = buyForCount;
        StartCoroutine(buyEachSecond());
        aSeedsResourceCounter.AddToMineSpeedCounter(buyAmmountCountAccepted);
        goldResourceCounter.AddToMineSpeedCounter(-buyForCountAccepted);
        chooseAmmountSlider.interactable = false;
        refreshCounters();
        changeButtons();
    }
    public void stopBuying()
    {
        aSeedsResourceCounter.AddToMineSpeedCounter(-buyAmmountCountAccepted);
        goldResourceCounter.AddToMineSpeedCounter(buyForCountAccepted);
        isBuying = false;
        chooseAmmountSlider.interactable = true;
        chooseAmmountSlider.value = 0;
        changeButtons();
    }

    public void changeButtons()
    {
        if (buyButton.gameObject.active == true)
        {
            buyButton.gameObject.SetActive(false);
            stopBuyButton.gameObject.SetActive(true);
        } else if (stopBuyButton.gameObject.active == true)
        {
            buyButton.gameObject.SetActive(true);
            stopBuyButton.gameObject.SetActive(false);
        }
    }

    

    public IEnumerator buyEachSecond()
    {
        if (isBuying == false)
        {
            yield return null;
        }
        while(goldResourceCounter.count >= buyForCountAccepted && isBuying == true)
        {
            aSeedsResourceCounter.AddToCounter(buyAmmountCountAccepted);
            goldResourceCounter.AddToCounter(-buyForCountAccepted);
            yield return new WaitForSeconds(1);
        }
        if (goldResourceCounter.count < buyForCountAccepted && isBuying == true) { stopBuying(); }
    }

    void calculateCost()
    {
        if (isBuying == false)
        {
            float preBuyAmmountCount = Mathf.Round(goldResourceCounter.mineSpeedCount * chooseAmmountSlider.value * 2 * 10 / costForUnit);
            buyAmmountCount = preBuyAmmountCount * 0.1f;
            float preBuyForCount = Mathf.Round(costForUnit * buyAmmountCount * 10);
            buyForCount = preBuyForCount * 0.1f;
            refreshCounters();
        }
        
    }

    void refreshCounters()
    {
        buyAmmountCounter.text = buyAmmountCount.ToString();
        float nBuyForCount = -buyForCount;
        buyForCounter.text = nBuyForCount.ToString();
        
    }
}
