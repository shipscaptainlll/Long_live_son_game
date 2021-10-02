using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealTimeCounter : MonoBehaviour
{
    

    public Text fpsText;
    public float deltaTime;

    private void Start()
    {
        
    }

    void Update()
    {
        float fps = Time.fixedTime;
        
        fpsText.text = fps.ToString();
    }

    

}
