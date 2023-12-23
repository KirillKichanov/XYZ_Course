using UnityEngine;

namespace Tests
{
    public class LoadHeroHelper
    {
        public static GameObject LoadHero()
        {
            var prefab = Resources.Load<Object>("Prefabs/Hero/Hero");
            var hero = Object.Instantiate<GameObject>((GameObject)prefab);
            return hero;
        }
    }
}