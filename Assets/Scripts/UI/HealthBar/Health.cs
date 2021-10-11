using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    public GameObject resource;

    public int currentHealth ;
    public int resourceMaxHealth;

    public event Action<float> OnHealthPctChanged = delegate { };

    private void Start()
    {
        currentHealth = resource.GetComponent<IResource>().MaxHealth;
        resourceMaxHealth = resource.GetComponent<IResource>().MaxHealth;
        

    }

    private void OnEnable()
    {
        
    }

    public void ModifyHealth(int amount)
    {
        
        currentHealth += amount;

        float currentHealthPct = (float)currentHealth / (float)resourceMaxHealth;
        OnHealthPctChanged(currentHealthPct);
    }

    private void ResetHealth()
    {
        currentHealth = resourceMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
