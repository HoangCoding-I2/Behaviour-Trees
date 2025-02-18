using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberBehaviour : MonoBehaviour
{
    private BehaviourTrees tree;
    private void Start()
    {
        tree = new BehaviourTrees();
        Node steal = new Node("Steal Something");
        Node goToDiamond = new Node("Go To Diamond");
        Node goToVan = new Node("Go To Van");

        steal.AddChild(goToDiamond);
        steal.AddChild(goToVan);
        tree.AddChild(steal);

        Node eat = new Node("Eat something");
        Node pizza = new Node("Go To Pizza shop");
        Node buy = new Node("Buy pizza");

        eat.AddChild(pizza);
        eat.AddChild(buy);
        tree.AddChild(eat);

        tree.PrintTree();

    }
   

}
