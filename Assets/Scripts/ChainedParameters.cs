using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainedParameters<T>
{
    private LinkedList<T> chain = new LinkedList<T>();
    private LinkedListNode<T> pointer;

    public T Current { get => pointer.Value; }
    public T First { get => chain.First.Value; }
    public T Last { get => chain.Last.Value; }
    
    public ChainedParameters(List<T> parameters)
    {
        foreach (T t in parameters)
        {
            LinkedListNode<T> node = new LinkedListNode<T>(t);
            chain.AddLast(node);
        }
        pointer = chain.First;
    }

    public T GetNext()
    {
        if (pointer.Next == null)
        {
            return chain.First.Value;
        }
        return pointer.Next.Value;
    }

    public void SetNext()
    {
        if (pointer.Next != null)
        {
            pointer = pointer.Next;
        }
        else
        {
            pointer = chain.First;
        }

    }

    public IEnumerable<T> Params()
    {
        LinkedListNode<T> localPointer = chain.First;
        while (localPointer != null)
        {
            yield return localPointer.Value;
            localPointer = localPointer.Next;
        }
        yield break;
    }
}
