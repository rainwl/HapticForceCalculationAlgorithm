using UnityEngine;
using UnityEngine.Serialization;

namespace OCTREE
{
    public class CreateOCtree : MonoBehaviour
    {
        public GameObject[] objects;
        public float nodeMinSize = 1;
        private Octree _octree;

        private void Start()
        {
            _octree = new Octree(objects, nodeMinSize);
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                _octree.RootNode.Draw();
            }
        }
    }
}