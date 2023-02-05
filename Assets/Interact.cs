using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    public GameObject pressE;
    public GameObject pressLeftClick;
    private GameObject axe;
    private StarterAssetsInputs _input;
    private bool hasGathered = false;
    private bool haveAxe = false;

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }
    private void Update()
    {
        if (pressE.activeSelf)
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
        haveAxe = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "axe")
        {
            axe = other.gameObject;
            pressE.SetActive(true);
        }
        if (other.tag == "sacred_tree" && haveAxe)
        {
            pressLeftClick.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "axe")
        {
            pressE.SetActive(false);
        }
        if (other.tag == "sacred_tree")
        {
            pressLeftClick.SetActive(false);
        }
    }
}
