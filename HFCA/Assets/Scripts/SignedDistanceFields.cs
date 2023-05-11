using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignedDistanceFields : MonoBehaviour
{
    [SerializeField] private Texture3D sdf;
    // Start is called before the first frame update
    void Start()
    {
        var x = sdf.GetPixels();
        Color[] test = x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
