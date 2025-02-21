using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class Sequence : Node
{
    public Sequence(string name)
    {
        Name = name;
    }
    public override Status Process()
    {

        var childStatus = Children[CurrentChild].Process();

        switch (childStatus)
        {
            case Status.Running:
                return Status.Running;

            case Status.Failure:
                CurrentChild = 0;
                return Status.Failure;

            case Status.Success:
                CurrentChild++;
                break;
        }

        if (CurrentChild >= Children.Count)
        {
            CurrentChild = 0;
            return Status.Success;
        }

        return Status.Running;
    }
}
