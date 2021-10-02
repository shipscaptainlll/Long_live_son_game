using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockResourceCounter : MonoBehaviour
{
    private int count;

    private Text countToText;


    public string Type
    {
        get
        {
            return "Rock";
        }
    }

    public void Start()
    {
        countToText = transform.Find("Counter").GetChild(0).GetComponent<Text>();
    }
    void OnEnable()
    {
        InputController.OnClicked += AddToCounter;
    }

    private void OnDisable()
    {
        InputController.OnClicked -= AddToCounter;
    }

    void AddToCounter()
    {
        count += 1;
        RefreshCounter(); 
    }

    void RefreshCounter()
    {
        countToText.text = count.ToString();
    }
}
