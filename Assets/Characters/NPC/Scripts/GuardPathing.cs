using UnityEngine;
using System.Collections;

public class GuardPathing : MonoBehaviour
{
    bool walkPatrol = true;


    Vector3 destinationPoint;
    NPCPathing pathing;
    NavMeshAgent navAgent;

    void Start()
    {
        destinationPoint = transform.position;
        pathing = GetComponent<NPCPathing>();
        navAgent = GetComponent<NavMeshAgent>();

        SetDestinationToClosest();
    }

    void Update ()
    {
        navAgent.SetDestination(destinationPoint);

        if(Mathf.Abs(destinationPoint.x - transform.position.x) < 0.5f && Mathf.Abs(destinationPoint.z - transform.position.z) < 0.5f)
        {
            SetDestinationToNext();
        }
	}

    void SetDestinationToNext()
    {
        for(int i=0; i < pathing.pathNodes.Count; i++)
        {
            if (pathing.pathNodes[i] == destinationPoint)
            {
                if(pathing.cyclic)
                    destinationPoint = pathing.pathNodes[(i + 1) % pathing.pathNodes.Count];
                else if (i < pathing.pathNodes.Count - 1)
                    destinationPoint = pathing.pathNodes[i + 1];

                return;
            }
        }
    }

    void SetDestinationToClosest()
    {
        if (pathing.pathNodes.Count > 0)
            destinationPoint = pathing.pathNodes[0];

        foreach (Vector3 v in pathing.pathNodes)
        {
            if (Vector3.Distance(transform.position, v) < Vector3.Distance(transform.position, destinationPoint))
            {
                destinationPoint = v;
            }
        }
    }
}
