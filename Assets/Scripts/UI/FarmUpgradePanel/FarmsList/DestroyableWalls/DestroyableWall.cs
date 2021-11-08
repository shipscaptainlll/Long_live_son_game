using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    [SerializeField] InteractionController InteractionController;
    [SerializeField] int WallNumber;

    public event Action<int> WallWasDestroyed = delegate { };
    void Start()
    {
        InteractionController.UsedBlastOnBoulder += DestroyWall;
    }

    void DestroyWall(GameObject wallPartContacted)
    {
        if (CheckThisWallContacted(wallPartContacted))
        {
            HideWall();
            InteractionController.UsedBlastOnBoulder -= DestroyWall;
            NotifyFarm();
        }
    }

    bool CheckThisWallContacted(GameObject wallPartContacted)
    {
        return (wallPartContacted.transform.parent == transform);
    }

    void NotifyFarm()
    {
        if (WallWasDestroyed != null)
        {
            WallWasDestroyed(WallNumber);
        }
    }

    void HideWall()
    {
        gameObject.SetActive(false);
    }
}
