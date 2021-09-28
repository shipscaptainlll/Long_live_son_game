using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Random.Range(0f, 0.5f);
        transform.position += new Vector3(x * Time.deltaTime, -x * Time.deltaTime * 0.2f, x * Time.deltaTime);
        transform.Rotate(Vector3.up * x * 8.5f * Time.deltaTime);
    }
}
