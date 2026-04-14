using Unity.Mathematics;
using UnityEngine;

namespace Map
{

    public class TileGrid : MonoBehaviour
    {
        public GameObject tilePrefab;

        private const int GridSize = 11;
        private const int TileSize = 5;
        private const float WidthOffsetFactor = TileSize * 1.5f;
        private static readonly float HeightOffsetFactor = TileSize * math.sqrt(3);

        private void Start()
        {
            GenerateGrid();
        }
        
        private void GenerateGrid()
        {
            for (var x = - GridSize + 1; x < GridSize; x++)
            {
                for (var y = - GridSize + 1; y < GridSize; y++)
                {
                    int z = -x - y;

                    if (Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z) <= GridSize)
                    {
                        CreateTile(x, y);
                    }
                }
            }
        }

        private static Vector3 ConvertPositionToCoordinates(int x, int y)
        {
            var xOffset = x * WidthOffsetFactor;
            var yOffset = (y  + x * 0.5f) * HeightOffsetFactor;
            
            return new Vector3(xOffset, 0, yOffset);
        }

        private void CreateTile(int x, int y)
        {
            var tileObj = Instantiate(tilePrefab, ConvertPositionToCoordinates(x, y), Quaternion.identity, transform);
            tileObj.GetComponent<Tile>().CreateTile(TileSize, x, y);
        }
    }
}