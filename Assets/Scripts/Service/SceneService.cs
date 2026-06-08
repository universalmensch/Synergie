using UnityEngine.SceneManagement;

namespace Service
{
    public class SceneService : ISceneService
    {
        
        public void LoadScene(ISceneService.SceneName sceneName)
        {
            SceneManager.LoadScene(sceneName.ToString());
        }
    }
}