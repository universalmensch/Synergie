using Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controller.SynergieUIScene
{
    public class UIController : MonoBehaviour
    {
        private ISceneService _sceneService;

        private void Start()
        {
            _sceneService = ProjectInstaller.SceneService;
        }

        public void LeaveButtonClicked()
        {
            _sceneService.UnloadScene(ISceneService.SceneName.SynergieUI);
            _sceneService.LoadScene(ISceneService.SceneName.MapUI, LoadSceneMode.Additive);
        }
    }
}