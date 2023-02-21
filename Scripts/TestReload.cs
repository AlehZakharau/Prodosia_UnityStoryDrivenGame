using System;
using UnityEngine;

namespace Core
{
    public class TestReload : MonoBehaviour
    {
        public GameObject cube;
        private bool a = false;

        private void Update()
        {
            cube.transform.position += Vector3.forward * 5.01f;
            Rotate(cube);
        }


        private void Rotate(GameObject cube)
        {
            this.cube.transform.RotateAround(Vector3.up, 20);
        }
    }
}