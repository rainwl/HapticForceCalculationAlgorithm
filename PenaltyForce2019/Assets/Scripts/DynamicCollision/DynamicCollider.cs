using System;
using UnityEngine;

namespace DynamicCollision
{
    /// <summary>
    /// before write this script , scene could be run at 5800 fps
    /// </summary>
    public class DynamicCollider : MonoBehaviour
    {

        public bool isColliding = false;
        private void OnTriggerEnter(Collider other)
        {
            // If and only if the first collision
            if (DynamicCommon.IsInitialCollision == false)
            {
                DynamicCommon.IsInitialCollision = true;
            }

            isColliding = true;
        }

        private void OnTriggerStay(Collider other)
        {
            isColliding = true;
        }

        private void OnTriggerExit(Collider other)
        {
            isColliding = false;
        }
    }
}
