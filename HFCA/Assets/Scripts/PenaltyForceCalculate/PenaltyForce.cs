using System.Collections.Generic;
using UnityEngine;

namespace PenaltyForceCalculate
{
    public class PenaltyForce : MonoBehaviour
    {
        [SerializeField] private Transform staticObj;
        [SerializeField] private Transform dynamicObj;
        
        public Vector3 penaltyForce;
        
        private readonly List<SphereCollider> _staticList = new();
        private readonly List<SphereCollider> _dynamicList = new();

        /// <summary>
        /// Initializing the List<SphereCollider />
        /// </summary>
        private void Start()
        {
            foreach (Transform child in staticObj)
            {
                var sc = child.GetComponent<SphereCollider>();
                if (sc is not null)
                {
                    _staticList.Add(sc);
                }
            }            
            foreach (Transform child in dynamicObj)
            {
                var sc = child.GetComponent<SphereCollider>();
                if (sc is not null)
                {
                    _dynamicList.Add(sc);
                }
            }
        }

        private void Update()
        {
            foreach (var sphere1 in _dynamicList)
            {
                foreach (var sphere2 in _staticList)
                {
                    var isColliding = Physics.CheckSphere(sphere1.transform.position, sphere1.radius + sphere2.radius);
                    if (!isColliding) continue;
                    var force = CalculateCommon.TotalForce(sphere1, sphere2);
                    penaltyForce += force;
                }
            }
        }
    }
}