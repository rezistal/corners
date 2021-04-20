﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardElementCell : MonoBehaviour, IBoardElement
{
    [SerializeField]
    private BoardElementController controller;
    [SerializeField]
    private GameObject hightlight;

    private void Start()
    {
        hightlight.SetActive(false);
    }

    public void Select()
    {
        hightlight.SetActive(true);
    }

    public void DeSelect()
    {
        hightlight.SetActive(false);
    }
}
