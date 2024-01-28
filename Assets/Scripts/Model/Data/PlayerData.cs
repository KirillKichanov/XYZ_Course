using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;
        
        public int Coins;
        public int Hp;
        public bool isArmed;
    }
}
