using System;
using System.Collections.Generic;
using UnityEngine;

namespace DynamicCollision
{
    public class DynamicColliderManager : MonoBehaviour
    {
        private DynamicCollider[] dyColliders;



        private void Start()
        {
            dyColliders = GetComponentsInChildren<DynamicCollider>();
        }

        private void Update()
        {
            if (DynamicCommon.IsInitialCollision == true && DynamicCommon.IsDictionaryWritten == false)
            {
                foreach (var item in dyColliders)
                {
                    DynamicCommon.DynamicDictionary.Add(item,item.transform.position);
                }
                DynamicCommon.IsDictionaryWritten = true;
            }
            
            
        }
    }
}
