using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DetectionRange : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI warning;
    private void Start()
    {
        warning.text = "";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Destructable"))
        {
            warning.text = "Detected black hole nearby";
            StartCoroutine(waitTime());
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
       
    //    warning.text = " ";
        
    //}

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(2.3f);
        warning.text = "";
    }
}
