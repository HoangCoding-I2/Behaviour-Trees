using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public enum Status { Success, Running, Failure }
    public Status NodeStatus { get; set; }
    public List<Node> Children { get; set; } = new List<Node>();
    public int CurrentChild { get; set; } = 0;
    public string Name { get; set; }

    public Node()
    {

    }
    public Node(string name)
    {
        Name = name;
    }
    public void AddChild(Node node)
    {
        if (node != null)
        {
            Children.Add(node);
        }
    }
}
