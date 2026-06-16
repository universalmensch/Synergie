using Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controller.MapScene
{
    public class PlayerController : MonoBehaviour, IClickable
    {
        public bool isSelected;
        
        private const float MoveSpeed = 10f;
        private Vector2 _targetPosition;
        private bool _isMoving;
        
        private ISceneService _sceneService;
        
        private void Start()
        {
            _sceneService = ProjectInstaller.SceneService;
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
            
            if (Vector3.Distance(transform.position, new Vector3(_targetPosition.x,  1f,  _targetPosition.y)) < 1f)
            {
                _isMoving = false;
                _sceneService.LoadScene(ISceneService.SceneName.Battle);
            }
        }

        public void OnClick()
        {
            isSelected = !isSelected;
            _sceneService.LoadScene(ISceneService.SceneName.SelectionUI, LoadSceneMode.Additive);
        }

        public void MoveTo(Vector2 targetPos)
        {
            _targetPosition = targetPos;
            _isMoving = true;
            isSelected = false;
        }
    }
}
