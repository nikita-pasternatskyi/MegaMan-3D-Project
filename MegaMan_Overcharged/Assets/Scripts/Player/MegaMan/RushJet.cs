using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.MegaMan
{
    class RushJet : MonoBehaviour
    {
        [SerializeField] private float _flightTime;
        [SerializeField] private float _currentFlightTime;
        [SerializeField] private float _flightSpeed;

        public float xRotSpeed = 50;
        public float yRotSpeed = 50;

        [SerializeField] private bool _isFlying;
        [SerializeField] public float turnSpeed;

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
            transform.Rotate(Vector3.up * Time.fixedDeltaTime * xRotSpeed * UnityEngine.Input.GetAxis("Horizontal"));
            transform.Rotate(Vector3.right * Time.fixedDeltaTime * yRotSpeed * UnityEngine.Input.GetAxis("Vertical"));

            Quaternion tempRot = transform.rotation;

            float zRot = 0;
            if (UnityEngine.Input.GetAxis("Horizontal") > 0.1)
            {
                zRot = -25f;
            }
            else if (UnityEngine.Input.GetAxis("Horizontal") < -0.1)
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