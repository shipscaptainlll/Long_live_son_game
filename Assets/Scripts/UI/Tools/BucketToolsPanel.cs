using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BucketToolsPanel : MonoBehaviour
{
    public GameObject listOfToolsInUI;
    public List<GameObject> listOfTools;
    public UBBucket uBBucket;
    [SerializeField]
    private InteractionController interactionController;
    public Text toolsOriginsCounter;
    public int toolsLimit;
    public int toolsCount;
    public int toolsUsed;


    // Start is called before the first frame update
    void Start()
    {
        toolsUsed = 0;
        toolsLimit = uBBucket.toolsBoughtMax;
        uBBucket.newToolBought += newToolBought;
        interactionController.contactedWell += toolUsageControl;
        interactionController.BucketContactedWithEarth += toolUsageControl;
        for (int i = 0; i < toolsLimit; i++)
        {
            listOfTools.Add(listOfToolsInUI.transform.GetChild(i).gameObject);
            listOfTools[i].transform.Find("OriginToolImage").GetComponent<Image>().enabled = false;
        }
        updateToolsCounterOnPanel();
    }
    private void Awake()
    {

    }

    public void newToolBought(int toolsCountFromAction)
    {
        toolsCount = toolsCountFromAction;
        updateToolsOnPanel();
        updateToolsCounterOnPanel();
    }

    public void updateToolsOnPanel()
    {
        for (int i = 0; i < toolsCount; i++)
        {
            listOfTools[i].transform.Find("OriginToolImage").GetComponent<Image>().enabled = true;
        }
    }

    public void updateUsedToolsOnPanel()
    {
        for (int i = 0; i < toolsCount; i++)
        {
            listOfTools[toolsCount - 1 - i].transform.Find("FadedToolImage").GetComponent<Image>().enabled = false;
        }
        for (int i = 0; i < toolsUsed; i++)
        {
            listOfTools[toolsCount - 1 - i].transform.Find("FadedToolImage").GetComponent<Image>().enabled = true;
        }
    }

    public void updateToolsCounterOnPanel()
    {
        toolsOriginsCounter.text = toolsCount.ToString();
    }

    public void toolUsageControl(int i)
    {
        if (i == 1)
        {
            useTool();
            updateUsedToolsOnPanel();
        } else if (i == 0)
        {
            stopUsingTool();
            updateUsedToolsOnPanel();
        }
    }

    public void toolUsageControl(GameObject garbageData, string toActivateOrDeactivate)
    {
        if (toActivateOrDeactivate == "Activate")
        {
            useTool();
            updateUsedToolsOnPanel();
        }
        else if (toActivateOrDeactivate == "Deactivate")
        {
            stopUsingTool();
            updateUsedToolsOnPanel();
        }
    }

    public void useTool()
    {
        toolsUsed += 1;
    }

    public void stopUsingTool()
    {
        toolsUsed -= 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
