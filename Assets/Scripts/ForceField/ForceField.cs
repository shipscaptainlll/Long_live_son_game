using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ForceField : MonoBehaviour
{
    //Force field
    public bool isCastingSphere;
    public GameObject characterController;
    Vector3 speedForceField = new Vector3(1.0f, 1.0f, 1.0f);
    public ParticleSystem magicFog;
    public Animator alchemyEnter;
    ParticleSystem.ShapeModule ps;
    public Light directionalLight;
    public GameObject handAlchemic;
    public GameObject crystalAlchemic;
    public PostProcessVolume volume;
    private Bloom _Bloom;
    public float elapsedTimeBar = 0f;
    public float barShowingLifeTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        directionalLight.color = new Color(0.1f, 0.3f, 0.1f, 255);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCastingSphere)
        {
            castForceField(); 
            if (elapsedTimeBar == 0f) { StartCoroutine(stopGlowingCrystall()); }
            
        }
        else if (!isCastingSphere) { decastForceField(); }
    }

    public void openSphere()
    {
        if (isCastingSphere == false)
        {
            Debug.Log("Hello");
            
            handAlchemic.SetActive(true);
            handAlchemic.GetComponent<Animator>().Play("Grasp");
            volume.profile.TryGetSettings(out _Bloom);
            _Bloom.intensity.value = 25;
            
            isCastingSphere = true;
            gameObject.SetActive(true);
            gameObject.transform.localScale = new Vector3(11f, 11f, 11f);
            ps = magicFog.GetComponent<ParticleSystem>().shape;
            magicFog.gameObject.SetActive(true);
            alchemyEnter.Play("AlchemyEnter");
        }
        else if (isCastingSphere == true)
        {
            _Bloom.intensity.value = 0;
            speedForceField = new Vector3(1.0f, 1.0f, 1.0f);
            isCastingSphere = false;
        }
    }

    public IEnumerator stopGlowingCrystall()
    {
        elapsedTimeBar = 0f;

        while (elapsedTimeBar < barShowingLifeTime)
        {
            
            _Bloom.intensity.value -= 0.6f;
            elapsedTimeBar += 0.25f;
            yield return new WaitForSeconds(0.25f);
        }

        if (elapsedTimeBar >= barShowingLifeTime)
        {
            speedForceField = new Vector3(1.0f, 1.0f, 1.0f);
            isCastingSphere = false;
            elapsedTimeBar = 0f;
            _Bloom.intensity.value = 0f;
        }

    }

    public void castForceField()
    {
        gameObject.transform.position = characterController.transform.position;
        if (gameObject.transform.localScale.x < 1100)
        {
            
            speedForceField += new Vector3(0.35f, 0.35f, 0.35f);
            gameObject.transform.localScale += speedForceField;

            //ps.scale = new Vector3(gameObject.transform.localScale.x / 625, gameObject.transform.localScale.y / 625, gameObject.transform.localScale.z / 4166);
            //magicFog.gameObject.transform.rotation = Quaternion.Euler(10.0f, 0.0f, 0.0f);
        }
    }

    public void decastForceField()
    {
        gameObject.transform.position = characterController.transform.position;
        //Debug.Log(forceField.transform.localScale.x);
        if (gameObject.transform.localScale.x > 10)
        {
            
            speedForceField += new Vector3(0.35f, 0.35f, 0.35f);
            gameObject.transform.localScale -= speedForceField;
            //ps.scale = new Vector3(gameObject.transform.localScale.x / 625, gameObject.transform.localScale.y / 625, gameObject.transform.localScale.z / 4166);

        }
        
        else if (gameObject.transform.localScale.x < 10)
        {
            
            gameObject.SetActive(false);
            magicFog.gameObject.SetActive(false);
        }
        if (gameObject.transform.localScale.x < 200)
        {
            handAlchemic.SetActive(false);
            handAlchemic.GetComponent<Animator>().Play("Idle");
            alchemyEnter.Play("AlchemyEnter");
            directionalLight.color = new Color(0.89f, 0.71f, 0.24f, 255);
        }

    }

    public void OnTriggerEnter(Collider other)
    {

        if (other != null && other.gameObject.layer.ToString() == "7" && isCastingSphere == true)
        {

            if (other.GetComponent<IInventoryItem>().Type == "Ore")
            {
                other.transform.GetChild(0).gameObject.SetActive(true);
                
                other.transform.GetChild(1).gameObject.SetActive(false);
            }
            if (other.GetComponent<IInventoryItem>().Type == "Tree")
            {
                //Debug.Log(other.transform.parent.parent.GetChild(0));
                other.transform.parent.parent.GetChild(0).gameObject.SetActive(true);
                other.transform.parent.parent.GetChild(1).gameObject.SetActive(false);
            }
            if (other.GetComponent<IInventoryItem>().Type == "Grass")
            {
                //Debug.Log(other.transform.parent);
                other.transform.parent.parent.GetChild(0).gameObject.SetActive(true);

                other.transform.parent.parent.GetChild(1).gameObject.SetActive(false);
            }
        }

        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other != null && other.gameObject.layer.ToString() == "7")
        {

            if (other.GetComponent<IInventoryItem>().Type == "Ore")
            {
                other.transform.GetChild(0).gameObject.SetActive(false);
                
                other.transform.GetChild(1).gameObject.SetActive(true);
            }
            if (other.GetComponent<IInventoryItem>().Type == "Tree")
            {
                other.transform.parent.parent.GetChild(0).gameObject.SetActive(false);
                other.transform.parent.parent.GetChild(1).gameObject.SetActive(true);
            }
            if (other.GetComponent<IInventoryItem>().Type == "Grass")
            {
                other.transform.parent.parent.GetChild(0).gameObject.SetActive(false);

                other.transform.parent.parent.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
}
