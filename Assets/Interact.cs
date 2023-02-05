using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    public GameObject pressE;
    private GameObject axe;
    private StarterAssetsInputs _input;
    private bool hasGathered = false;

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }
    private void Update()
    {
        if (pressE.active)
        {
            if (_input.interact)
            {
                pressE.SetActive(false);
                StartCoroutine(removeAxe(axe));
                hasGathered = true;
            }
            gameObject.GetComponent<ThirdPersonController>().Gather();
        } else if (hasGathered) 
        { 
            gameObject.GetComponent<ThirdPersonController>().Gather();
        }
    }

    IEnumerator removeAxe(GameObject axe)
    {
        yield return new WaitForSeconds(1);
        axe.SetActive(false);
        gameObject.GetComponent<ThirdPersonController>().ActivateAxe();
        hasGathered = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "axe")
        {
            axe = other.gameObject;
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
