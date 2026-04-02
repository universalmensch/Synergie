using UnityEngine;

namespace  Map
{
    public class Tile : MonoBehaviour
    {
        public static void DrawTile(float size, Vector3 center)
        {
            Gizmos.color = Color.red;
            
            var points = new Vector3[12];
            var step = size / 2;
            
            // top
            points[0] = center + new Vector3(size, 0, -step);
            points[1] = center + new Vector3(size, 0, step);
            
            // top right
            points[2] = center + new Vector3(size, 0, step);
            points[3] = center + new Vector3(0,0, size);
            
            // bottom right
            points[4] = center + new Vector3(0,0, size);
            points[5] = center + new Vector3(-size, 0, step);
            
            // bottom
            points[6] = center + new Vector3(-size, 0, step);
            points[7] = center + new Vector3(-size, 0, -step);
            
            // bottom left
            points[8] = center + new Vector3(-size, 0, -step);
            points[9] = center + new Vector3(0, 0, -size);
            
            // top left
            points[10] = center + new Vector3(0, 0, -size);
            points[11] = center + new Vector3(size, 0, -step);
            
            Gizmos.DrawLineList(points);
        }
    }
}
;