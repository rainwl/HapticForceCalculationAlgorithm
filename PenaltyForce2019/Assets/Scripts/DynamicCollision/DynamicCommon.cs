using System.Collections.Generic;
using UnityEngine;

namespace DynamicCollision
{
    public class DynamicCommon : MonoBehaviour
    {
        /// <summary>
        /// If and only if the first collision
        /// Clear when no collisions
        /// </summary>
        public static bool IsInitialCollision { get; set; } = false;


        /// <summary>
        /// When IsInitialCollision is true ,write the data in Dictionary,once
        /// Clear when no collisions
        /// </summary>
        public static bool IsDictionaryWritten { get; set; } = false;


        /// <summary>
        /// Determine if the scene is free of any collisions
        /// </summary>
        public static bool IsNoCollisions { get; set; } = true;
        
        
        /// <summary>
        /// If and only if the first collision,record each collider position
        /// Clear when no collisions
        /// </summary>
        public static Dictionary<DynamicCollider, Vector3> DynamicDictionary { get; set; } =
            new Dictionary<DynamicCollider, Vector3>(80);

        /// <summary>
        /// The sphere collider involved in the collision
        /// </summary>
        public static List<DynamicCollider> CollisionList { get; set; } = new List<DynamicCollider>(80);
        
        /// <summary>
        /// Origin Vector
        /// </summary>
        public static Vector3 OriginVector { get; set; } = Vector3.zero;

        
        public static Vector3 PenaltyForce { get; set; }

        /// <summary>
        /// if true , the same direction with OriginVector
        /// else , reverse
        /// </summary>
        /// <param name="frameDirection"></param>
        /// <returns></returns>
        public static bool Direction(Vector3 frameDirection)
        {
            var x = Vector3.Dot(OriginVector, frameDirection);
            return x > 0;//same direction
            // var angle = Mathf.Acos(Vector3.Dot(OriginVector, frameDirection));
            // return angle < Mathf.PI / 2;
        }
        
        
    }
}