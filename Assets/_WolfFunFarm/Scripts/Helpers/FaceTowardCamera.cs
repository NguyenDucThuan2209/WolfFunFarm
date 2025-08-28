using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfFunFarm
{
    public class FaceTowardCamera : MonoBehaviour
    {
        private void Update()
        {
            var cameraTransform = Camera.main.transform;
            var lookDirection = transform.position - cameraTransform.position;
            lookDirection.y = 0; // Keep only the horizontal direction
            if (lookDirection.sqrMagnitude > 0.001f) // Avoid zero-length direction
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }
}