using Controller.BattleScene;
using Controller.MapScene;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller
{
    public class ClickRaycaster : MonoBehaviour
    {
        [SerializeField] private Camera gameCamera;
        [SerializeField] private GameController gameController;

        private void Update()
        {
            if (!Mouse.current.leftButton.wasPressedThisFrame) return;

            var ray = gameCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out var hit)) HandleClick(hit);
        }

        private void HandleClick(RaycastHit hit)
        {
            hit.collider.GetComponent<IClickable>()?.OnClick();

            if (hit.collider.TryGetComponent(out TileController tile)) gameController.Clicked(tile);
            else if (hit.collider.TryGetComponent(out UnitController unit)) gameController.Clicked(unit);
        }
    }
}