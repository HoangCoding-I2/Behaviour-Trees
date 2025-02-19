using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    public delegate Status Tick();
    public Tick ProcesseMethod;

    public Leaf()
    {

    }
    public Leaf(string name, Tick pm)
    {
        Name = name;
        ProcesseMethod = pm;
    }

    public override Status Process()
    {
        if(ProcesseMethod != null)
            return ProcesseMethod();
        return Status.Failure;
    }
}
