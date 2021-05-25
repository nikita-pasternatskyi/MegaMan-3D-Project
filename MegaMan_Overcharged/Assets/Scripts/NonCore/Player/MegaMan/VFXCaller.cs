using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

namespace NonCore.Player.MegaMan
{

    [System.Serializable]
    public class VFXCaller
    {
        private int effectCalls;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private VisualEffect _visualEffect;

        public void EnableVFX()
        {
            effectCalls+=1;
            if (_particleSystem)
            {
                 _particleSystem.Play();
            }
            if (_visualEffect)
            {
                _visualEffect.SendEvent("OnPlay");
                _visualEffect.Play();
            }
            else if (!_particleSystem && !_visualEffect)
            {
                Debug.LogWarning("No effects found");
            }
        }

        public void DisableVFX()
        {
            effectCalls-=1;
            if (effectCalls <= 0)
            {
                if (_particleSystem)
                {
                    if (_particleSystem.isPlaying)
                        _particleSystem.Stop();
                }
                if (_visualEffect)
                {
                    _visualEffect.Stop();
                }
                effectCalls = 0;
            }
        }
    }
}
