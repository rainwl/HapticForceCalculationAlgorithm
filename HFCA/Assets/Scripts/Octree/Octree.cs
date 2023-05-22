using UnityEngine;

namespace OCTREE
{
    public class Octree
    {
        public readonly OctreeNode RootNode;

        public Octree(GameObject[] objects, float minNodeSize)
        {
            // Create a Bounds around all the objects in Array
            Bounds bounds = new();
            foreach (var go in objects)
            {
                bounds.Encapsulate(go.GetComponent<Collider>().bounds);
            }

            // 
            var maxSize = Mathf.Max(new float[] { bounds.size.x, bounds.size.y, bounds.size.z });
            var sizeVector = new Vector3(maxSize, maxSize, maxSize) * 0.5f;
            bounds.SetMinMax(bounds.center - sizeVector, bounds.center + sizeVector);
            RootNode = new OctreeNode(bounds, minNodeSize);
            AddObjects(objects);
        }

        private void AddObjects(GameObject[] worldObjects)
        {
            foreach (var go in worldObjects)
            {
                RootNode.AddObject(go);
            }
        }
    }
}