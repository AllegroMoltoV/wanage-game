using UnityEngine;

public class TorusWithBoxColliders : MonoBehaviour
{
    [SerializeField] private int segments = 100;
    [SerializeField] private int tubeSegments = 20;
    [SerializeField] private float radius = 1.0f;
    [SerializeField] private float tubeRadius = 0.3f;

    void Start()
    {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        Mesh torusMesh = CreateTorus(segments, tubeSegments, radius, tubeRadius);
        meshFilter.mesh = torusMesh;

        SaveMeshAsAsset(torusMesh, "Assets/Torus.asset");

        AddBoxColliders(radius, tubeRadius);
    }

    Mesh CreateTorus(int segments, int tubeSegments, float radius, float tubeRadius)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[segments * tubeSegments];
        int[] triangles = new int[segments * tubeSegments * 6];
        Vector2[] uv = new Vector2[segments * tubeSegments];

        for (int seg = 0; seg < segments; seg++)
        {
            float theta = 2.0f * Mathf.PI * seg / segments;
            Vector3 circleCenter = new Vector3(Mathf.Cos(theta) * radius, Mathf.Sin(theta) * radius, 0.0f);

            for (int tubeSeg = 0; tubeSeg < tubeSegments; tubeSeg++)
            {
                float phi = 2.0f * Mathf.PI * tubeSeg / tubeSegments;
                Vector3 vertex = new Vector3(
                    Mathf.Cos(theta) * (radius + Mathf.Cos(phi) * tubeRadius),
                    Mathf.Sin(theta) * (radius + Mathf.Cos(phi) * tubeRadius),
                    Mathf.Sin(phi) * tubeRadius
                );

                int vertexIndex = seg * tubeSegments + tubeSeg;
                vertices[vertexIndex] = vertex;
                uv[vertexIndex] = new Vector2((float)seg / segments, (float)tubeSeg / tubeSegments);

                int nextSeg = (seg + 1) % segments;
                int nextTubeSeg = (tubeSeg + 1) % tubeSegments;

                int triIndex = vertexIndex * 6;
                triangles[triIndex] = vertexIndex;
                triangles[triIndex + 1] = nextSeg * tubeSegments + tubeSeg;
                triangles[triIndex + 2] = nextSeg * tubeSegments + nextTubeSeg;

                triangles[triIndex + 3] = vertexIndex;
                triangles[triIndex + 4] = nextSeg * tubeSegments + nextTubeSeg;
                triangles[triIndex + 5] = seg * tubeSegments + nextTubeSeg;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        return mesh;
    }

    void AddBoxColliders(float radius, float tubeRadius)
    {
        int boxCount = 12; // ボックスの数。増やすと精度が上がるが、負荷も上がる
        float angleStep = 360f / boxCount;
        float boxWidth = 2 * Mathf.PI * radius / boxCount;
        float boxHeight = 2 * tubeRadius;

        for (int i = 0; i < boxCount; i++)
        {
            float angle = i * angleStep;
            float rad = Mathf.Deg2Rad * angle;
            Vector3 boxPosition = new Vector3(Mathf.Cos(rad) * radius, Mathf.Sin(rad) * radius, 0);

            GameObject box = new GameObject("BoxCollider_" + i);
            box.transform.SetParent(this.transform);
            box.transform.localPosition = boxPosition;
            box.transform.localRotation = Quaternion.Euler(0, 0, angle);

            BoxCollider boxCollider = box.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(boxWidth, boxHeight, tubeRadius);
        }
    }

    void SaveMeshAsAsset(Mesh mesh, string path)
    {
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.CreateAsset(mesh, path);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }
}
