using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseFarm : MonoBehaviour
{
    [SerializeField] InteractionController InteractionController;
    [SerializeField] Transform SecondFloorFarmsUI;
    [SerializeField] Transform SecondFloorFarms;
    [SerializeField] Transform InvisibleFarmTransform;
    [SerializeField] Button RightButton;
    [SerializeField] Button LeftButton;

     IFarmUI InvisibleFarm;
    Transform[] FarmArrayUI = new Transform[8];
    List<IFarmUI> FoundFarmArray = new List<IFarmUI>();
    Transform currentlyShownFarm;
    Transform currentlyShownElement;
    int _farmNumberInArray;
    int _currentlyShownFarmNumber;
    int _farmsOnLevel;
    int _farmsKnown;


    private void Start()
    {
        InvisibleFarm = InvisibleFarmTransform.GetComponent<IFarmUI>();

        for (int i = 0; i < 7; i++) 
        {
            SecondFloorFarmsUI.GetChild(i).GetComponent<IFarmUI>().FarmReached += AddNewKnown;
            SecondFloorFarmsUI.GetChild(i).GetComponent<IFarmUI>().FarmIsCreated += RefreshPanels;
        }
        
        RightButton.onClick.AddListener(SwipeRight);
        LeftButton.onClick.AddListener(SwipeLeft);
        FoundFarmArray.Add(InvisibleFarm);
        _farmsOnLevel = 8;
        _farmsKnown = 1;
        
        for (int i = 0; i < _farmsOnLevel; i++)
        {
            FarmArrayUI[i] = SecondFloorFarmsUI.GetChild(i);
        }
        _currentlyShownFarmNumber = 1;
        //_farmNumberInArray = FoundFarmArray[_currentlyShownFarmNumber - 1].FarmNumber;
        //currentlyShownFarm = FarmArrayUI[_farmNumberInArray-1];
        VisualizeFarm(_currentlyShownFarmNumber);
    }

    void VisualizeFarm(int farmNumber)
    {
        if (currentlyShownElement != null)
        {
            Hide(currentlyShownElement);
        }
        _farmNumberInArray = FoundFarmArray[_currentlyShownFarmNumber - 1].FarmNumber;
        currentlyShownFarm = FarmArrayUI[_farmNumberInArray - 1];
        IFarmUI Farm = currentlyShownFarm.GetComponent<IFarmUI>();
        if (FoundFarmArray[_currentlyShownFarmNumber - 1].IsCreated)
        {
            currentlyShownElement = currentlyShownFarm.Find("Borders").Find("Created");
            Show(currentlyShownElement);
        } else if (FoundFarmArray[_currentlyShownFarmNumber - 1].IsReached)
        {
            currentlyShownElement = currentlyShownFarm.Find("Borders").Find("NotCreated");
            Show(currentlyShownElement);
        } else if (FoundFarmArray[_currentlyShownFarmNumber - 1].IsKnown)
        {
            currentlyShownElement = currentlyShownFarm.Find("Borders").Find("NotOpened");
            Show(currentlyShownElement);
        }
    }

    void RefreshPanels()
    {
        VisualizeFarm(_currentlyShownFarmNumber);
    }
    void AddNewKnown(IFarmUI foundFarm)
    {
        FoundFarmArray.Remove(InvisibleFarm);
        if (_farmsKnown <= 7)
        {
            
            _farmsKnown++;
            Debug.Log(_farmsKnown);
            FoundFarmArray.Add(foundFarm);
            if (_farmsKnown < 7)
            {
                FoundFarmArray.Add(InvisibleFarm);
            }
            foundFarm.FarmReached -= AddNewKnown;
            VisualizeFarm(_currentlyShownFarmNumber);
        } else 
        {
            
        }
    }

    void SwipeRight()
    {
        Debug.Log(_currentlyShownFarmNumber);
        if ((_currentlyShownFarmNumber + 1) <= _farmsKnown)
        {
            _currentlyShownFarmNumber++;
            VisualizeFarm(_currentlyShownFarmNumber);
        } else
        {
            _currentlyShownFarmNumber = 1;
            VisualizeFarm(_currentlyShownFarmNumber);
        }
    }

    void SwipeLeft()
    {
        if ((_currentlyShownFarmNumber - 1) >= 1)
        {
            _currentlyShownFarmNumber--;
            VisualizeFarm(_currentlyShownFarmNumber);
        }
        else
        {
            _currentlyShownFarmNumber = _farmsKnown;
            VisualizeFarm(_currentlyShownFarmNumber);
        }
    }

    void Show(Transform ObjectToShow)
    {
        ObjectToShow.GetComponent<CanvasGroup>().alpha = 1;
        ObjectToShow.transform.SetAsLastSibling();
        ObjectToShow.parent.parent.transform.SetAsLastSibling();
    }

    void Hide(Transform ObjectToHide)
    {
        ObjectToHide.GetComponent<CanvasGroup>().alpha = 0;
    }
}
