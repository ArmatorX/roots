using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Teleport : MonoBehaviour
{
    public GameObject finalSphere;
    public GameObject chobi;
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {   
            if (!other.gameObject.GetComponent<ThirdPersonController>().AxeGameObject.activeSelf) return;
            chobi.GetComponent<movement>().movePositionTransform = new Transform[1];
            chobi.GetComponent<movement>().movePositionTransform[0] = finalSphere.transform;
            chobi.GetComponent<movement>().pathCounter = 0;
            chobi.GetComponent<movement>().TpChobiEnd(finalSphere.transform .position);
            Destroy(gameObject);
        }
    }
}
