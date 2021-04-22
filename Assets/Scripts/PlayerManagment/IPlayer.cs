﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    Dictionary<(int x, int y), BoardElementController> Figures { get; }
    List<(int x, int y)> StartCondition { get; }
    GameObject Prefab { get; }
    string Name { get; }
    Color Color { get; }
}