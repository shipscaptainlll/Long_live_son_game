using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuest
{
    event Action<int, int> questCompleted;

    void getReward();
}

