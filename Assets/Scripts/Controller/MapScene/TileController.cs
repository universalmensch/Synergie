using UnityEngine;

namespace Controller.MapScene
{
    public class TileController :  MonoBehaviour, IClickable
    {
        public Vector2 tileCenter;

        private void Start()
        {
            tileCenter = new Vector2(transform.position.x, transform.position.z);
        }
        
        public void OnClick()
        {
            Debug.Log("Tile geclicked");
        }
    }
}