using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.MegaMan
{


    class RushCall : MonoBehaviour
    {
        public delegate void OnRushJetted();
        public static event OnRushJetted OnRushJet;

        [SerializeField] private float _spawnDistance;
        [SerializeField] PlayerMove _playerMove;
        [SerializeField] RushMode _currentRushState = RushMode.None;
        [SerializeField] GameObject _rushCoil;
        [SerializeField] GameObject _rushJet;

        private void OnEnable()
        {
            Input.CompanionSpecialOnePressed += CallRushCoil;
            Input.CompanionSpecialTwoPressed += CallRushJet;
        }

        public void MountRushJet()
        {
            _currentRushState = RushMode.RushJet;
            _playerMove.enabled = false;
            OnRushJet?.Invoke();
        }

        private void CallRushJet()
        {
            if (_currentRushState == RushMode.None)
                SpawnRush(_rushJet);
                _currentRushState = RushMode.RushJetAwaiting;
        }

        private void CallRushCoil()
        {
            if (_currentRushState == RushMode.None)
            {
                SpawnRush(_rushCoil);
                _currentRushState = RushMode.RushCoil;
            }
        }

        private void SpawnRush(GameObject whichRush)
        {
            Vector3 position = transform.position + transform.forward * _spawnDistance;
            Instantiate(whichRush, position, transform.rotation);
        }

        private void OnDisable()
        {
            Input.CompanionSpecialOnePressed -= CallRushCoil;
            Input.CompanionSpecialTwoPressed -= CallRushJet;
        }

    }
}
