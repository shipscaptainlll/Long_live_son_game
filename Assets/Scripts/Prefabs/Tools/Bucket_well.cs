using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket_well : MonoBehaviour
{
    public int damagePerHit;
    // Start is called before the first frame update
    

    private void Awake()
    {
        damagePerHit = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void hittedObjectOnce()
    {
        transform.parent.GetComponent<Health>().ModifyHealth(-damagePerHit);
    }

    
}
