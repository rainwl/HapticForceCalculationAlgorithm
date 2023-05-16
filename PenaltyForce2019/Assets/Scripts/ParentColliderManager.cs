﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentColliderManager : MonoBehaviour
{
    private ColliderManager[] childColliders;
    private float totalDistance;

    private void Start()
    {
        childColliders = GetComponentsInChildren<ColliderManager>();
    }

    private void Update()
    {
        totalDistance = 0f;
        foreach (var child in childColliders) {
            totalDistance += child.totalDistance;
        }

    }
    private void OnGUI()
    {
        GUI.skin.label.fontSize = 50;
        GUI.Label(new Rect(900, 800, 1000, 500), "penalty force: " + totalDistance);
        //GUI.Label(new Rect(900, 900, 1000, 500), "last force: " + lastDistance);
    }
}