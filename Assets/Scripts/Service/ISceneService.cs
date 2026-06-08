namespace Service
{
    public interface ISceneService
    {
        public enum SceneName
        {
            Map,
            Battle
        }
        
        public void LoadScene(SceneName sceneName);
    }
}