using UnityEngine;

namespace DynamicCollision
{
    /// <summary>
    /// before write this script , scene could be run at 5800 fps
    /// </summary>
    public class DynamicCollider : MonoBehaviour
    {
        /// <summary>
        /// All collision states in the scene are detected
        /// </summary>
        public bool isColliding = false;

        /// <summary>
        /// The frame motion direction when the sphere collider leaves the collision collection
        /// which is used to determine whether it is close or far away
        /// and whether to perform the remove operation
        /// </summary>
        private Vector3 previousPos;
        private Vector3 currentPos;

        /// <summary>
        /// The position of the previous frame
        /// </summary>
        public Vector3 lastPos;
        private void OnTriggerEnter(Collider other)
        {
            // If and only if the first collision
            if (DynamicCommon.IsInitialCollision == false)
            {
                DynamicCommon.IsInitialCollision = true;
                DynamicCommon.IsShadowFollow = false;
                // OriginVector
                var posFirst = transform.position;
                var posOther = other.transform.position;
                DynamicCommon.OriginVector = posFirst - posOther;
            }
            isColliding = true;
        }

        private void OnTriggerStay(Collider other)
        {
            isColliding = true;
            
            if (!DynamicCommon.CollisionList.Contains(this))
            {
                DynamicCommon.CollisionList.Add(this);
            }

            // for Exit calculate
            previousPos = transform.position;
        }

        private void OnTriggerExit(Collider other)
        {
            isColliding = false;
            currentPos = transform.position;
            
            var dir = currentPos - previousPos;
            //If the collider exits in the same direction with origin, remove it
            if (DynamicCommon.Direction(dir))
            {
                DynamicCommon.CollisionList.Remove(this);
            }
        }
    }
}
