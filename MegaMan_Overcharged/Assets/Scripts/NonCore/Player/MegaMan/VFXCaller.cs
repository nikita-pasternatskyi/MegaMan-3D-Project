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

            else if (_visualEffect)
            {
                _visualEffect.SendEvent("Fire");
                _visualEffect.Play();
            }
            else
                throw new MissingReferenceException("No particle system or visual effect found");
        }

        public void DisableVFX()
        {
            Debug.Log("Disable");
            effectCalls-=1;
            if (effectCalls <= 0)
            {
                if (_particleSystem)
                {
                    if (_particleSystem.isPlaying)
                        _particleSystem.Stop();
                }
                else if (_visualEffect)
                {
                    _visualEffect.Stop();
                }
                effectCalls = 0;
            }
        }
    }
}
