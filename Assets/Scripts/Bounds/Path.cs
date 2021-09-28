using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path: MonoBehaviour
{
    public Transform origin;
    public Transform destination;
    Vector3 pointAlongLinePos;
    public float dist;
    public float x;
    public bool isUpdating;

    // Start is called before the first frame update
    void Start()
    {
        //transform.GetComponent<LineRenderer>().SetPosition(1, origin.position);
        // transform.GetComponent<LineRenderer>().SetPosition(0, origin.position);

        transform.GetComponent<LineRenderer>().startWidth = 0.035f;
        transform.GetComponent<LineRenderer>().endWidth = 0.035f;

        isUpdating = true;
        x = Mathf.Lerp(0, dist, 1f);


        Vector3 pointA = origin.position;

        Vector3 pointB = destination.position;
        dist = Vector3.Distance(origin.position, destination.position);
        pointAlongLinePos = x * Vector3.Normalize(pointB - pointA);

        transform.GetComponent<LineRenderer>().SetPosition(0, pointAlongLinePos);
    }

    private void Update()
    {
        //transform.GetComponent<LineRenderer>().startWidth = 0.7f;
        //transform.GetComponent<LineRenderer>().endWidth = 0.7f;

        
            x = Mathf.Lerp(0, dist, 1f);


            Vector3 pointA = origin.position;

            Vector3 pointB = destination.position;
            dist = Vector3.Distance(origin.position, destination.position);
            pointAlongLinePos = x * Vector3.Normalize(pointB - pointA);

            transform.GetComponent<LineRenderer>().SetPosition(0, pointAlongLinePos);
            isUpdating = false;
        gameObject.GetComponent<Path>().enabled = false;
    }
}
