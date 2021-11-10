using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerTwentyEighth : MonoBehaviour
{
    public event Action CharacterReachedSphere = delegate { };
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (CharacterReachedSphere != null)
        {
            CharacterReachedSphere();
        }
        gameObject.SetActive(false);
    }
}
