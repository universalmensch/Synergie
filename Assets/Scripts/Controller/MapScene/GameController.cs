using UnityEngine;

namespace Controller.MapScene
{
    public class GameController : MonoBehaviour
    {

        [SerializeField] private PlayerController selectedPlayer;

        public void TileClicked(TileController tile)
        {
            if (selectedPlayer.isSelected)
            {
                selectedPlayer.MoveTo(tile.tileCenter);
            }
        }
    }
}
