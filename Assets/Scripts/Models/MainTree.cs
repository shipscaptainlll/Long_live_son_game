using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTree : MonoBehaviour
{
    public Merchant15QuestTrigger Merchant15QuestTrigger; //Script that notify tree that 15th quest completed


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        //Subscribe for event, sphere near merchant, that notifies 15th quest to get completed
        Merchant15QuestTrigger.CharacterReachedSphere += ShowMainTree;
    }

    private void ShowMainTree()
    {
        Debug.Log("Hello world");
        gameObject.SetActive(true);
        //Unsbscribe from event, sphere near merchant, that notifies 15th quest to get completed
        Merchant15QuestTrigger.CharacterReachedSphere -= ShowMainTree;
    }
}
