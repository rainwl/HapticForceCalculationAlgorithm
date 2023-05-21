using UnityEngine;

namespace OCTREE
{
    public class OctreeNode
    {
        private Bounds _nodeBounds;
        private readonly float _minSize;
        private readonly Bounds[] _childBounds;
        private OctreeNode[] _children = null;

        public OctreeNode(Bounds bound, float minNodeSize)
        {
            _nodeBounds = bound;
            _minSize = minNodeSize;

            var quarter = _nodeBounds.size.y / 4.0f;
            var childLength = _nodeBounds.size.y / 2;
            var childSize = new Vector3(childLength, childLength, childLength);
            _childBounds = new Bounds[8];
            _childBounds[0] = new Bounds(_nodeBounds.center + new Vector3(-quarter, quarter, -quarter), childSize);
            _childBounds[1] = new Bounds(_nodeBounds.center + new Vector3(quarter, quarter, -quarter), childSize);
            _childBounds[2] = new Bounds(_nodeBounds.center + new Vector3(-quarter, quarter, quarter), childSize);
            _childBounds[3] = new Bounds(_nodeBounds.center + new Vector3(quarter, quarter, quarter), childSize);
            _childBounds[4] = new Bounds(_nodeBounds.center + new Vector3(-quarter, -quarter, -quarter), childSize);
            _childBounds[5] = new Bounds(_nodeBounds.center + new Vector3(quarter, -quarter, -quarter), childSize);
            _childBounds[6] = new Bounds(_nodeBounds.center + new Vector3(-quarter, -quarter, quarter), childSize);
            _childBounds[7] = new Bounds(_nodeBounds.center + new Vector3(quarter, -quarter, quarter), childSize);
        }

        public void AddObject(GameObject gameObject)
        {
            DivideAndAdd(gameObject);
        }

        private void DivideAndAdd(GameObject gameObject)
        {
            if (_nodeBounds.size.y <= _minSize)
            {
                return;
            }

            _children ??= new OctreeNode[8];
            var dividing = false;
            for (var i = 0; i < 8; i++)
            {
                _children[i] ??= new OctreeNode(_childBounds[i], _minSize);
                if (!_childBounds[i].Intersects(gameObject.GetComponent<Collider>().bounds)) continue;
                dividing = true;
                _children[i].DivideAndAdd(gameObject);
            }

            if (dividing == false)
            {
                _children = null;
            }
        }


        public void Draw()
        {
            Gizmos.color = new Color(0, 1, 0,0.5f);
            Gizmos.DrawWireCube(_nodeBounds.center, _nodeBounds.size);
            if (_children == null) return;
            if (_children == null) return;
            for (var i = 0; i < 8; i++)
            {
                if (_children[i] != null)
                {
                    _children[i].Draw();
                }
            }
        }
    }
}