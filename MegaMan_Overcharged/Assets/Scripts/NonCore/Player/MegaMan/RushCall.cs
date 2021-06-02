using Core.Levels;
using UnityEngine;
using Core.Player;

namespace NonCore.Player.MegaMan
{
    public class RushCall : RequiresInput
    {
        [SerializeField] private Transform _body;
        [SerializeField] private CameraControl _playerCamera;
        [SerializeField] private float _spawnDistance;
        [SerializeField] private RushMode _currentRushState = RushMode.None;
        [SerializeField] private RushMode _selectedRush = RushMode.RushCoil;
        [SerializeField] private MonoBehaviour[] _componentsToDisableDuringFlight;
        [SerializeField] GameObject _rushCoilPrefab;
        [SerializeField] GameObject _rushJetPrefab;

        protected override void OnSwitchRushType()
        {
            if (_selectedRush == RushMode.RushJet)
                _selectedRush = RushMode.RushCoil;
            else if (_selectedRush == RushMode.RushCoil)
                _selectedRush = RushMode.RushJet;
        }

        protected override void OnRushCall()
        {
            if (_selectedRush == RushMode.RushCoil)
                CallRushCoil();
            else if (_selectedRush == RushMode.RushJet)
                CallRushJet();
        }

        private void Update()
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                if (_currentRushState == RushMode.RushJet)
                {
                    _body.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }

        public void MountRushJet()
        {
            _currentRushState = RushMode.RushJet;
            foreach (var component in _componentsToDisableDuringFlight)
            {
                component.enabled = false;
            }
        }

        public void DismountRushJet()
        {

        }

        private void CallRushJet()
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                if (_currentRushState == RushMode.None)
                {
                    SpawnRush(_rushJetPrefab);
                    _currentRushState = RushMode.RushJetAwaiting;
                }
            }
        }

        private void CallRushCoil()
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                if (_currentRushState == RushMode.None)
                {
                    SpawnRush(_rushCoilPrefab);
                    _currentRushState = RushMode.RushCoil;
                }
            }
        }

        private void SpawnRush(GameObject whichRush)
        {
            RaycastHit raycastHit;
            Vector3 position = _body.position + _body.forward * _spawnDistance;

            if (Physics.Raycast(position, -transform.up, out raycastHit))
            {
                position.y = raycastHit.point.y + 0.5f;
            }
            Instantiate(whichRush, position, _body.rotation);
        }


    }
}
