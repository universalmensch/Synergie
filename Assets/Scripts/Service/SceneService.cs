using UnityEngine.SceneManagement;

namespace Service
{
    public class SceneService : ISceneService
    {
        
        public void LoadScene(ISceneService.SceneName sceneName)
        {
            SceneManager.LoadScene(sceneName.ToString());
        }

        public void LoadScene(ISceneService.SceneName sceneName, LoadSceneMode mode)
        {
            SceneManager.LoadScene(sceneName.ToString(), mode);
        }

        public void UnloadScene(ISceneService.SceneName sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName.ToString());
        }
    }
}