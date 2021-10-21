using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargoSellWoodSlot : MonoBehaviour
{
    public Button sellButton;
    public Text sellForCounter;
    public float sellForCount;
    public Text sellAmmountCounter;
    public int sellAmmountCount;
    public float costForUnit;
    public Slider chooseAmmountSlider;
    public LogResourceCounter woodResourceCounter;
    public GoldResourceCounter goldResourceCounter;

    // Start is called before the first frame update
    void Start()
    {
        sellButton.onClick.AddListener(sellFor);
        chooseAmmountSlider.onValueChanged.AddListener(delegate { calculateCost(); });
        costForUnit = 0.12f;
    }

    void sellFor()
    {
        woodResourceCounter.AddToCounter(-sellAmmountCount);
        goldResourceCounter.AddToCounter(sellForCount);
        chooseAmmountSlider.value = 0;
        refreshCounters();
    }

    void calculateCost()
    {
        sellAmmountCount = (int) (woodResourceCounter.count * chooseAmmountSlider.value);
        sellForCount = costForUnit * sellAmmountCount;
        refreshCounters();
    }

    void refreshCounters()
    {
        sellAmmountCounter.text = sellAmmountCount.ToString();
        sellForCounter.text = sellForCount.ToString();
    }
}
