using UnityEngine;

namespace Controller.BattleScene
{
    public class CameraController : MonoBehaviour
    {
        private const float CameraTilt = 50f;
        private static readonly Vector3 Offset = new(0.0f, 12.0f, -8.0f);

        private void Start()
        {
            transform.position = Offset;
            transform.rotation = Quaternion.Euler(CameraTilt, 0f, 0f);
        }
    }
}