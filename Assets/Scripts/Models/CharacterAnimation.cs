using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator anim;
    public Animator treeAnimator;
    public Animator alchemyEnter;
    public MouseRotation mouseRotation;
    public PersonMovement personMovement;
    public bool beingBusy = false;
    public GameObject HUD;
    public GameObject shopInventory;

    public int IS_MINED = 0;
    public int IS_ALCHEMY_MINED = 0;
    public ParticleSystem particleSystemMined;

    //HealthBar
    public Health itemHealth;

    public InteractionController interactionController;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(anim);
        if (Input.GetMouseButtonDown(0) && beingBusy == false && HUD.GetComponent<CanvasGroup>().alpha == 0 && !shopInventory.activeInHierarchy)
        {
            anim.SetBool("isMining", true);
            IS_MINED = 1;
        }
        if (Input.GetMouseButtonDown(1) && beingBusy == false && !HUD.activeInHierarchy && !shopInventory.activeInHierarchy)
        {
            //alchemyEnter.enabled = true;
            
            //alchemyEnter.Play("New State");
            anim.SetBool("isMining", true);
            IS_ALCHEMY_MINED = 1;
        }
        if (Input.GetMouseButtonUp(0))
        {
            IS_MINED = 0;
            anim.SetBool("isMining", false);
        }
        if (Input.GetMouseButtonUp(1) && beingBusy == false && !HUD.activeInHierarchy && !shopInventory.activeInHierarchy)
        {
            //alchemyEnter.enabled = false;
            
            IS_ALCHEMY_MINED = 0;
            anim.SetBool("isMining", false);
        }
        if (Input.GetKeyDown (KeyCode.W))
        {
            anim.SetBool("isMoving", true);
            //anim.Crossfade("",1);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("isMoving", false);
            //anim.Crossfade("",1);
        }
    }

    void detectItem()
    {
        mouseRotation.detectToItems();
    }

    void collectItem()
    {
        mouseRotation.collectToItems();
    }

    public void seeingOre()
    {
        beingBusy = true;
        anim.SetBool("seeingOre", true);
        mouseRotation.mouseSensitivity = 10;
        personMovement.speed = 0.2f;
    }
    public void seeingTree()
    {
        
        beingBusy = true;
        anim.SetBool("seeingTree", true);
        mouseRotation.mouseSensitivity = 10;
        personMovement.speed = 0.2f;
    }
    public void seeingGrass()
    {
        beingBusy = true;
        anim.SetBool("seeingGrass", true);
        mouseRotation.mouseSensitivity = 10;
        personMovement.speed = 0.2f;
    }

    public void triggerTreeAnimation()
    {
        mouseRotation.TriggerFallingAnimation();
    }

    public void triggerGrassAnimation()
    {
        
        mouseRotation.TriggerFallingAnimation();
    }
    public void mineAlchemyResources()
    {
        mouseRotation.mineAlchemyResources();
        if (IS_ALCHEMY_MINED == 1) { alchemyEnter.Play("New State");  anim.speed = 0.01f; }
        
    }
    public void stopMineAlchemyResources()
    {
        mouseRotation.stopMineAlchemyResources();
        anim.speed = 1f;
    }

    public void startCollectAlchemyResources()
    {
        mouseRotation.startCollectAlchemyResources();
        /*if (IS_ALCHEMY_MINED == 1)
        {
            anim.speed = 0.001f;
            mouseRotation.oreDetectedInstanceGO.GetComponent<Animator>().speed = 2;
            mouseRotation.oreDetectedInstanceGO.GetComponent<Animator>().Play("Rock_Dissapper");
        }*/
    }

    public void stopCollectAlchemyResources()
    {
        anim.speed = 1f;
        
    }

    public void startParticles()
    {
        mouseRotation.startParticles();
        /*if (IS_ALCHEMY_MINED == 1)
        {
            anim.speed = 0.03f;
            personMovement.speed = 10;
            mouseRotation.mouseSensitivity = 100;
        }*/
    }
    public void stopParticles()
    {
        mouseRotation.stopParticles();
        /*if (IS_ALCHEMY_MINED == 1)
        {
            anim.speed = 0.03f;
            particleSystemMined.Stop();
        }*/
    }
    public void StopMiningOre()
    {
        anim.speed = 1;
        IS_ALCHEMY_MINED = 0;
        anim.SetBool("seeingOre", false);
        beingBusy = false;
        mouseRotation.mouseSensitivity = 100;
        personMovement.speed = 10;
    }
    public void StopChoppingTree()
    {
        anim.SetBool("seeingTree", false);
        beingBusy = false;
        mouseRotation.mouseSensitivity = 100;
        personMovement.speed = 10;
    }
    public void StopCuttingGrass()
    {
        anim.SetBool("seeingGrass", false);
        beingBusy = false;
        mouseRotation.mouseSensitivity = 100;
        personMovement.speed = 10;
    }

    public void stopMunallyCollectingOre()
    {
        interactionController.stopBoulderInteractManually();
    }

    public void stopMunallyCollectingTree()
    {
        interactionController.stopTreeInteractManually();
    }
}
