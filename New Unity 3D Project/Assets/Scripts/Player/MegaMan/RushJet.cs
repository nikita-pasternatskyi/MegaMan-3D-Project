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
        [SerializeField] private float _maximumXRotation = 45;
        [SerializeField] private float _maximumYRotation = 90;
        [SerializeField] private float _maximumZRotation = 25;
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


        private void Update()
        {
            if (_isFlying)
                Fly();
        }

        private void Fly()
        {
            transform.position += _flightSpeed * transform.forward * Time.deltaTime;
            Quaternion targetRotation = new Quaternion(UnityEngine.Input.GetAxis("Vertical") * _maximumXRotation * Time.deltaTime, Camera.main.transform.rotation.y, 0, transform.rotation.w);
            //transform.rotation = targetRotation;
            transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        private void StartFlight() => _isFlying = true;
    }
}
