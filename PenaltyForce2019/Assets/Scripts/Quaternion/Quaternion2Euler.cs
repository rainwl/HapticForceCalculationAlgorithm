using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Quaternion2Euler : MonoBehaviour
{
    public Quaternion q;
    public Vector3 oldEuler;
    public Vector3 newEuler;

    public Transform oldObj;
    public Transform newObj;
    
    private int i = 1;
    private int j = 2;
    private int k = 3;

    private float epsilon; //Levi-Civita

    private float angle1 = 0;
    private float angle2 = 0;
    private float angle3 = 0;

    public float a1;
    public float a2;
    public float a3;
    
    private float anglePlus;
    private float angleSubtract;

    private bool not_proper = false;


    void Update()
    {
        oldEuler = q.eulerAngles;
        
        if (i == k)
        {
            not_proper = false;
            k = 6 - i - j;
        }
        else
        {
            not_proper = true;
        }

        epsilon = (i - j) * (j - k) * (k - i) * 0.5f;

        float a;
        float b;
        float c;
        float d;

        if (not_proper)
        {
            a = q[0] - q[j];
            b = q[i] + q[k] * epsilon;
            c = q[j] + q[0];
            d = q[k] * epsilon - q[i];
        }
        else
        {
            a = q[0];
            b = q[i];
            c = q[j];
            d = q[k] * epsilon;
        }

        angle2 = Mathf.Acos((2 * ((a * a + b * b) / (a * a + b * b + c * c + d * d)) - 1));
        anglePlus = Mathf.Atan2(b, a);
        angleSubtract = Mathf.Atan2(d, c);
        switch (angle2)
        {
            case 0:
                angle1 = 0;
                angle3 = 2 * anglePlus - angleSubtract;
                break;
            case Mathf.PI * 0.5f:
                angle1 = 0;
                angle3 = 2 * angleSubtract + angle1;
                break;
            default:
                angle1 = anglePlus - angleSubtract;
                angle3 = anglePlus + angleSubtract;
                break;
        }

        if (not_proper)
        {
            angle3 = epsilon * angle3;
            angle2 = angle2 - Mathf.PI * 0.5f;
        }

        a1 = angle1 * Mathf.Rad2Deg;
        a2 = angle2 * Mathf.Rad2Deg;
        a3 = angle3 * Mathf.Rad2Deg;

        newEuler.x = a3;//a1;
        newEuler.y = a2;//a2;
        newEuler.z = a1;//a3;
        
        
        
        
        newObj.transform.eulerAngles = newEuler;
        oldObj.transform.eulerAngles = oldEuler;
    }

    /// <summary>
    /// [0,2*Math.PI]
    /// </summary>
    /// <param name="radian"></param>
    /// <returns></returns>
    private static double RadiansToPi(double radian)
    {
        radian = radian % (2 * Math.PI);
        if (radian < 0)
        {
            radian += 2 * Math.PI;
        }

        return radian;
    }
}