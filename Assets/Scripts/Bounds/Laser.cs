using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform origin;
    public Transform destination;
    private float counter;
    private float Ncounter;
    Vector3 pointAlongLinePos;
    Vector3 pointAlongLineNeg;
    Vector3 pointAlongLineDiff;
    public float lineDrawSpeed = 6f;
    Vector3 local;
    public bool sphereReached;
    public bool sphereFilled;
    Vector3 scaleChange;
    public float d;
    public bool goalIsReached;

    public float dist;
    public bool isTriggerable;
    public bool reachedZero;
    public bool isBusy;
    public Animator animationOfBound;
    public float scaleBeginning;
    // Start is called before the first frame update
    void Start()
    {
        //transform.GetComponent<LineRenderer>().SetPosition(1, origin.position);
       // transform.GetComponent<LineRenderer>().SetPosition(0, origin.position);

        transform.GetComponent<LineRenderer>().startWidth = 0.15f;
        transform.GetComponent<LineRenderer>().endWidth = 0.15f;

        scaleChange = new Vector3(0.003f, 0.003f, 0.003f);
        sphereFilled = false;
        scaleBeginning = destination.parent.GetChild(0).localScale.z;
    }

    // Update is called once per frame


    void FixedUpdate()
    {
        transform.GetComponent<LineRenderer>().startWidth = 0.1f ;
        transform.GetComponent<LineRenderer>().endWidth = 0.1f ;
        float x1 = GetComponent<LineRenderer>().GetPosition(0).x;
        float x2 = GetComponent<LineRenderer>().GetPosition(0).y;
        float x3 = GetComponent<LineRenderer>().GetPosition(0).z;

        d = Mathf.Sqrt(x1 * x1 + x2 * x2 + x3 * x3);

        // * (float)origin.localScale.x / (float)destination.localScale.x;
        //Debug.Log(origin.localScale);
        if ((float)origin.localScale.x > 1) {
            dist = Vector3.Distance(origin.position, destination.position) ; } else { dist = Vector3.Distance(origin.position, destination.position); }
        
        local = transform.GetComponent<LineRenderer>().GetPosition(0);
        if (Input.GetKey(KeyCode.E) && isTriggerable)
        {
            if(counter < dist)
            {
                
                //Debug.Log(d + " " + origin.gameObject);
                counter += .002f / lineDrawSpeed;

                float x = Mathf.Lerp(0, dist, counter);
                

                Vector3 pointA = origin.position;
                
                Vector3 pointB = destination.position;
                
                pointAlongLinePos = x * Vector3.Normalize(pointB - pointA) ;

                if (d < dist && sphereReached == false) { transform.GetComponent<LineRenderer>().SetPosition(0, GetComponent<LineRenderer>().GetPosition(0) + pointAlongLinePos);  }
                else if (d >= dist) { transform.GetComponent<LineRenderer>().SetPosition(0, GetComponent<LineRenderer>().GetPosition(0)); sphereReached = true; }
                
                Ncounter = 0; 
            } 
            
        }
        
        if (sphereReached)
        {
            float scaleNow = destination.parent.GetChild(0).localScale.z;
            if (scaleNow < scaleBeginning * 1.5f) { destination.parent.GetChild(0).localScale += new Vector3(0.6f, 0.6f, 0.6f); animationOfBound.speed *= 1.33f; }
            else if (scaleNow >= scaleBeginning * 1.5f) { sphereFilled = true; goalIsReached = true; reachedZero = true; }
            
        }
        
        if (d < dist)
        { sphereReached = false; }

        if (GetComponent<LineRenderer>().GetPosition(0).y > 0.5 && !sphereReached) { reachedZero = false; }

        if (!Input.GetKey(KeyCode.E) && sphereFilled == false)
        {

            if (Ncounter < dist)
            {
                Ncounter += .22f / lineDrawSpeed;

                float x = Mathf.Lerp(0, dist, 0.09f);

                Vector3 pointA = origin.position;
                Vector3 pointB = destination.position;

                pointAlongLineNeg = x * Vector3.Normalize(pointB - pointA);

                pointAlongLineDiff = pointAlongLinePos - pointAlongLineNeg;
                if (GetComponent<LineRenderer>().GetPosition(0).y < 0.05) { transform.GetComponent<LineRenderer>().SetPosition(0, GetComponent<LineRenderer>().GetPosition(0)); reachedZero = true; }
                else if (GetComponent<LineRenderer>().GetPosition(0).y >= 0.05) { transform.GetComponent<LineRenderer>().SetPosition(0, GetComponent<LineRenderer>().GetPosition(0) - pointAlongLineNeg); }
                counter = 0;
            }
            
        }
        //transform.GetComponent<LineRenderer>().endWidth += 0.01f; 
    }

    public void laserOn()
    {

    }

    public void laserOff()
    {

    }
}
