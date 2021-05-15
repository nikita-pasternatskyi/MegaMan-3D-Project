using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.General
{
    [System.Serializable]
    public class PhysicsImitation : MonoBehaviour
    {
        [SerializeField] private BoxCollider _boxCollider;
        private Vector3 _startPosition;
        private Vector3 _groundPosition;
        private float _height() => _boxCollider.bounds.size.y;
        private float _fallSpeed;

        public void Start()
        {
            CheckGround();
            _fallSpeed = 0.1f;
            StartCoroutine(Fall());
        }

        private void CheckGround()
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position, -transform.up, out raycastHit))
            {
                _startPosition = transform.position;
                _groundPosition = raycastHit.point;
                _groundPosition.y += _height() * transform.localScale.y / 2;
            }
        }

        private IEnumerator Fall()
        {
            while (transform.position.y != _groundPosition.y)
            {
                _fallSpeed *= 1.1f;
                transform.position = Vector3.Lerp(_startPosition, _groundPosition, _fallSpeed);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
