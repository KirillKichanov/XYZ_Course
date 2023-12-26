using System;
using UnityEngine;

namespace Scripts.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        private PlayerData _savedData;

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

        public void SaveState()
        {
            _savedData = new PlayerData(){ Coins = _data.Coins, Hp = _data.Hp, isArmed = _data.isArmed};
        }

        public bool HasSavedState()
        {
            return _savedData != null;
        }

        public void LoadSavedState()
        {
            _data = new PlayerData(){ Coins = _savedData.Coins, Hp = _savedData.Hp, isArmed = _savedData.isArmed};
        }

        public static GameSession Get()
        {
            return FindObjectOfType<GameSession>();
        }
    }
}