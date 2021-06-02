using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

namespace Core.General
{
    [System.Serializable]
    public class VFXCaller
    {
        private int effectCalls;
        [SerializeField] private ParticleSystem[] _particleSystems;
        [SerializeField] private VisualEffect[] _visualEffects;


        private void StartParticleSystems(ParticleSystem[] particleSystems)
        {
            try
            {
                foreach (var particleSystem in particleSystems)
                {
                    particleSystem.Play();
                }
            }
            catch 
            {
                Debug.LogWarning("No Particle systems found");
            }
        }
        private void StopParticleSystems(ParticleSystem[] particleSystems)
        {
            try
            {
                foreach (var particleSystem in particleSystems)
                {
                    if (particleSystem.isPlaying)
                    {
                        particleSystem.Stop();
                    }
                }
            }
            catch
            {
                Debug.LogWarning("No Particle systems found");
            }
        }
        private void StartVisualEffects(VisualEffect[] visualEffects)
        {
            try
            {
                foreach (var visualEffect in visualEffects)
                {
                    visualEffect.SendEvent("OnPlay");
                    visualEffect.Play();
                }
            }
            catch
            {
                Debug.LogWarning("No Visual effecs found");
            }
        }
        private void StopVisualEffects(VisualEffect[] visualEffects)
        {
            try
            {
                foreach (var visualEffect in visualEffects)
                {
                    visualEffect.Stop();
                }
            }
            catch
            {
                Debug.LogWarning("No Visual effecs found");
            }
        }

        public void EnableVFX()
        {
            effectCalls+=1;
            StartParticleSystems(_particleSystems);
            StartVisualEffects(_visualEffects);
        }

        public void DisableVFX()
        {
            effectCalls-=1;
            if (effectCalls <= 0)
            {
                StopParticleSystems(_particleSystems);
                StopVisualEffects(_visualEffects);
                effectCalls = 0;
            }
        }
    }
}
