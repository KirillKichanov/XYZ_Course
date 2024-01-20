using Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Components.LevelManagement
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        public void Exit()
        {
            GameSession.Get().SaveState();
            SceneManager.LoadScene(_sceneName);
        }
    }
}