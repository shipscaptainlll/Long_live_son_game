using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickAccessController : MonoBehaviour
{
    public bool QAPIsOpened;
    public GameObject quickAccessPanel;
    public GameObject currentlyOpened;

    //Upgrades panel
    public GameObject upgradesPanel;
    public Button upgradeButton;

    //Cargo panel
    public GameObject cargoPanel;
    public Button cargoButton;

    //Pickaxes panel
    public GameObject pickaxesPanel;
    public Button pickaxesButton;
    public bool isToolOpened;

    //Scissorses panel
    public GameObject scissorsesPanel;
    public Button scissorsesButton;

    //Buckets panel
    public GameObject bucketsPanel;
    public Button bucketsButton;

    //Axes panel
    public GameObject axesPanel;
    public Button axesButton;

    //Meditation panel
    public GameObject meditationPanel;
    public Button meditationButton;

    // Start is called before the first frame update
    void Start()
    {
        currentlyOpened = null;
        QAPIsOpened = false;
        isToolOpened = false;
        upgradeButton.onClick.AddListener(openUpgradePanel);
        cargoButton.onClick.AddListener(openCargoPanel);
        pickaxesButton.onClick.AddListener(openPickaxesPanel);
        scissorsesButton.onClick.AddListener(openScissorsesPanel);
        bucketsButton.onClick.AddListener(openBucketsPanel);
        axesButton.onClick.AddListener(openAxesPanel);
        meditationButton.onClick.AddListener(openMeditationPanel);
    }

    public void openPanel()
    {
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 1;
        quickAccessPanel.transform.SetAsLastSibling();
        QAPIsOpened = true;
    }

    public void closePanel()
    {
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
        quickAccessPanel.transform.SetAsFirstSibling();
        QAPIsOpened = false;
    }

    public void openUpgradePanel()
    {
        //Debug.Log("openUpgradePanel");
        currentlyOpened = upgradesPanel;
        upgradesPanel.transform.SetAsLastSibling();
        upgradesPanel.GetComponent<CanvasGroup>().alpha = 1;
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
        QAPIsOpened = false;
    }
    public void openCargoPanel()
    {
        //Debug.Log("openCargoPanel");
        currentlyOpened = cargoPanel;
        cargoPanel.transform.SetAsLastSibling();
        cargoPanel.GetComponent<CanvasGroup>().alpha = 1;
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
        QAPIsOpened = false;
    }
    public void openPickaxesPanel()
    {
        Debug.Log("openPickaxesPanel");
        currentlyOpened = pickaxesPanel;
        isToolOpened = true;
        pickaxesPanel.transform.SetAsLastSibling();
        pickaxesPanel.GetComponent<CanvasGroup>().alpha = 1;
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
        QAPIsOpened = false;
    }
    public void openScissorsesPanel()
    {
        Debug.Log("openScissorsesPanel");
        currentlyOpened = scissorsesPanel;
        isToolOpened = true;
        scissorsesPanel.transform.SetAsLastSibling();
        scissorsesPanel.GetComponent<CanvasGroup>().alpha = 1;
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
        QAPIsOpened = false;
    }
    public void openBucketsPanel()
    {
        Debug.Log("openBucketsPanel");
        currentlyOpened = bucketsPanel;
        isToolOpened = true;
        bucketsPanel.transform.SetAsLastSibling();
        bucketsPanel.GetComponent<CanvasGroup>().alpha = 1;
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
        QAPIsOpened = false;
    }
    public void openAxesPanel()
    {
        Debug.Log("openAxesPanel");
        currentlyOpened = axesPanel;
        isToolOpened = true;
        axesPanel.transform.SetAsLastSibling();
        axesPanel.GetComponent<CanvasGroup>().alpha = 1;
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
        QAPIsOpened = false;
    }
    public void openMeditationPanel()
    {
        Debug.Log("openMeditationPanel");
        currentlyOpened = meditationPanel;
        meditationPanel.transform.SetAsLastSibling();
        meditationPanel.GetComponent<CanvasGroup>().alpha = 1;
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
        QAPIsOpened = false;
    }

    public void closeCurrentlyOpened()
    {
        currentlyOpened.GetComponent<CanvasGroup>().alpha = 0;
        cargoPanel.transform.SetAsFirstSibling();
        isToolOpened = false;
        currentlyOpened = null;
    }
}
