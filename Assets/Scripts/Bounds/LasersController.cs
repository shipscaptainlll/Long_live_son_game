using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasersController : MonoBehaviour
{
    public bool isFirstLevel;
    public bool isLowLevel;
    public bool isMiddleLevel1;
    public bool isMiddleLevel2;

    public GameObject firstBound;
    public Laser firstBoundLaser;
    public GameObject lowBound;
    public Laser lowBoundLaser1;
    public Laser lowBoundLaser2;
    public GameObject middleBound1;
    public GameObject middleBound2;
    public Laser middleBoundLaser1;
    public Laser middleBoundLaser2;
    public Laser middleBoundLaser3;
    public GameObject highBound1;
    public GameObject highBound2;
    public GameObject highBound3;
    public Laser middleBoundLaser4;
    public Laser middleBoundLaser5;
    public Laser middleBoundLaser6;
    public GameObject highBound4;
    public GameObject highBound5;
    public GameObject highBound6;

    public bool x;
    public bool y;
    public int z;

    public GameObject kamen;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        isFirstLevel = true;
        
        isLowLevel = false;
        z = 3;
        Laser firstBoundLaser = firstBound.transform.GetChild(0).GetComponent<Laser>();
        firstBoundLaser.enabled = true;
        firstBoundLaser.isTriggerable = true;
        //Laser lowBoundLaser1 = lowBound.transform.GetChild(0).GetComponent<Laser>();
        //Laser lowBoundLaser2 = lowBound.transform.GetChild(1).GetComponent<Laser>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFirstLevel) //if we started and currently on 1 level of mining
        {
            if(firstBoundLaser.goalIsReached == true) //if bound is filled, then level is solved, we initialize next one and close this one
            {
                transform.parent.parent.GetComponentInParent<ItemHealthCounter>().lowSphereFilled = true;
                isLowLevel = true; //initialize next level

                //initialize next lazers and randomly make some of them triggarable
                lowBoundLaser1.enabled = true;
                lowBoundLaser2.enabled = true;
                lowBoundLaser1.isTriggerable = (Random.value > 0.5f);
                lowBoundLaser2.isTriggerable = (Random.value > 0.5f);

                isFirstLevel = false; //closing this level
                
            }
        }
        
        if (isLowLevel) //if we currently on 2 level of mining
        {
            if (lowBoundLaser1.reachedZero == true && lowBoundLaser2.reachedZero == true && !Input.GetKey(KeyCode.E)) // if you put up E button, than randomize lasers triggers
            {
                if (lowBoundLaser1.goalIsReached == false)
                {
                    lowBoundLaser1.isTriggerable = (Random.value > 0.5f);
                }

                if (lowBoundLaser2.goalIsReached == false)
                {
                    lowBoundLaser2.isTriggerable = (Random.value > 0.5f);
                }
            }
                
            if (lowBoundLaser1.goalIsReached == true)
            {
                Debug.Log("lowBoundLaser1 is reached");
                transform.parent.parent.GetComponentInParent<ItemHealthCounter>().middleSphereFilled1 = true;
            }
            if (lowBoundLaser2.goalIsReached == true)
            {
                Debug.Log("lowBoundLaser2 is reached");
                transform.parent.parent.GetComponentInParent<ItemHealthCounter>().middleSphereFilled2 = true;
            }
            /*
            if (lowBoundLaser1.goalIsReached == true) //if bound is filled, then level is solved, we initialize next one and close this one
            {
                isMiddleLevel1 = true; //initialize next level

                //initialize next lazers and randomly make some of them triggarable
                middleBoundLaser1.enabled = true;
                middleBoundLaser2.enabled = true;
                middleBoundLaser3.enabled = true;
                middleBoundLaser1.isTriggerable = (Random.value > 0.5f);
                middleBoundLaser2.isTriggerable = (Random.value > 0.5f);
                middleBoundLaser3.isTriggerable = (Random.value > 0.5f);
            }
            if (lowBoundLaser2.goalIsReached == true) //if bound is filled, then level is solved, we initialize next one and close this one
            {
                isMiddleLevel2 = true; //initialize next level

                //initialize next lazers and randomly make some of them triggarable
                middleBoundLaser4.enabled = true;
                middleBoundLaser5.enabled = true;
                middleBoundLaser6.enabled = true;
                middleBoundLaser4.isTriggerable = (Random.value > 0.5f);
                middleBoundLaser5.isTriggerable = (Random.value > 0.5f);
                middleBoundLaser6.isTriggerable = (Random.value > 0.5f);
            }
            */
            if (lowBoundLaser1.goalIsReached == true && lowBoundLaser2.goalIsReached == true) //if bound is filled, then level is solved, we initialize next one and close this one
            {
                anim.speed = 1f;
                isLowLevel = false; //closing this level
            }
        }

        if (isMiddleLevel1) //if we currently on 3 level of mining
        {
            if (middleBoundLaser1.reachedZero == true && middleBoundLaser2.reachedZero == true && middleBoundLaser3.reachedZero == true && !Input.GetKey(KeyCode.E)) // if you put up E button, than randomize lasers triggers
            {
                if (middleBoundLaser1.goalIsReached == false)
                {
                    middleBoundLaser1.isTriggerable = (Random.value > 0.5f);
                }

                if (middleBoundLaser2.goalIsReached == false)
                {
                    middleBoundLaser2.isTriggerable = (Random.value > 0.5f);
                }

                if (middleBoundLaser3.goalIsReached == false)
                {
                    middleBoundLaser3.isTriggerable = (Random.value > 0.5f);
                }
            }

            if (middleBoundLaser1.goalIsReached == true)
            {
                Debug.Log("lowBoundLaser1 is reached");
            }
            if (middleBoundLaser2.goalIsReached == true)
            {
                Debug.Log("lowBoundLaser2 is reached");
            }
            if (middleBoundLaser3.goalIsReached == true)
            {
                Debug.Log("lowBoundLaser2 is reached");
            }
            if (middleBoundLaser1.goalIsReached == true && middleBoundLaser2.goalIsReached == true && middleBoundLaser3.goalIsReached == true)
            {
                isMiddleLevel1 = true;
                isMiddleLevel1 = false;
            }
        }

        if (isMiddleLevel2) //if we currently on 3 level of mining
        {
            if (middleBoundLaser4.reachedZero == true && middleBoundLaser5.reachedZero == true && middleBoundLaser6.reachedZero == true && !Input.GetKey(KeyCode.E)) // if you put up E button, than randomize lasers triggers
            {
                if (middleBoundLaser4.goalIsReached == false)
                {
                    middleBoundLaser4.isTriggerable = (Random.value > 0.5f);
                }

                if (middleBoundLaser5.goalIsReached == false)
                {
                    middleBoundLaser5.isTriggerable = (Random.value > 0.5f);
                }

                if (middleBoundLaser6.goalIsReached == false)
                {
                    middleBoundLaser6.isTriggerable = (Random.value > 0.5f);
                }
            }

            if (middleBoundLaser4.goalIsReached == true)
            {
                Debug.Log("lowBoundLaser1 is reached");
            }
            if (middleBoundLaser5.goalIsReached == true)
            {
                Debug.Log("lowBoundLaser2 is reached");
            }
            if (middleBoundLaser6.goalIsReached == true)
            {
                Debug.Log("lowBoundLaser2 is reached");
            }
            if (middleBoundLaser4.goalIsReached == true && middleBoundLaser5.goalIsReached == true && middleBoundLaser6.goalIsReached == true)
            {
                isMiddleLevel2 = true;
                isMiddleLevel2 = false;
            }
        }

    }

    

}
