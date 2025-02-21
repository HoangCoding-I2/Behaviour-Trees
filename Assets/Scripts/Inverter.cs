using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class Inverter : Node
{
    public Inverter(string name)
    {
        Name = name;
    }
    public override Status Process()
    {

        if (Children == null || Children.Count == 0)
        {
            return Status.Failure; // Hoặc một trạng thái phù hợp khác
        }

        var childStatus = Children[0].Process();

        switch (childStatus)
        {
            case Status.Running:
                return Status.Running;

            case Status.Failure:
                CurrentChild = 0;
                return Status.Success;

            case Status.Success:
                return Status.Failure;

            default:
                return Status.Running; // Hoặc một trạng thái phù hợp khác
        }
    }
}
