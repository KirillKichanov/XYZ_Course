using UnityEngine;

namespace Tests
{
    public class LoadPrefabHelper
    {
        public static GameObject LoadHero()
        {
            var prefab = Resources.Load<Object>("Prefabs/Hero/Hero");
            var hero = Object.Instantiate<GameObject>((GameObject)prefab);
            return hero;
        }

        public static GameObject LoadSilverCoin()
        {
            var prefab = Resources.Load<Object>("Prefabs/Props/Coins/SilverCoin");
            var silverCoin = Object.Instantiate<GameObject>((GameObject)prefab);
            return silverCoin;
        }
        
        public static GameObject LoadGoldenCoin()
        {
            var prefab = Resources.Load<Object>("Prefabs/Props/Coins/GoldenCoin");
            var goldenCoin = Object.Instantiate<GameObject>((GameObject)prefab);
            return goldenCoin;
        }
    }
}