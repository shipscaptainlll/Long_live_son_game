using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTree : MonoBehaviour
{
    public Merchant15QuestTrigger Merchant15QuestTrigger;


    void Start()
    {
        //gameObject.SetActive(false);
        Merchant15QuestTrigger.CharacterReachedSphere += ShowMainTree;
    }

    private void ShowMainTree()
    {
        gameObject.SetActive(true);
        Merchant15QuestTrigger.CharacterReachedSphere -= ShowMainTree;
    }
}
