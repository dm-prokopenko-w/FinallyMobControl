using UnityEngine.SceneManagement;

namespace Core
{
    public static class SceneLoader 
    {
        public static void LoadMenuScene()
        {
            SceneManager.LoadScene(Constants.MenuScene);
        }

        public static void LoadGameScene()
        {
            SceneManager.LoadScene(Constants.GameScene);
        }
    }
}