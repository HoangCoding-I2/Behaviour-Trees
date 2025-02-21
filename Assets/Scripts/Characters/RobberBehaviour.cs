using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;


public class RobberBehaviour : BTAgent
{

    [SerializeField] private GameObject diamond;
    [SerializeField] private GameObject van;
    [SerializeField] private GameObject backDoor;
    [SerializeField] private GameObject frontDoor;
    [SerializeField] private GameObject holder;
    [SerializeField] private GameObject waterlillies;

    private const int MoneyThreshold = 500;
    private const int MoneyReward = 200;

    [Range(0, 1000)]
    [SerializeField] private int money = 800;

    private void Start()
    {
        tree = new BehaviourTrees();
        Sequence steal = new Sequence("Steal Something");
        Leaf goToBackDoor = new Leaf("Go To Back Door", GoToBackDoor);
        Leaf hasGotMoney = new Leaf("Has Got Money", HasMoney);
        Leaf goToFrontDoor = new Leaf("Go To Front Door", GoToFrontDoor);
        Leaf goToDiamond = new Leaf("Go To Diamond",GoToDiamond);
        Leaf goToWaterlillies = new Leaf("Go To Waterlillies", GoToWaterlillies);
        Leaf goToVan = new Leaf("Go To Van",GoToVan);
        Selector opendoor = new Selector("Open Door");

        Inverter inverterMoney = new Inverter("Invert Money");
        inverterMoney.AddChild(hasGotMoney);

        opendoor.AddChild(goToFrontDoor);
        opendoor.AddChild(goToBackDoor);


        steal.AddChild(inverterMoney);
        steal.AddChild(opendoor);
        steal.AddChild(goToDiamond);
        steal.AddChild(goToWaterlillies);
        steal.AddChild(goToVan);
        tree.AddChild(steal);

        tree.PrintTree();
    }


    public Node.Status HasMoney()
    {
        return money < MoneyThreshold ? Node.Status.Failure : Node.Status.Success;
    }
    public Node.Status GoToFrontDoor()
    {
        return GoToDoor(frontDoor);
    }
    public Node.Status GoToBackDoor()
    {
        return GoToDoor(backDoor);
    }
    private Node.Status GoToDoor(GameObject door)
    {
        Node.Status status = GoToLocation(door.transform.position);
        if (status == Node.Status.Success)
        {
            Lock doorLock = door.GetComponent<Lock>();
            if (doorLock != null && !doorLock.isLocked)
            {
                door.SetActive(false);
                return Node.Status.Success;
            }
            return Node.Status.Failure;
        }
        return status;
    }

    public Node.Status GoToWaterlillies()
    {
        return GetWaterlillies(waterlillies);
    }
    private Node.Status GetWaterlillies(GameObject waterlillie)
    {
        Node.Status status = GoToLocation(waterlillie.transform.position);
        if (status == Node.Status.Success)
        {
            waterlillies.transform.parent = this.gameObject.transform;
            return Node.Status.Success;
        }
        return status;
    }
    private Node.Status GetDiamond(GameObject diamond)
    {
        Node.Status status = GoToLocation(diamond.transform.position);
        if (status == Node.Status.Success)
        {
            holder.transform.parent = this.gameObject.transform;
            holder.GetComponent<Rotate>().enabled = false;
            return Node.Status.Success;
        }
        return status;
    }
    public Node.Status GoToDiamond()
    {
        return GetDiamond(diamond);
    }
    public Node.Status GoToVan()
    {
        Node.Status status = GoToLocation(van.transform.position);
        if (status == Node.Status.Success)
        {
            money += MoneyReward;
            diamond.SetActive(false);
        }
        return status;
    }


}
