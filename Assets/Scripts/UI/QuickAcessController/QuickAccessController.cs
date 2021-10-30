using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickAccessController : MonoBehaviour
{
    public bool QAPIsOpened;
    public GameObject quickAccessPanel;
    public GameObject currentlyOpened;
    public GameObject CharacterBody; //Character body that will be spawned near spawn place when entering panel
    public MouseRotation CharacterCamera; //Character body that will be rotated in right direction near spawn place when entering panel

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
    public Transform CharacterSpawnMeditation; //Place where character spawns when entering meditation panel;

    //Tree upgrade panel
    public GameObject treeUpgradePanel;
    public Button treeUpgradeButton;
    public Button treeUpgradeCloseButton;

    //Seeds tools panel
    [SerializeField]
    private GameObject seedsToolsPanel;
    public event Action<GameObject> ChooseSeedsPanelOpenedFor = delegate { };

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
        seedsToolsPanel.GetComponent<SeedsToolsPanel>().ProductionTypeChosen += CloseSeedsToolPanel;
    }

    public IEnumerator OpenPanel2()
    {
        while (quickAccessPanel.GetComponent<CanvasGroup>().alpha < 1)
        {
            quickAccessPanel.GetComponent<CanvasGroup>().alpha += 0.18f;
            yield return new WaitForSeconds(0.01f);
        }
        quickAccessPanel.transform.SetAsLastSibling();
        QAPIsOpened = true;
    }
    
    public IEnumerator ClosePanel2()
    {
        while (quickAccessPanel.GetComponent<CanvasGroup>().alpha > 0)
        {
            quickAccessPanel.GetComponent<CanvasGroup>().alpha -= 0.18f;
            yield return new WaitForSeconds(0.01f);
        }
        quickAccessPanel.transform.SetAsFirstSibling();
        QAPIsOpened = false;
    }

    public void openUpgradePanel()
    {
        currentlyOpened = upgradesPanel;
        StartCoroutine(ShowSubPanel(currentlyOpened));
    }
    public void closeUpgradePanel()
    {
        StartCoroutine(closeCurrentlyOpened2());
    }
    public void openCargoPanel()
    {
        currentlyOpened = cargoPanel;
        StartCoroutine(ShowSubPanel(currentlyOpened));
    }
    public void closeCargoPanel()
    {
        StartCoroutine(closeCurrentlyOpened2());
    }
    public void openPickaxesPanel()
    {
        currentlyOpened = pickaxesPanel;
        StartCoroutine(ShowSubTool(currentlyOpened));
    }
    public void openScissorsesPanel()
    {
        currentlyOpened = scissorsesPanel;
        StartCoroutine(ShowSubTool(currentlyOpened));
    }
    public void openBucketsPanel()
    {
        currentlyOpened = bucketsPanel; 
        StartCoroutine(ShowSubTool(currentlyOpened));
    }
    public void openAxesPanel() 
    {
        currentlyOpened = axesPanel; 
        StartCoroutine(ShowSubTool(currentlyOpened));
    }
    public void openFarmUpgradePanel()
    {
        currentlyOpened = farmUpgradePanel;
        StartCoroutine(ShowSubPanel(currentlyOpened));
    }
    public void closeFarmUpgradePanel()
    {
        StartCoroutine(closeCurrentlyOpened2());
    }
    public void openBlastBoulderPanel()
    {
        currentlyOpened = blastBoulderPanel;
        StartCoroutine(ShowSubTool(currentlyOpened));
    }
    
    public void openTreeUpgradePanel()
    {
        currentlyOpened = treeUpgradePanel;
        StartCoroutine(ShowSubPanel(currentlyOpened));
    }

    

    public void closeTreeUpgradePanel()
    {
        StartCoroutine(closeCurrentlyOpened2());
    }

    public void openMeditationPanel()
    {
        Debug.Log("openMeditationPanel");
        // Teleporting character to the spawn place, associated with this panel
        var offstet = CharacterSpawnMeditation.transform.position - CharacterBody.transform.position;
        CharacterBody.GetComponent<CharacterController>().enabled = false;
        CharacterBody.transform.position = CharacterSpawnMeditation.transform.position;
        CharacterBody.GetComponent<CharacterController>().enabled = true;
        CharacterBody.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
        CharacterCamera.yRotation = 40;
        //Changing panels transparency
        currentlyOpened = meditationPanel;
        StartCoroutine(ShowSubPanel(currentlyOpened));
    }

    public void closeMeditationPanel()
    {
        StartCoroutine(closeCurrentlyOpened2());
    }

    public void OpenSeedChoosePanelFor(GameObject detectedEarth)
    {
        currentlyOpened = seedsToolsPanel;
        StartCoroutine(ShowSubPanel(currentlyOpened));
        seedsToolsPanel.transform.parent.SetAsLastSibling();
        NotifySeedsPanelAboutObject(detectedEarth);
    }

    

    private void NotifySeedsPanelAboutObject(GameObject detectedEarth)
    {
        if (ChooseSeedsPanelOpenedFor != null)
        {
            ChooseSeedsPanelOpenedFor(detectedEarth);
            Debug.Log(detectedEarth);
        }
    }

    private void CloseSeedsToolPanel(GameObject garbageDate, String garbageString)
    {
        StartCoroutine(closeCurrentlyOpened2());
    }

    public IEnumerator closeCurrentlyOpened2()
    {
        while (currentlyOpened.GetComponent<CanvasGroup>().alpha > 0)
        {
            currentlyOpened.GetComponent<CanvasGroup>().alpha -= 0.18f;
            yield return new WaitForSeconds(0.01f);
        }
        isToolOpened = false;
        isPanelOpened = false;
        currentlyOpened = null;
    }

    public IEnumerator ShowSubPanel(GameObject subPanel)
    {
        while (quickAccessPanel.GetComponent<CanvasGroup>().alpha > 0)
        {
            quickAccessPanel.GetComponent<CanvasGroup>().alpha -= 0.12f;
            yield return new WaitForSeconds(0.01f);
        }
        QAPIsOpened = false;

        while (subPanel.GetComponent<CanvasGroup>().alpha < 1)
        {
            subPanel.GetComponent<CanvasGroup>().alpha += 0.12f;
            yield return new WaitForSeconds(0.01f);
        }
        subPanel.transform.SetAsLastSibling();
        isPanelOpened = true;
    }

    public IEnumerator ShowSubTool(GameObject subPanel)
    {
        while (quickAccessPanel.GetComponent<CanvasGroup>().alpha > 0)
        {
            quickAccessPanel.GetComponent<CanvasGroup>().alpha -= 0.12f;
            yield return new WaitForSeconds(0.01f);
        }
        QAPIsOpened = false;

        while (subPanel.GetComponent<CanvasGroup>().alpha < 1)
        {
            subPanel.GetComponent<CanvasGroup>().alpha += 0.12f;
            yield return new WaitForSeconds(0.01f);
        }
        subPanel.transform.SetAsLastSibling();
        isToolOpened = true;
    }
}
