using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLow : MonoBehaviour
{
    public Transform origin;
    public Transform destination;
    public Transform destination1;
    private float counter;
    private float Ncounter;
    Vector3 pointAlongLinePos;
    Vector3 pointAlongLineNeg;
    Vector3 pointAlongLineDiff;
    public float lineDrawSpeed = 6f;
    Vector3 local;
    public bool sphereReached1;
    public bool sphereFilled;
    Vector3 scaleChange;

    private float dist;
    // Start is called before the first frame update
    void Start()
    {
        //transform.GetComponent<LineRenderer>().SetPosition(1, origin.position);
       // transform.GetComponent<LineRenderer>().SetPosition(0, origin.position);

        transform.GetComponent<LineRenderer>().startWidth = 0.1f;
        transform.GetComponent<LineRenderer>().endWidth = 0.1f;

        scaleChange = new Vector3(0.003f, 0.003f, 0.003f);
        sphereFilled = false;

    }

    // Update is called once per frame

    
    void Update()
    {
        Vector3 pointA = origin.position;
        Vector3 pointB = destination.position;
        float x1 = GetComponent<LineRenderer>().GetPosition(0).x;
        float x2 = GetComponent<LineRenderer>().GetPosition(0).y;
        float x3 = GetComponent<LineRenderer>().GetPosition(0).z;
        float d = Mathf.Sqrt(x1 * x1 + x2 * x2 + x3 * x3) * 1.5f;

        dist = Vector3.Distance(origin.position, destination.position);
        local = transform.GetComponent<LineRenderer>().GetPosition(0);
        if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Z))
        {
            if(counter < dist)
            {
                
                counter += .00009f / lineDrawSpeed;

                float x = Mathf.Lerp(0, dist, counter);
                

                //Vector3 pointA = origin.position;

                //Vector3 pointB = destination.position;
                
                pointAlongLinePos = x * Vector3.Normalize(pointB - pointA) ;

                if (d < dist) { transform.GetComponent<LineRenderer>().SetPosition(0, GetComponent<LineRenderer>().GetPosition(0) + pointAlongLinePos);  }
                else if (d >= dist) { transform.GetComponent<LineRenderer>().SetPosition(0, GetComponent<LineRenderer>().GetPosition(0)); sphereReached1 = true; }
                
                Ncounter = 0;
            }
            
        }
        
        Debug.Log(d);
        if (sphereReached1)
        {
            float scaleNow = destination.localScale.z;
            if (scaleNow < 1.5) { destination.localScale += scaleChange; }
            else if (scaleNow >= 1.5) { sphereFilled = true; }
            
        }

        if (d > dist)
        { sphereReached1 = false; }
        
        if (!Input.GetKey(KeyCode.E) && sphereFilled == false)
        {

            if (Ncounter < dist)
            {
                Ncounter += .01f / lineDrawSpeed;

                float x = Mathf.Lerp(0, dist, 0.006f);

                //Vector3 pointA = origin.position;
                //Vector3 pointB = destination.position;

                pointAlongLineNeg = x * Vector3.Normalize(pointB - pointA);

                pointAlongLineDiff = pointAlongLinePos - pointAlongLineNeg;
                if (GetComponent<LineRenderer>().GetPosition(0).z > 0) { transform.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0f, 0f, 0f)); }
                else if (GetComponent<LineRenderer>().GetPosition(0).z <= 0) { transform.GetComponent<LineRenderer>().SetPosition(0, GetComponent<LineRenderer>().GetPosition(0) - pointAlongLineNeg); }
                counter = 0;
            }
            
        }
        //transform.GetComponent<LineRenderer>().endWidth += 0.01f;
    }
}
