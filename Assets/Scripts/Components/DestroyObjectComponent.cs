using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Components
{ 
    public class DestroyObjectComponent : MonoBehaviour

    {
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private GameObject _objectToDestroy;
        [SerializeField] private SpriteAnimation _spriteAnimation;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        public void Collect()
        {
            _spriteAnimation.gameObject.SetActive(true);
            _spriteRenderer.enabled = false;
            _collider2D.enabled = false;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}