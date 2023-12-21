using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Scripts
{
    public class HeroCameraFollow : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        

        public void SetCameraFollow(Transform target)
        {
            _camera.Follow = target;
        }
    }
}