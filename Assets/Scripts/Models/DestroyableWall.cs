using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    [SerializeField] InteractionController InteractionController;

    void Start()
    {
        InteractionController.UsedBlastOnBoulder += DestroyWall;
    }

    void DestroyWall(GameObject wallPartContacted)
    {
        if (CheckThisWallContacted(wallPartContacted))
        {
            HideWall();
        }
    }

    bool CheckThisWallContacted(GameObject wallPartContacted)
    {
        return (wallPartContacted.transform.parent == transform);
    }

    void HideWall()
    {
        gameObject.SetActive(false);
    }
}
