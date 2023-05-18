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
        /// If and only if the first collision,record each collider position
        /// Clear when no collisions
        /// </summary>
        public static Dictionary<DynamicCollider, Vector3> DynamicDictionary { get; set; } =
            new Dictionary<DynamicCollider, Vector3>(80);
    }
}