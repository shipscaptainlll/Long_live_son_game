using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResCollectedController : MonoBehaviour
{
    public GameObject resourcesPanel;
    private void Start()
    {
        
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        //HealthBar.OreCollected += addCollectedOreToPanel;
    }

    private void OnDisable()
    {
        //HealthBar.OreCollected -= addCollectedOreToPanel;
    }

    void addCollectedOreToPanel()
    {
        //resourcesPanel.transform.Find("StoneSlot").GetComponent<RockResourceCounter>().AddToCounter();
    }
}
