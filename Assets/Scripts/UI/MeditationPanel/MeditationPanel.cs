using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeditationPanel : MonoBehaviour
{
    public Button meditationButton;
    public Text costText;
    public RShardResourceCounter rShardResourceCounter;
    public QuickAccessController quickAccessController;
    public int costOfMeditation;
    public bool isMeditating = false;

    // Start is called before the first frame update
    void Start()
    {
        costOfMeditation = 500;
        refreshCostOfMeditation();
        meditationButton.onClick.AddListener(startMeditation);
    }

    
    public void startMeditation()
    {
        if (isMeditating == false && costOfMeditation < rShardResourceCounter.count)
        {
            isMeditating = true;
            decreaseMainCounter();
            quickAccessController.closeMeditationPanel();
            StartCoroutine(meditate());
        }
        
    }
    public IEnumerator meditate()
    {
        yield return new WaitForSeconds(3);
        isMeditating = false;
        Debug.Log("Hello");
    }

    //decrease meditation cost from main resource counter
    public void decreaseMainCounter()
    {
        rShardResourceCounter.AddToCounter(-costOfMeditation);
    }
    public void refreshCostOfMeditation()
    {
        costText.text = costOfMeditation.ToString();
    }
}
