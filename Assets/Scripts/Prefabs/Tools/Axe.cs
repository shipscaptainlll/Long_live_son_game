using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public int damagePerHit;
    // Start is called before the first frame update
    

    private void Awake()
    {
        damagePerHit = 10;
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
