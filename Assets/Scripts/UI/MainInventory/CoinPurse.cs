using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPurse : MonoBehaviour
{
    public int coinAmmount;
    public int CoinAmmount
    {
        get { return coinAmmount; }
        set { coinAmmount = value; }
    }

    public void AddAmmount(int Ammount)
    {
        coinAmmount += Ammount;
    }
}
