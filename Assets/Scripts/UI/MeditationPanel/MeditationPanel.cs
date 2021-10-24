using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeditationPanel : MonoBehaviour
{
    public Button meditationButton; // Button that starts meditating process, when clicked
    public Text costText; // UI panel text object, that shows how much meditation costs to character
    public RShardResourceCounter rShardResourceCounter; //Acesspoint to rock shards main counter
    public QuickAccessController quickAccessController; //Acesspoint to quick access panel (which is called in game by rclick)
    public Animator BonfireAnimation; //Bonfire light movement animation
    public int costOfMeditation; //Defines how many shards will it cost to meditate
    public bool isMeditating; //Defines if character meditating right now or not
    public byte miningSpeedMultiplier; //How x times faster the mining process during meditation will be
    public byte meditationDuration; //How long character will meditate
    public float normalBonfireSpeed; //Normal speed of bonfire lights movement
    

    public event Action<byte, bool> startedMeditating = delegate { }; //Send notification that character started meditating to eleventh quest
    // Start is called before the first frame update
    void Start()
    {
        //Initialize crucial parameters at start
        isMeditating = false; //At start of the game course is false
        costOfMeditation = 5; 
        miningSpeedMultiplier = 10;
        meditationDuration = 6;
        normalBonfireSpeed = 1f;
        //Refreshes UI panels to show previously initialized values
        refreshCostOfMeditation();
        //Adds listener on UI button, that will consuquently start meditating process
        meditationButton.onClick.AddListener(startMeditation);
    }

    //Checks if player has enough rock shards to pay, takes this ammount from rock shards main counter and notifies subscribers that meditation started
    public void startMeditation()
    {
        if (isMeditating == false && costOfMeditation < rShardResourceCounter.count)
        {
            isMeditating = true;
            //Changing bonfire light animation speed to create an illusion of time acceleration
            StartCoroutine(AccelerateBonfireLightMovement());
            if (startedMeditating != null)
            {
                startedMeditating(miningSpeedMultiplier, isMeditating);
            }
            decreaseMainCounter();
            quickAccessController.closeMeditationPanel();
            StartCoroutine(meditate());
        }
    }

    //Waits for ammount of time (meditating) and then stops meditating and notifies subscribers about it
    public IEnumerator meditate()
    {
        yield return new WaitForSeconds(meditationDuration - 1);
        StartCoroutine(DecreaseBonfireLightMovement());
        yield return new WaitForSeconds(1);
        isMeditating = false;
        //Returning bonfire light animation speed back to normal
        
        if (startedMeditating != null)
        {
            startedMeditating(miningSpeedMultiplier, isMeditating);
        }
    }

    private IEnumerator AccelerateBonfireLightMovement()
    {
        float secondsPassed = 0;
        float secondsLimit = 1f;
        Debug.Log(BonfireAnimation.speed);
        while (secondsPassed < secondsLimit)
        {
            secondsPassed += Time.deltaTime;
            BonfireAnimation.speed = Mathf.Lerp(normalBonfireSpeed, normalBonfireSpeed * miningSpeedMultiplier, secondsPassed / secondsLimit);
            yield return null;
        }

        BonfireAnimation.speed = normalBonfireSpeed * miningSpeedMultiplier;
    }

    private IEnumerator DecreaseBonfireLightMovement()
    {
        float secondsPassed = 0;
        float secondsLimit = 1f;
        while (secondsPassed < secondsLimit)
        {
            secondsPassed += Time.deltaTime;
            BonfireAnimation.speed = Mathf.Lerp(normalBonfireSpeed * miningSpeedMultiplier, normalBonfireSpeed, secondsPassed / secondsLimit);
            yield return null;
        }

        BonfireAnimation.speed = normalBonfireSpeed;
        Debug.Log(BonfireAnimation.speed);
    }

    //Subtracts meditation cost from main resource counter
    public void decreaseMainCounter()
    {
        rShardResourceCounter.AddToCounter(-costOfMeditation);
    }

    //Refreshes UI panel text value, that shows how much does meditation costs
    public void refreshCostOfMeditation()
    {
        costText.text = costOfMeditation.ToString();
    }
}
