using UnityEngine;
using Assets.Scripts.General;

namespace Assets.Scripts.Player
{
    [AddComponentMenu("Objects/Projectile")]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private LayerMask _hittableLayerMask;
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        [SerializeField] private float _lifeTime;

        Vector3 _prevPos;
        RaycastHit _hit;

        private void Update()
        {
            _prevPos = transform.position;
        }

        private void FixedUpdate()
        {
            _lifeTime -= Time.fixedDeltaTime;
            //transform.position += transform.forward * _speed;
            transform.Translate(transform.forward * Time.fixedDeltaTime);
            if (_lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void LateUpdate()
        {
            if (Physics.Raycast(_prevPos, transform.position, out _hit, _hittableLayerMask))
            {
                Debug.Log("it hit!!!!" + _hit.collider.name);
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("a");
            other.GetComponent<Health>()?.TakeDamage(_damage);
        }

    }
}
