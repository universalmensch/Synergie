using UnityEngine;

namespace Controller.MapScene
{
    public class MapController : GameController
    {
        [SerializeField] private PlayerController player;

        public override void Clicked(Object obj)
        {
            if (obj.GetType() != typeof(TileController)) return;

            if (player.isSelected)
            {
                player.MoveTo(((TileController)obj).tileCenter);
            }
        }
    }
}