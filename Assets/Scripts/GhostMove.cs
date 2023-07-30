using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GhostMove : MonoBehaviour
{

    
    [SerializeField] private Transform Player;
    [SerializeField] public float ChaseTime;
    [SerializeField] public float ScatterTime;
    public PlayerEvents script;


    public Vector3[] targetPositions;
    private NavMeshAgent agent;
    public bool chase = false;
    private int nextPos = 0;
    private bool scatter;

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
            if (chase == false)
            {
                scatter = true;
            }
            StartCoroutine(GhostChase());
            StartCoroutine(GhostScatter());
        }
    }

    private void Update()
    {
        if (script.playScene == true) {
            //Verificaci�n de posiciones asignadas
            if (chase == true && targetPositions.Length == 3)
            {
                agent.destination = Player.position;
                nextPos = 0;
            }

            if (scatter == true)
            {
                //Cambio de posici�n
                if (nextPos < 2)
                {
                    agent.destination = targetPositions[nextPos];

                    if (agent.transform.position.x == targetPositions[nextPos].x && agent.transform.position.z == targetPositions[nextPos].z)
                    {
                        nextPos++;
                        agent.destination = targetPositions[nextPos];
                    }
                }
                else
                {
                    nextPos = 0;
                }
            }
        }
        else {
            agent.destination = agent.transform.localPosition;
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
        while (true && script.playScene == true)
        {
            yield return new WaitUntil(() => chase == true);
            yield return new WaitForSeconds(ChaseTime);
            Scatter();
        }
    }

    private IEnumerator GhostScatter()
    {
        while (true && script.playScene == true)
        {
            yield return new WaitUntil(() => scatter == true);
            yield return new WaitForSeconds(ScatterTime);
            Chase();
        }
    }
}
