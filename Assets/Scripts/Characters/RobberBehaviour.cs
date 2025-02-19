using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;


public class RobberBehaviour : MonoBehaviour
{
    [SerializeField] GameObject diamond;
    [SerializeField] GameObject van;
    NavMeshAgent agent;
    private BehaviourTrees tree;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        tree = new BehaviourTrees();
        Node steal = new Node("Steal Something");
        Leaf goToDiamond = new Leaf("Go To Diamond",GoToDiamond);
        Leaf goToVan = new Leaf("Go To Van",GoToVan);

        steal.AddChild(goToDiamond);
        steal.AddChild(goToVan);
        tree.AddChild(steal);

        tree.PrintTree();

        tree.Process();

        
    }
    public Node.Status GoToDiamond()
    {
        agent.SetDestination(diamond.transform.position);
        return Node.Status.Success;
    }
    public Node.Status GoToVan()
    {
        agent.SetDestination(van.transform.position);
        return Node.Status.Success;
    }

}
