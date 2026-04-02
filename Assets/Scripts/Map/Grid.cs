using UnityEngine;

namespace Map
{

    public class Grid : MonoBehaviour
    {

        private void OnDrawGizmos()
        {
            
            for (int x = 0; x < 10; x++)
            {
                for (int z = 0; z < 10; z++)
                {
                    Tile.DrawTile(1, new Vector3(x * 2 + z, 0, z * 1.5f));
                }
            }
            
            
        }   
    }
}