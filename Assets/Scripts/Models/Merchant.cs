using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Merchant : MonoBehaviour
{
    public Text contactMerchant;
    public GameObject shopInventoryInterface;
    public GameObject HUD;

    public int CONTACT_MERCHANT_TO_UPLOAD = 0;
    // Start is called before the first frame update
    void Start()
    {
        contactMerchant.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("GoodBuy");
        }
        shopInventoryInterface.gameObject.SetActive(false);
        HUD.transform.localPosition = new Vector3(0, 243.75f, 0);
        contactMerchant.gameObject.SetActive(false);
        CONTACT_MERCHANT_TO_UPLOAD = 0;
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Hello");
        }
        if (HUD.GetComponent<CanvasGroup>().alpha == 0) {
            contactMerchant.gameObject.SetActive(true);
            CONTACT_MERCHANT_TO_UPLOAD = 1;
        }
        
    }

    
}
