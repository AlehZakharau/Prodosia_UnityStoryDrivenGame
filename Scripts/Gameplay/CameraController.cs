using Cinemachine;
using Data;
using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class CameraController
    {
        private InputActions inputActions;
        private CinemachineVirtualCamera camera;
        private readonly CameraConfig cameraConfig;

        private float speed = 4f;

        public CameraController(InputActions inputActions, CinemachineVirtualCamera camera, CameraConfig cameraConfig)
        {
            this.inputActions = inputActions;
            this.camera = camera;
            this.cameraConfig = cameraConfig;
        }

        public void Tick()
        {
            var move = inputActions.Player.Move.ReadValue<Vector2>();
            var zoom = inputActions.Player.Zoom.ReadValue<Vector2>();

            move = move.normalized;
            zoom = zoom.normalized;
            var velocity = CheckCameraBoarder(move, zoom);


            velocity.x *= cameraConfig.CameraSpeedX;
            velocity.y *= cameraConfig.CameraSpeedY;
            velocity.z *= cameraConfig.CameraSpeedZ;

            velocity *= Time.deltaTime;

            camera.transform.Translate(velocity);
        }


        private Vector3 CheckCameraBoarder(Vector2 moveInput, Vector3 zoomInput)
        {
            var pos = camera.transform.position;
            if (pos.x > cameraConfig.rightFace && moveInput.x >= 0 || pos.x < cameraConfig.leftFace && moveInput.x <= 0)
            {
                moveInput.x = 0;
            }
            if (pos.y > cameraConfig.upFace & moveInput.y >= 0 || pos.y < cameraConfig.downFace && moveInput.y <= 0)
            {
                moveInput.y = 0;
            }
            if(pos.z > cameraConfig.forwardFace && zoomInput.y > 0 || pos.z < cameraConfig.backFace && zoomInput.y < 0)
            {
                zoomInput.y = 0;
            }

            return new Vector3(moveInput.x, moveInput.y, zoomInput.y).normalized;
        }
    }
}