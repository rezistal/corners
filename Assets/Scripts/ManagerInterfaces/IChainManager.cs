using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChainManager <out T>
{
    T Next();
}
