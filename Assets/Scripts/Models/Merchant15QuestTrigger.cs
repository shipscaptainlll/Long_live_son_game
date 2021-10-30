using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant15QuestTrigger : MonoBehaviour
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
    }
}
