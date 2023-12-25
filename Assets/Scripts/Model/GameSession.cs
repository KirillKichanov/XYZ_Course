using System;
using UnityEngine;

namespace Scripts.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        
        public PlayerData Data => _data;

        private void Awake()
        {
            if (IsSessionExist())
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(this);
            }
        }

        private bool IsSessionExist()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSessions in sessions)
            {
                if (gameSessions != this)
                    return true;
            }

            return false;
        }
    }
}