using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTrees : Node
{
    
    public BehaviourTrees()
    {
        Name = "Tree";
    }
    public BehaviourTrees(string name)
    {
        Name = name;
    }
    public void PrintTree()
    {
        PrintNode(this, 0);
    }

    private void PrintNode(Node node, int level)
    {
        string indent = new string(' ', level * 2);
        Debug.Log($"{indent}{node.Name}");

        foreach (var child in node.Children)
        {
            PrintNode(child, level + 1);
        }
    }

}
