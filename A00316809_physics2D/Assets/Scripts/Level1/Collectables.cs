using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour, IItem
{
    public static event Action<int> OnGemCollect;
    public int value = 5; 

    public void Collect()
    {
        OnGemCollect.Invoke(value);
        Destroy(gameObject);
    }

    
}
