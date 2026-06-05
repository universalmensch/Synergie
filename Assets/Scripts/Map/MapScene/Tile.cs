using UnityEngine;

namespace  Map
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(LineRenderer))]
    public class Tile : MonoBehaviour
    {
        

        public void CreateTile(int size, int x, int y)
        {
            GetComponent<MeshFilter>().mesh = CreateMesh(size);
            CreateOutline(size);
        }
        
        private static Vector3[] GetHexCorners(float size)
        {
            Vector3[] corners = new Vector3[6];
            for (int i = 0; i < 6; i++)
            {
                float angle = Mathf.Deg2Rad * (60 * i);
                corners[i] = new Vector3(
                    Mathf.Cos(angle) * size,
                    0.01f,
                    Mathf.Sin(angle) * size
                );
            }
            return corners;
        }
        
        private void CreateOutline(float size)
        {
            var lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;
            lineRenderer.positionCount = 7; // letzter Punkt = erster Punkt
            lineRenderer.loop = true;
            lineRenderer.widthMultiplier = 0.1f;

            var corners = GetHexCorners(size);
            for (int i = 0; i < 6; i++)
                lineRenderer.SetPosition(i, corners[i]);

            lineRenderer.SetPosition(6, corners[0]); // schließen
        }
        
        private static Mesh CreateMesh(float size)
        {
            Mesh mesh = new Mesh();
            mesh.name = "Tile";
            
            

            // 6 Ecken + Mittelpunkt
            Vector3[] vertices = new Vector3[7];
            vertices[0] = Vector3.zero; // center

            for (int i = 0; i < 6; i++)
            {
                float angle = Mathf.Deg2Rad * (60 * i);
                vertices[i + 1] = new Vector3(
                    Mathf.Cos(angle) * size,
                    0,
                    Mathf.Sin(angle) * size
                );
            }

            // 6 Dreiecke
            int[] triangles = new int[18];
            for (int i = 0; i < 6; i++)
            {
                int triIndex = i * 3;
                triangles[triIndex] = 0;
                triangles[triIndex + 1] = (i + 2 > 6) ? 1 : i + 2;
                triangles[triIndex + 2] = i + 1;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            return mesh;
        }
    }
}
;