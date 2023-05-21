using UnityEngine;

namespace OCTREE
{
    public class CreateOCtree : MonoBehaviour
    {
        public GameObject[] worldObjects;
        public int nodeMinSize = 1;
        private Octree _octree;

        private void Start()
        {
            _octree = new Octree(worldObjects, nodeMinSize);
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