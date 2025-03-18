using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D obj)
    {
        IItem item = obj.gameObject.GetComponent<IItem>();
        if(item != null )
        {
            item.Collect();
        }
    }
}
