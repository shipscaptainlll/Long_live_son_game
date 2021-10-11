using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors_tree : MonoBehaviour
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
        transform.parent.Find("Scissors1").gameObject.SetActive(false);
        if (transform.parent.GetComponent<Earth>().isGatheringCarrots == true) { transform.parent.Find("Carrots").gameObject.SetActive(false); }
        if (transform.parent.GetComponent<Earth>().isGatheringApples == true) { transform.parent.Find("Apples").gameObject.SetActive(false); }
        transform.parent.GetComponent<Earth>().startCollectingPlantToResources();
        transform.parent.GetComponent<Earth>().collectMinedOre();
        transform.gameObject.SetActive(false);
        
    }

    
}
