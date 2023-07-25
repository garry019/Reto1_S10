using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI;

public class GhostMove : MonoBehaviour
{

    
    [SerializeField] private Transform Player;
    public Vector3[] targetPositions;
    private NavMeshAgent agent;

    bool chase = true;
    bool scatter = false;
    int nextPos = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (targetPositions.Length < 3)
        {
            Debug.Log("Debes asignar las posiciones.");
        }
        else
        {
            StartCoroutine(GhostChase());
            StartCoroutine(GhostScatter());
        }
        
    }

    private void Update()
    {
        //Verificación de posiciones asignadas
        if (chase == true && targetPositions.Length == 3)
        {
            agent.destination = Player.position;
            nextPos = 0;
        }

        if (scatter == true)
        {
            //Cambio de posición
            if (nextPos <= 2)
            {
                agent.destination = targetPositions[nextPos];

                if (agent.transform.position.x == targetPositions[nextPos].x && agent.transform.position.z == targetPositions[nextPos].z)
                {
                    nextPos++;
                    agent.destination = targetPositions[nextPos];
                }
            }
            else {
                nextPos = 0;
            }
        }
    }

    public void Chase()
    {
        chase = true;
        scatter = false;
    }

    public void Scatter()
    {
        chase = false;
        scatter = true;
    }

    private IEnumerator GhostChase()
    {

        while (true)
        {
            yield return new WaitUntil(() => chase == true);
            yield return new WaitForSeconds(20);
            Scatter();
        }
    }

    private IEnumerator GhostScatter()
    {
        while (true)
        {
            yield return new WaitUntil(() => scatter == true);
            yield return new WaitForSeconds(7);
            Chase();
        }
    }
}
