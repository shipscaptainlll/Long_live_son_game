using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCalledUI : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void callAnimation(GameObject hud)
    {
        
        StartCoroutine(animateCanvases(hud));
    }

    public IEnumerator animateCanvases(GameObject hud)
    {
        if (hud.GetComponent<HUD>())
        {
            hud.GetComponent<CanvasGroup>().alpha = 0f;
            float elapsed = 0f;
            float updateSeconds = 0.2f;
            //hud.GetComponent<Animator>().enabled = true;
            hud.GetComponent<Animator>().Play("HUDAnimation");
            while (elapsed < updateSeconds)
            {
                elapsed += Time.deltaTime;
                hud.GetComponent<CanvasGroup>().alpha += 0.05f;
                yield return null;
            }
            hud.GetComponent<CanvasGroup>().alpha = 1.0f;
            //hud.GetComponent<Animator>().enabled = false;
        }
        else if (hud.GetComponent<HUDShopInventory>())
        {
            hud.GetComponent<CanvasGroup>().alpha = 0f;
            float elapsed = 0f;
            float updateSeconds = 0.2f;
            //hud.GetComponent<Animator>().enabled = true;
            hud.GetComponent<Animator>().Play("ShopAnimation");
            while (elapsed < updateSeconds)
            {
                elapsed += Time.deltaTime;
                hud.GetComponent<CanvasGroup>().alpha += 0.05f;
                yield return null;
            }
            hud.GetComponent<CanvasGroup>().alpha = 1.0f;
            //hud.GetComponent<Animator>().enabled = false;
        }

    }
}
