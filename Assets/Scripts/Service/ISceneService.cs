using UnityEngine.SceneManagement;

namespace Service
{
    public interface ISceneService
    {
        public enum SceneName
        {
            Map,
            Battle,
            SelectionUI,
            SynergieUI,
            MapUI,
        }
        
        public void LoadScene(SceneName sceneName);

        public void LoadScene(SceneName sceneName, LoadSceneMode mode);
        
        public void UnloadScene(SceneName sceneName);
    }
}