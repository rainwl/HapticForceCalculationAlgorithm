using UnityEditor;
using UnityEngine;

namespace SDF_Calculator
{
    [ExecuteInEditMode]
    public class DrawSDF : MonoBehaviour
    {
        public Texture3D texture;
        public float stepScale = 1;
        public float surfaceOffset;
        public bool useCustomColorRamp;

        // We should initialize this gradient before using it as a custom color ramp
        public Gradient customColorRampGradient;
    }
    [CanEditMultipleObjects]
    [CustomEditor(typeof(DrawSDF))]
    public class Handle : Editor
    {
        private void OnSceneViewGUI(SceneView sv)
        {
            Object[] objects = targets;
            foreach (var obj in objects)
            {
                DrawSDF reference = obj as DrawSDF;
                if (reference != null && reference.texture != null)
                {
                    Handles.matrix = reference.transform.localToWorldMatrix;
                    Handles.DrawTexture3DSDF(reference.texture, reference.stepScale, reference.surfaceOffset,
                        reference.useCustomColorRamp ? reference.customColorRampGradient : null);
                }
            }
        }

        void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneViewGUI;
        }

        void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneViewGUI;
        }
    }
}