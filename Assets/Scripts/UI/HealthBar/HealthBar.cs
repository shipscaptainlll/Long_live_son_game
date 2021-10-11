using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    public float updateSpeedSeconds = 0.01f;

    private float barShowingLifeTime = 5f;
    float elapsedTimeBar;
    private IEnumerator coroutine;

    public event Action OreMined = delegate { };

    private void Awake()
    {
        GetComponentInParent<Health>().OnHealthPctChanged += HandleHealthChanged;


    }

    private void HandleHealthChanged(float pct)
    {
        //Debug.Log("3.HandleHealthChanged"); 
        
        StartCoroutine(ChangeToPct(pct));
        
    }

    public void showBar()
    {
        if (elapsedTimeBar == 0f) { StartCoroutine(showBarCoroutine()); }
        //if (elapsedTimeBar > 0) { StopCoroutine(coroutine); elapsedTimeBar = 0f; }
        elapsedTimeBar = 0f;
        

        
    }


    private IEnumerator ChangeToPct(float pct)
    {
        //Debug.Log("4.ChangeToPct");
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0f;
        if (foregroundImage.fillAmount > 0) { 
            while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }
        }

        foregroundImage.fillAmount = pct;
        if (foregroundImage.fillAmount <= 0)
        {
            
            resetOreHealth();
            if (OreMined != null) {
                OreMined();
            }
            


        }
        //Debug.Log("5.ChangeToPctEnd");
    }

    private void resetOreHealth()
    {
        transform.parent.GetComponent<Health>().currentHealth = 100;
        foregroundImage.fillAmount = 1;
        //Debug.Log("Ore health restored");
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

    public IEnumerator showBarCoroutine()
    {

        

        elapsedTimeBar = 0f;

        while (elapsedTimeBar < barShowingLifeTime)
        {
            yield return new WaitForSeconds(1f);

            elapsedTimeBar += 1f;
            yield return null;
        }

        if (elapsedTimeBar >= barShowingLifeTime)
        {
            gameObject.SetActive(false);
            elapsedTimeBar = 0f;
        }
        
    }
}
