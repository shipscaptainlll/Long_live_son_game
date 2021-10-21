using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargoSellManureBagSlot : MonoBehaviour
{
    public Button sellButton;
    public Text sellForCounter;
    public float sellForCount;
    public Text sellAmmountCounter;
    public int sellAmmountCount;
    public float costForUnit;
    public Slider chooseAmmountSlider;
    public MBagResourceCounter mBagResourceCounter;
    public GoldResourceCounter goldResourceCounter;

    // Start is called before the first frame update
    void Start()
    {
        sellButton.onClick.AddListener(sellFor);
        chooseAmmountSlider.onValueChanged.AddListener(delegate { calculateCost(); });
        costForUnit = 280f;
    }

    void sellFor()
    {
        mBagResourceCounter.AddToCounter(-sellAmmountCount);
        goldResourceCounter.AddToCounter(sellForCount);
        chooseAmmountSlider.value = 0;
        refreshCounters();
    }

    void calculateCost()
    {
        sellAmmountCount = (int) (mBagResourceCounter.count * chooseAmmountSlider.value);
        sellForCount = costForUnit * sellAmmountCount;
        refreshCounters();
    }

    void refreshCounters()
    {
        sellAmmountCounter.text = sellAmmountCount.ToString();
        sellForCounter.text = sellForCount.ToString();
    }
}
