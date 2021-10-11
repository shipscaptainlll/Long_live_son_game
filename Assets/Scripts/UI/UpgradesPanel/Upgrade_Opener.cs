using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Opener : MonoBehaviour
{
    public GameObject upgrades_Panel;
    public string Type;
    public bool isOpened;
    public Animator test;

    
    // Start is called before the first frame update
    void Start()
    {
        Type = "Upgrade opener";
        isOpened = false;
    }

    // Update is called once per frame
    public void OpenPanel()
    {
        upgrades_Panel.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void ClosePanel()
    {
        upgrades_Panel.GetComponent<CanvasGroup>().alpha = 0;
    }
}
