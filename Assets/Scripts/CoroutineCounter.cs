using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoroutineCounter : MonoBehaviour
{
    

    public Text fpsText;
    public float fps;

    private void Start()
    {
        fps = 0f;
        StartCoroutine(coroutineTime());
    }

    void Update()
    {
        
        
        fpsText.text = fps.ToString();
    }


    public IEnumerator coroutineTime()
    {
        float secondsGone = 0f;
        float secondsToGo = 10000f;
        Debug.Log("hello");
        while (secondsGone < secondsToGo)
        {
            secondsGone += 0.01f;
            fps += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
