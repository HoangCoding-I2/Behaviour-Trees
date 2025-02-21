using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;


public class BTAgent : MonoBehaviour
{
    public enum ActionState { Idle, Working }

    public ActionState state = ActionState.Idle;
    public Node.Status treeStatus = Node.Status.Running;
    public NavMeshAgent agent;
    public BehaviourTrees tree;

    private const float TargetDistanceThreshold = 2.0f;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        tree = new BehaviourTrees();
    }
    private void Update()
    {
        if(treeStatus != Node.Status.Success)
            treeStatus = tree.Process();
    }
    public Node.Status GoToLocation(Vector3 destination)
    {
        float distanceToTarget = Vector3.Distance(destination, this.transform.position);
        if (state == ActionState.Idle)
        {
            agent.SetDestination(destination);
            state = ActionState.Working;
        }
        else if (Vector3.Distance(agent.pathEndPosition, destination) >= TargetDistanceThreshold)
        {
            state = ActionState.Idle;
            return Node.Status.Failure;
        }
        else if (distanceToTarget < TargetDistanceThreshold)
        {
            state = ActionState.Idle;
            return Node.Status.Success;
        }
        return Node.Status.Running;
    }

}
