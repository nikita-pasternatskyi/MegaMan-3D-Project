using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

namespace Core.General
{
    public class VFXCaller : MonoBehaviour
    {
        private int effectCalls;
        private ParticleSystem _particleSystem;
        private VisualEffect _visualEffect;

        private void Start()
        {
           TryGetComponent<ParticleSystem>(out _particleSystem);
           TryGetComponent<VisualEffect>(out _visualEffect);
        }

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
