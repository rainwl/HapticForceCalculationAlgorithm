using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private SingleCollision[] childColliders;
    private float finalDistance;
    public Color textColor;

    /// <summary>
    /// when first collider colliding,Cylinder collection should be separated
    /// a cylinder collection mesh should be render at current position
    /// </summary>
    public static bool IsSeparation { get; set; } = false;

    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            IsSeparation = true;
            Debug.Log("Collision Enter");
        }
    }

    private void OnCollisionExit(UnityEngine.Collision other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            IsSeparation = false;
        }
    }

    private void Start()
    {
        childColliders = GetComponentsInChildren<SingleCollision>();
    }

    /// <summary>
    /// we need to change this iterate
    /// dont iterate each child in childColliders
    /// just iterate the child who participate in collision
    /// and we should use dictionary or other high-performance grammar
    /// </summary>
    private void Update()
    {
        finalDistance = 0f;
        foreach (var child in childColliders)
        {
            finalDistance += child.Distance;
        }
    }
    /// <summary>
    /// use if to judge Tag,is a loss-performance way ,modify later
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            foreach (var child in childColliders)
            {
                child.IsFirstEnter = false;
                //finalDistance = 0;
            }
            
        }
    }

    private void OnGUI()
    {
        GUI.color = textColor;
        GUI.skin.label.fontSize = 50;
        GUI.Label(new Rect(900, 800, 1000, 500), "penalty force: " + finalDistance);
        //GUI.Label(new Rect(900, 900, 1000, 500), "last force: " + lastDistance);
    }
}