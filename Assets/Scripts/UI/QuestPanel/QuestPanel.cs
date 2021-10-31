using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    public List<GameObject> listOfQuests = new List<GameObject>();
    public int countOfQuests;
    public int countOfActiveQuest;
    public int questPanelPositionMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform quest in transform)
        {
            countOfQuests++;
            Debug.Log("hello world");
        }
        for (int i = 0; i < countOfQuests; i++)
        {
            listOfQuests.Add(transform.GetChild(i).gameObject);
            listOfQuests[i].GetComponent<IQuest>().questCompleted += refreshCurrentQuest;
            Debug.Log(listOfQuests[i]);
        }
        countOfQuests = 0;
        countOfActiveQuest = 0;
        questPanelPositionMultiplier = 250;
    //showing first quest
    refreshCurrentQuest(-1, 19);
    }

    

    //Resfresh current quests, it will show, hide and move quest if character completes old quest
    public void refreshCurrentQuest(int completedQuestID, int nextQuestID)
    {
        if (completedQuestID >= 0)
        {
            countOfActiveQuest -= 1;
            StartCoroutine(closeCurrentQuest(completedQuestID));
        }
        if (nextQuestID >= 0)
        {
            countOfActiveQuest += 1;
            openNextQuest(nextQuestID);
        }
        Debug.Log(countOfActiveQuest);
        Debug.Log("Hello world, it's working!");
    }

    //Dynamically increases active quest panel transparency and then closes it
    public IEnumerator closeCurrentQuest(int completedQuestID)
    {
        while (listOfQuests[completedQuestID].GetComponent<CanvasGroup>().alpha > 0)
        {
            yield return new WaitForSeconds(0.01f);
            listOfQuests[completedQuestID].GetComponent<CanvasGroup>().alpha -= 0.05f;
            listOfQuests[completedQuestID].transform.localScale += new Vector3(0.1f, 0.1f, 0);
            //Unsubscribe quest manager from this quest
            //listOfQuests[completedQuestID].GetComponent<IQuest>().questCompleted -= refreshCurrentQuest;
        }
        if (completedQuestID != 14)
        {
            listOfQuests[completedQuestID].SetActive(false);
        }
    }

    //Method that starts showing next quest and moves it to the right position on the monitor
    public void openNextQuest(int nextQuestID)
    {
        StartCoroutine(showNextQuest(nextQuestID));
        StartCoroutine(moveNextQuest(nextQuestID));
    }

    //Dynamically decreases active quest panel transparency and then opens it
    public IEnumerator showNextQuest(int nextQuestID)
    {
        listOfQuests[nextQuestID].SetActive(true);
        while (listOfQuests[nextQuestID].GetComponent<CanvasGroup>().alpha < 1)
        {
            yield return new WaitForSeconds(0.01f);
            listOfQuests[nextQuestID].GetComponent<CanvasGroup>().alpha += 0.05f;
        }   
    }

    public IEnumerator moveNextQuest(int nextQuestID)
    {
        listOfQuests[nextQuestID].transform.localPosition = new Vector3(0, (250 - countOfActiveQuest * questPanelPositionMultiplier), 0);
        while (listOfQuests[nextQuestID].transform.localPosition.y < (750 - countOfActiveQuest * questPanelPositionMultiplier))
        {
            yield return new WaitForSeconds(0.01f);
            listOfQuests[nextQuestID].transform.localPosition += new Vector3(0, 30f, 0);
        }
    }
}
