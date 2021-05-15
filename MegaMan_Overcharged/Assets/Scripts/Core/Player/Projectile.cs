using UnityEngine;
using Core.General;

namespace Core.Player
{
    public abstract class Projectile : MonoBehaviour
    {
        protected virtual void FlyForward(float speed)
        {
            transform.position += transform.forward * speed * Time.fixedDeltaTime;
        }

    }
}
