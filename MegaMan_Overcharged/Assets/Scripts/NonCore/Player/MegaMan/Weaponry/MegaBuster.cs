using Core.General;
using Core.Player;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace NonCore.Player.MegaMan
{
    enum ChargeState { 
        Zero, First, Second, Third,
    }

    public class MegaBuster : Weapon
    {
        [SerializeField] private GameObject _zeroProjectile;
        [SerializeField] private GameObject _firstStepPrefab;
        [SerializeField] private GameObject _secondStepPrefab;
        [SerializeField] private GameObject _thirdStepPrefab;
        [SerializeField] private Transform _whereToSpawn;
        [SerializeField] private Transform _referenceRotation;
        [SerializeField] private VFXCaller _firstStepVFX;
        [SerializeField] private VFXCaller _secondStepVFX;
        [SerializeField] private VFXCaller _thirdStepVFX;
        private GameObject _currentProjectile;
        private bool pressed = false;
        private ChargeState _currentChargeState;

        [SerializeField] private int _firstStep;
        [SerializeField] private int _secondStep;
        [SerializeField] private int _thirdStep;

        private void Start()
        {
            _currentProjectile = _zeroProjectile;
            _currentChargeState = ChargeState.Zero;
        }

        public override void OnAlternateFire()
        {            
        }

        public override void OnMainFire()
        {
            pressed = pressed ? false : true;
            if (!pressed && _currentProjectile != _zeroProjectile)
            {
                StopAllCoroutines();
                _firstStepVFX.DisableVFX();
                _secondStepVFX.DisableVFX();
                _thirdStepVFX.DisableVFX();
                ObjectSpawner.SpawnObject(_currentProjectile, _whereToSpawn.position, _referenceRotation.rotation);
                _currentProjectile = _zeroProjectile;
            }
            else if (pressed)
            {
                StopAllCoroutines();
                //_firstStepVFX.DisableVFX();
                //_secondStepVFX.DisableVFX();
                //_thirdStepVFX.DisableVFX();
                _currentProjectile = _zeroProjectile;
                ObjectSpawner.SpawnObject(_currentProjectile, _whereToSpawn.position, _referenceRotation.rotation);
                StartCoroutine(ChargeBuster());
            }
        }

        public override void Refill(int refillValue)
        {
            base.Refill(refillValue);
        }

        private void SwitchChargeState(ChargeState chargeStateToSwitchTo)
        {
            _currentChargeState = chargeStateToSwitchTo;
            switch (chargeStateToSwitchTo)
            {
                case ChargeState.Zero:
                    _firstStepVFX.DisableVFX();
                    _secondStepVFX.DisableVFX();
                    _thirdStepVFX.DisableVFX();
                    _currentProjectile = _zeroProjectile;
                    break;
                case ChargeState.First:
                    _firstStepVFX.EnableVFX();
                    _currentProjectile = _firstStepPrefab;
                    break;
                case ChargeState.Second:
                    _firstStepVFX.DisableVFX();
                    _secondStepVFX.EnableVFX();
                    _currentProjectile = _secondStepPrefab;
                    break;
                case ChargeState.Third:
                    _secondStepVFX.DisableVFX();
                    _thirdStepVFX.EnableVFX();
                    _currentProjectile = _thirdStepPrefab;
                    break;


            }
        }

        private IEnumerator ChargeBuster()
        {

                SwitchChargeState(ChargeState.Zero);
                for (int i = 0; i < _firstStep; i++)
                {
                    if(pressed)
                    yield return new WaitForFixedUpdate();
                }
                SwitchChargeState(ChargeState.First);
                for (int i = 0; i < _secondStep; i++)
                {
                    if (pressed)
                    yield return new WaitForFixedUpdate();
                }
                SwitchChargeState(ChargeState.Second);
                for (int i = 0; i < _thirdStep; i++)
                {
                    if (pressed)
                    yield return new WaitForFixedUpdate();
                }
                SwitchChargeState(ChargeState.Third);
        }


    }
}
