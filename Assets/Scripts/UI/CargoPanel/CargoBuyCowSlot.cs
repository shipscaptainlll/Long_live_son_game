using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargoBuyCowSlot : MonoBehaviour
{
    public Button buyButton;
    public Text buyForCounter;
    public float buyForCount;
    public Text buyAmmountCounter;
    public int buyAmmountCount;
    public float costForUnit;
    public Slider chooseAmmountSlider;
    public CowMainResourceCounter cowResourceCounter;
    public GoldResourceCounter goldResourceCounter;

    // Start is called before the first frame update
    void Start()
    {
        buyButton.onClick.AddListener(buyFor);
        chooseAmmountSlider.onValueChanged.AddListener(delegate { calculateCost(); });
        costForUnit = 5000;
    }

    void buyFor()
    {
        cowResourceCounter.AddToCounter(buyAmmountCount);
        goldResourceCounter.AddToCounter(-buyForCount);
        chooseAmmountSlider.value = 0;
        refreshCounters();
    }

    void calculateCost()
    {
        buyAmmountCount = (int) (goldResourceCounter.count / costForUnit * chooseAmmountSlider.value);
        buyForCount = costForUnit * buyAmmountCount;
        refreshCounters();
    }

    void refreshCounters()
    {
        buyAmmountCounter.text = buyAmmountCount.ToString();
        buyForCounter.text = buyForCount.ToString();
    }
}
