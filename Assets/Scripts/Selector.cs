using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    public Selector(string name)
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

            case Status.Success:
                CurrentChild = 0;
                return Status.Success;

            case Status.Failure:
                CurrentChild++;
                break;
        }
        if (CurrentChild >= Children.Count)
        {
            CurrentChild = 0;
            return Status.Failure;
        }
        return Status.Running;
    }
}
