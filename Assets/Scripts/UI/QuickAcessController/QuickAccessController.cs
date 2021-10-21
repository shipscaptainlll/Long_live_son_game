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
    public Button upgradeCloseButton;

    //Cargo panel
    public GameObject cargoPanel;
    public Button cargoButton;
    public Button cargoCloseButton;

    //Pickaxes panel
    public GameObject pickaxesPanel;
    public Button pickaxesButton;
    public bool isToolOpened;
    public bool isPanelOpened;

    //Scissorses panel
    public GameObject scissorsesPanel;
    public Button scissorsesButton;

    //Buckets panel
    public GameObject bucketsPanel;
    public Button bucketsButton;

    //Axes panel
    public GameObject axesPanel;
    public Button axesButton;

    //Farm upgrade panel]
    public GameObject farmUpgradePanel;
    public Button farmUpgradeButton;
    public Button farmUpgradeCloseButton;

    //Boulder blast panel
    public GameObject blastBoulderPanel;
    public Button blastBoulderButton;

    //Meditation panel
    public GameObject meditationPanel;
    public Button meditationButton;
    public Button meditationCloseButton;

    //Tree upgrade panel
    public GameObject treeUpgradePanel;
    public Button treeUpgradeButton;
    public Button treeUpgradeCloseButton;

    // Start is called before the first frame update
    void Start()
    {
        currentlyOpened = null;
        QAPIsOpened = false;
        isToolOpened = false;
        isPanelOpened = false;
        upgradeButton.onClick.AddListener(openUpgradePanel);
        upgradeCloseButton.onClick.AddListener(closeUpgradePanel);
        cargoButton.onClick.AddListener(openCargoPanel);
        cargoCloseButton.onClick.AddListener(closeCargoPanel);
        pickaxesButton.onClick.AddListener(openPickaxesPanel);
        scissorsesButton.onClick.AddListener(openScissorsesPanel);
        bucketsButton.onClick.AddListener(openBucketsPanel);
        axesButton.onClick.AddListener(openAxesPanel);
        farmUpgradeButton.onClick.AddListener(openFarmUpgradePanel);
        farmUpgradeCloseButton.onClick.AddListener(closeFarmUpgradePanel);
        blastBoulderButton.onClick.AddListener(openBlastBoulderPanel);
        treeUpgradeButton.onClick.AddListener(openTreeUpgradePanel);
        treeUpgradeCloseButton.onClick.AddListener(closeTreeUpgradePanel);
        meditationButton.onClick.AddListener(openMeditationPanel);
        meditationCloseButton.onClick.AddListener(closeMeditationPanel);
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
        isPanelOpened = true;
        upgradesPanel.GetComponent<CanvasGroup>().alpha = 1;
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
        QAPIsOpened = false;
    }
    public void closeUpgradePanel()
    {
        closeCurrentlyOpened();
    }
    public void openCargoPanel()
    {
        //Debug.Log("openCargoPanel");
        currentlyOpened = cargoPanel;
        cargoPanel.transform.SetAsLastSibling();
        isPanelOpened = true;
        cargoPanel.GetComponent<CanvasGroup>().alpha = 1;
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
        QAPIsOpened = false;
    }
    public void closeCargoPanel()
    {
        closeCurrentlyOpened();
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
        //Debug.Log("openAxesPanel");
        currentlyOpened = axesPanel;
        isToolOpened = true;
        axesPanel.transform.SetAsLastSibling();
        axesPanel.GetComponent<CanvasGroup>().alpha = 1;
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
        QAPIsOpened = false;
    }
    public void openFarmUpgradePanel()
    {
        Debug.Log("openFarmUpgradePanel");
        currentlyOpened = farmUpgradePanel;
        isPanelOpened = true;
        farmUpgradePanel.transform.SetAsLastSibling();
        farmUpgradePanel.GetComponent<CanvasGroup>().alpha = 1;
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
        QAPIsOpened = false;
    }
    public void closeFarmUpgradePanel()
    {
        closeCurrentlyOpened();
    }
    public void openBlastBoulderPanel()
    {
        Debug.Log("blastBoulderPanel");
        currentlyOpened = blastBoulderPanel;
        isToolOpened = true;
        blastBoulderPanel.transform.SetAsLastSibling();
        blastBoulderPanel.GetComponent<CanvasGroup>().alpha = 1;
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
        QAPIsOpened = false;
    }
    
    public void openTreeUpgradePanel()
    {
        Debug.Log("openTreeUpgradePanel");
        currentlyOpened = treeUpgradePanel;
        isPanelOpened = true;
        treeUpgradePanel.transform.SetAsLastSibling();
        treeUpgradePanel.GetComponent<CanvasGroup>().alpha = 1;
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
        QAPIsOpened = false;
    }
    public void closeTreeUpgradePanel()
    {
        closeCurrentlyOpened();
    }

    public void openMeditationPanel()
    {
        Debug.Log("openMeditationPanel");
        currentlyOpened = meditationPanel;
        isPanelOpened = true;
        meditationPanel.transform.SetAsLastSibling();
        meditationPanel.GetComponent<CanvasGroup>().alpha = 1;
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
        QAPIsOpened = false;
    }

    public void closeMeditationPanel()
    {
        closeCurrentlyOpened();
    }

    
    public void closeCurrentlyOpened()
    {
        currentlyOpened.GetComponent<CanvasGroup>().alpha = 0;
        isToolOpened = false;
        currentlyOpened = null;
    }
}
