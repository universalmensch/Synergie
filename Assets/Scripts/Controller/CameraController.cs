using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller
{
    public class CameraController : MonoBehaviour
    {
        private const float CameraSpeed = 15f;
        private const float CameraRotationSpeed = 60f;
        private const float CameraTilt = 60f;
        private static readonly Vector3 Offset = new(0.0f, 20.0f, -10.0f);
        
        [SerializeField] private GameObject player;
        private Vector3 _movementVector;
        private Vector3 _rotationVector;
        private Vector3 _resetRotationVector;
        private float _currentRotation;
        
        private void Start()
        {
            ResetCamera();
        }

        private void Update()
        {
            HandleCameraMovement();
        }

        private void HandleCameraMovement()
        {
            var rotatedMovement = Quaternion.Euler(0f, _currentRotation, 0f) * _movementVector;
            transform.position += rotatedMovement * (Time.deltaTime * CameraSpeed);
            
            _currentRotation += _rotationVector.y * (Time.deltaTime * CameraRotationSpeed);
            transform.rotation = Quaternion.Euler(CameraTilt, _currentRotation, 0f);
        }

        [UsedImplicitly]
        private void OnMoveCamera(InputValue movementValue)
        {
            var movementInput = movementValue.Get<Vector2>();
            _movementVector = new Vector3(movementInput.x, 0f, movementInput.y);
        }

        [UsedImplicitly]
        private void OnResetCamera()
        {
            ResetCamera();
        }

        [UsedImplicitly]
        private void OnRotateCamera(InputValue rotationValue)
        {
            var rotation = rotationValue.Get<Vector2>();
            _rotationVector = new Vector3(0f, rotation.x, 0f);
        }

        private void ResetCamera()
        {
            transform.position = player.transform.position + Offset;
            _currentRotation = 0f;
        }
    }
}
