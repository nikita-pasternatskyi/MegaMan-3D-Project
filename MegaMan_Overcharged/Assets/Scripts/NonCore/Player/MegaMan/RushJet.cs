using Core.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NonCore.Player.MegaMan
{
    public class RushJet : RequiresInput
    {
        public InputActionReference Movement;
        [SerializeField] private float _flightTime;
        [SerializeField] private float _currentFlightTime;
        [SerializeField] private float _flightSpeed;

        public float xRotSpeed = 50;
        public float yRotSpeed = 50;

        [SerializeField] private bool _isFlying;
        [SerializeField] public float turnSpeed;

        private Vector2 _input;

        private void OnEnable()
        {
            Movement.action.performed += context => _input = context.ReadValue<Vector2>();
        }

        private void OnDisable()
        {
            Movement.action.performed -= context => _input = context.ReadValue<Vector2>();
        }

        private void OnTriggerEnter(Collider other)
        {
            other.transform.parent = this.transform;

            if (other.GetComponent<RushCall>() != null)
            {
                other.GetComponent<RushCall>().MountRushJet();
                StartFlight();
            }
        }

        private void FixedUpdate()
        {        
            if (_isFlying)
                Fly();
        }

        private void Fly()
        {
            transform.Rotate(Vector3.up * Time.fixedDeltaTime * xRotSpeed * _input.x);
            transform.Rotate(Vector3.right * Time.fixedDeltaTime * yRotSpeed * _input.y);

            Quaternion tempRot = transform.rotation;

            float zRot = 0;
            if (_input.x > 0.1)
            {
                zRot = -25f;
            }
            else if (_input.y < -0.1)
            {
                zRot = 25f;
            }
            else 
            {
                zRot = 0;
            }

            tempRot.eulerAngles = new Vector3(tempRot.eulerAngles.x, tempRot.eulerAngles.y, zRot);
            transform.rotation = Quaternion.Lerp(transform.rotation, tempRot, Time.fixedDeltaTime * turnSpeed);
            transform.position += _flightSpeed * transform.forward * Time.deltaTime;
        }

        private void StartFlight() => _isFlying = true;
    }
}