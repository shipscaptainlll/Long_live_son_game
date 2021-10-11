using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnimation : MonoBehaviour
{
    private Animator treeAnimation;

    // Start is called before the first frame update
    void Start()
    {
        treeAnimation = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerTreeFallingAnimation()
    {
        treeAnimation.Play("TreeUnderAtack");
    }
}
