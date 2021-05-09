using Core.Levels;
using UnityEngine;
using Core.Player;

namespace NonCore.Player.MegaMan
{
    class RushCall : MonoBehaviour
    {
        public delegate void OnRushJetted();
        public static event OnRushJetted OnRushJet;

        [SerializeField] private PlayerCamera _playerCamera;
        [SerializeField] private float _spawnDistance;
        [SerializeField] RushMode _currentRushState = RushMode.None;
        [SerializeField] GameObject _rushCoil;
        [SerializeField] GameObject _rushJet;

        
        private void OnCallRush()
        { 
        
        }

        private void Update()
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                if (_currentRushState == RushMode.RushJet)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }

        public void MountRushJet()
        {
            _currentRushState = RushMode.RushJet;
            //_playerMove.enabled = false;
            _playerCamera.ChangeCameraMode(CameraModes.AroundPoint);
            OnRushJet?.Invoke();
        }

        private void CallRushJet()
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                if (_currentRushState == RushMode.None)
                {
                    SpawnRush(_rushJet);
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
                    SpawnRush(_rushCoil);
                    _currentRushState = RushMode.RushCoil;
                }
            }
        }

        private void SpawnRush(GameObject whichRush)
        {
            RaycastHit raycastHit;
            Vector3 position = transform.position + transform.forward * _spawnDistance;

            if (Physics.Raycast(position, -transform.up, out raycastHit))
            {
                position.y = raycastHit.point.y + 0.5f;
            }
            Instantiate(whichRush, position, transform.rotation);
        }


    }
}
