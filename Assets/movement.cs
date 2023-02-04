using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movement : MonoBehaviour
{
    [SerializeField] private Transform[] movePositionTransform;
    public Transform characterTransform;
    public int pathCounter = 0;
    private NavMeshAgent navMeshAgent;
    public bool followingCharacter = false;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (followingCharacter)
        {
            navMeshAgent.destination = characterTransform.position;
        } else
        {
            navMeshAgent.destination = movePositionTransform[pathCounter].position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "spheres")
        {
            pathCounter++;
        }
        if (pathCounter == movePositionTransform.Length)
        {
            pathCounter = 0;
        }
    }
}
