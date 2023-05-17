using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField]
    private Transform master;

    private void Update()
    {
        if (!CollisionManager.IsSeparation)
        {
            var transShadow = this.transform;
            var transMaster = master.transform;
            transShadow.position = transMaster.position;
            transShadow.rotation = transMaster.rotation;
        }
    }
}
