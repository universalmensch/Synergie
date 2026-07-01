using Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controller.MapUIScene
{
    public class UIController : MonoBehaviour
    {
        private ISceneService _sceneService;

        private void Start()
        {
            _sceneService = ProjectInstaller.SceneService;
        }

        public void SwitchToSynergieUI()
        {
            _sceneService.UnloadScene(ISceneService.SceneName.MapUI);
            _sceneService.LoadScene(ISceneService.SceneName.SynergieUI, LoadSceneMode.Additive);
        }
    }
}