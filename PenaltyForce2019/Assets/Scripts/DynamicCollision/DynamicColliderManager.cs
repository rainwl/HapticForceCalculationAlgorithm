using UnityEngine;

namespace DynamicCollision
{
    public class DynamicColliderManager : MonoBehaviour
    {
        private DynamicCollider[] collidersArray;
        private void Start()
        {
            collidersArray = GetComponentsInChildren<DynamicCollider>();
        }
        
        private void Update()
        {
            // At the instant of the first collision
            // the sphere collider position of the driving colliding collection is recorded
            if (DynamicCommon.IsInitialCollision == true && DynamicCommon.IsDictionaryWritten == false)
            {
                foreach (var item in collidersArray)
                {
                    var position = item.transform.position;
                    DynamicCommon.DynamicDictionary.Add(item,position);
                    item.lastPos = position;
                }
                DynamicCommon.IsDictionaryWritten = true;
            }
            
            DynamicCommon.PenaltyForce = Vector3.zero;
            
            foreach (var item in DynamicCommon.CollisionList)
            {
                // Calculate and the direction of the previous frame, reverse or not
                var currentPos = item.transform.position;
                var posDelta = currentPos - item.lastPos;
                item.lastPos = currentPos;
                
                // If it is the reverse of origin
                // then the distance to the original location (in the dictionary) is calculated
                if (!DynamicCommon.Direction(posDelta))
                {
                    foreach (var kvp in DynamicCommon.DynamicDictionary)
                    {
                        if (kvp.Key == item)
                        {
                            var force = kvp.Value - currentPos;
                            DynamicCommon.PenaltyForce += force;
                        }
                    }
                }
                else
                {
                    foreach (var kvp in DynamicCommon.DynamicDictionary)
                    {
                        if (kvp.Key == item)
                        {
                            var force = kvp.Value - currentPos;
                            DynamicCommon.PenaltyForce -= force;
                        }
                    }
                }

            }

            // Reset only if all spheres are not colliding and the departure direction is the same as the origin
            if (DynamicCommon.IsNoCollisions)
            {
                if (TakeOut())
                {
                    DynamicCommon.IsInitialCollision = false;
                    DynamicCommon.IsDictionaryWritten = false;
                    DynamicCommon.DynamicDictionary.Clear();
                    DynamicCommon.CollisionList.Clear();
                    DynamicCommon.IsShadowFollow = true;
                }
            }
        }

        /// <summary>
        /// The direction judgment is made by calculating whether the sum of the distance difference of each sphere
        /// in the active collision of the current frame and the initial frame is greater than 0
        /// </summary>
        /// <returns></returns>
        bool TakeOut()
        {
            var totalDis = Vector3.zero;
            
            if (DynamicCommon.DynamicDictionary == null) return false;

            foreach (var kvp in DynamicCommon.DynamicDictionary)
            {
                for (int i = 0; i < collidersArray.Length; i++)
                {
                    if (kvp.Key == collidersArray[i])
                    {
                        var dis = collidersArray[i].transform.position - kvp.Value;
                        totalDis += dis;
                    }
                }
            }

            var dot = Vector3.Dot(DynamicCommon.OriginVector, totalDis);
            // In the same direction, the representative left
            return dot > 0;
        }
        /// <summary>
        /// Display force size
        /// </summary>
        private void OnGUI()
        {
            GUI.skin.label.fontSize = 50;
            GUI.Label(new Rect(900, 800, 1000, 500), "penalty force: " + DynamicCommon.PenaltyForce);
            GUI.Label(new Rect(900, 900, 1000, 500), "penalty force: " + DynamicCommon.PenaltyForce.magnitude);
            GUI.Label(new Rect(900, 1000, 1000, 500), "IsNoCollisions: " + DynamicCommon.IsNoCollisions);
        }
    }
}
