using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DynamicCollision
{
    public class DynamicCollisionDetection : MonoBehaviour
    {
        private DynamicCollider[] dyColliders;

        private void Start()
        {
            dyColliders = GetComponentsInChildren<DynamicCollider>();
        }

        private void Update()
        {
            // default no collide
            var noCollisions = true;
        
            foreach (var col in dyColliders)
            {
                if (!col.isColliding)
                {
                    continue;
                }
                noCollisions = false;
                break;
            }
            
            // if all colliders has no collision event
            if (noCollisions)
            {
                DynamicCommon.IsInitialCollision = false;
                DynamicCommon.IsDictionaryWritten = false;
                DynamicCommon.DynamicDictionary.Clear();
            }
        }

        
    }
}
