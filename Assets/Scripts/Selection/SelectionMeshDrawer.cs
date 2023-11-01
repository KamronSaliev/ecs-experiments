using Unity.Mathematics;
using UnityEngine;

namespace ECSExperiments.Selection
{
    public class SelectionMeshDrawer : MonoBehaviour
    {
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Material _material;
        [SerializeField] private float3 _offset;

        private const float ViewAngle = 90.0f;

        public void Draw(float3 position, float scale)
        {
            Graphics.DrawMesh(_mesh,
                Matrix4x4.TRS(
                    position + _offset,
                    quaternion.RotateX(math.radians(ViewAngle)),
                    Vector3.one * scale),
                _material,
                0);
        }
    }
}