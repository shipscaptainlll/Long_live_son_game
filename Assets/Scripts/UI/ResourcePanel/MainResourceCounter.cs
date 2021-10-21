using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainResourceCounter : MonoBehaviour
{
    public float timeMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        timeMultiplier = 1f;
    }

    
    public void speedUpMultiplier()
    {
        timeMultiplier = 30f;
    }

    public void resetMultiplier()
    {
        timeMultiplier = 1;
    }
}
