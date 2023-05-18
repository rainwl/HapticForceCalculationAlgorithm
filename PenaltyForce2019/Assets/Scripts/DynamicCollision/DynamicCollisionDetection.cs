using UnityEngine;

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
            DynamicCommon.IsNoCollisions = true;
            foreach (var col in dyColliders)
            {
                if (!col.isColliding)
                {
                    continue;
                }

                DynamicCommon.IsNoCollisions = false;
                break;
            }
        }
    }
}
