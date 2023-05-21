using UnityEngine;

namespace OCTREE
{
    public class CreateOCtree : MonoBehaviour
    {
        public GameObject[] worldObjects;
        public int nodeMinSize = 1;
        private OCTREE.Octree _octree;

        private void Start()
        {
            _octree = new OCTREE.Octree(worldObjects, nodeMinSize);
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