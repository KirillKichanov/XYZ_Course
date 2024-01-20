using System.Collections;
using System.Collections.Generic;
using Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Components.LevelManagement
{ 
    public class ReloadLevelComponent : MonoBehaviour
    {
        public void Reload()
        {
            var session = FindObjectOfType<GameSession>();
            if (session.HasSavedState())
            {
                session.LoadSavedState();
            }
            else
            {
                Destroy(session.gameObject);
            }
            
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}