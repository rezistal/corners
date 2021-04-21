using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainedParameters<T>
{
    private LinkedList<T> chain = new LinkedList<T>();
    private LinkedListNode<T> Pointer;

    public ChainedParameters(List<T> parameters)
    {
        foreach (T t in parameters)
        {
            LinkedListNode<T> node = new LinkedListNode<T>(t);
            chain.AddLast(node);
        }
        Pointer = chain.Last;
    }

    public T Next()
    {
        if (Pointer.Next == null)
        {
            Pointer = chain.First;
            return Pointer.Value;
        }
        Pointer = Pointer.Next;
        return Pointer.Value;
    }
}
