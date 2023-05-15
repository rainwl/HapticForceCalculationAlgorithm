using Unity.Mathematics;
using UnityEngine;

namespace PenaltyForceCalculate
{
    public static class CalculateCommon
    {
        /// <summary>
        /// Calculate the penalty force between the two spheres
        /// </summary>
        /// <param name="sphere1"></param>
        /// <param name="sphere2"></param>
        /// <returns>penalty force</returns>
        public static Vector3 TotalForce(SphereCollider sphere1, SphereCollider sphere2)
        {
            var volume = Volume(sphere1.radius, sphere2.radius, Vector3.Distance(sphere1.center, sphere2.center));
            var force = ForceOfPenalty(volume, sphere1.center, sphere2.center);
            return force;
        }
    
        /// <summary>
        /// Individual penalty force
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="center1"></param>
        /// <param name="center2"></param>
        /// <returns></returns>
        private static Vector3 ForceOfPenalty(float volume, Vector3 center1, Vector3 center2)
        {
            var force = volume * (center1 - center2);
            return force;
        }

        /// <summary>
        /// Total intersection volume for two spheres
        /// </summary>
        /// <param name="r1">Radius of sphere1</param>
        /// <param name="r2">Radius of sphere2</param>
        /// <param name="d">The distance between the centers of two spheres</param>
        /// <returns>Intersection Volume</returns>
        private static float Volume(float r1, float r2, float d)
        {
            var volume = (math.PI * (r1 + r2 - d) * (r1 + r2 - d) *
                          (d * d + 2 * d * r2 - 3 * r2 * r2 + 2 * d * r1 + 6 * r1 * r2 - 3 * r1 * r1)) / (12 * d);
            return volume;
        }
    }
}