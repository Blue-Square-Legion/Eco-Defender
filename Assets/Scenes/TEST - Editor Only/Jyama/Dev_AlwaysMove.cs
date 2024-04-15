using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DevTool
{
    public class Dev_AlwaysMove : MonoBehaviour
    {
        public Vector3 Movement;
        public float Speed = 0.01f;

        private void FixedUpdate()
        {
            transform.position += Movement * Speed;
        }
    }
}

