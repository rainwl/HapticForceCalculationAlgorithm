using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace PenaltyForceCalculate
{
    public class PenaltyForce : MonoBehaviour
    {
        [SerializeField] private Transform staticObj;
        [SerializeField] private Transform dynamicObj;
        
        public Vector3 penaltyForce = Vector3.zero;
        public Vector3 lastForce = Vector3.zero;
        private float _penaltyForce;
        private float _lastForce;
        
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
            var colliding = false;
            foreach (var sphere1 in _dynamicList)
            {
                foreach (var sphere2 in _staticList)
                {
                    if (sphere1.bounds.Intersects(sphere2.bounds))
                    {
                        colliding = true;
                        var force = CalculateCommon.PenaltyForce(sphere1, sphere2);
                        penaltyForce += force;
                        lastForce = penaltyForce;
                    }
                }
            }

            if (!colliding)
            {
                penaltyForce = Vector3.zero;
            }
        }

        private void OnGUI()
        {
            GUI.skin.label.fontSize = 50;
            GUI.Label(new Rect(900, 800, 1000, 500), "penalty force: " + penaltyForce);
            GUI.Label(new Rect(900, 900, 1000, 500), "last force: " + lastForce);
        }
    }
}