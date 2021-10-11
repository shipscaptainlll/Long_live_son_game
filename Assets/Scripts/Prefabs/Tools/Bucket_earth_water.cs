using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket_earth_water : MonoBehaviour
{
    public int damagePerHit;
    // Start is called before the first frame update
    

    private void Awake()
    {
        damagePerHit = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void hittedObjectOnce()
    {
        transform.parent.GetComponent<Earth>().collectPlantToResources();
        transform.parent.GetComponent<Earth>().startGrowing();
        transform.gameObject.SetActive(false);
    }

    
}
