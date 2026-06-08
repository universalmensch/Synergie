using UnityEngine;

namespace Controller.MapScene
{
    public class PlayerController : MonoBehaviour, IClickable
    {
        public bool isSelected;
        
        private const float MoveSpeed = 10f;
        private Vector2 _targetPosition;
        private bool _isMoving;
        
        private void Start()
        {
        
        }

        private void Update()
        {
            if (_isMoving)
            {
                HandleMovement();
            }
        }

        private void HandleMovement()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(_targetPosition.x,  1f,  _targetPosition.y),
                MoveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
            {
                _isMoving = false;
            }
        }

        public void OnClick()
        {
            isSelected = !isSelected;
            Debug.Log("Player geclicked");
        }

        public void MoveTo(Vector2 targetPos)
        {
            _targetPosition = targetPos;
            _isMoving = true;
            isSelected = false;
        }
    }
}
