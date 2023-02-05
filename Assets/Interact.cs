using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject pressE;

    private void Update()
    {
        if (pressE.active)
        {
            gameObject.GetComponent<ThirdPersonController>().Gather();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "axe")
        {
            pressE.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "axe")
        {
            pressE.SetActive(false);
        }
    }
}
