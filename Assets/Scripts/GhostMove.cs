using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMove : MonoBehaviour
{

    private NavMeshAgent agent;
    [SerializeField] private Transform movePositionTransform;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        attack();
    }

    public void attack()
    {
        agent.destination = movePositionTransform.position;
    }

}
