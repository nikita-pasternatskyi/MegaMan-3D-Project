using System;
using System.Collections;
using UnityEngine;
using Assets.Scripts.Player;
using Mirror;

namespace Assets.Scripts.Items
{
    class Item : NetworkBehaviour
    {
        public delegate void OnPickUp();
        public event OnPickUp PickedUp;

        private float _lerpMultiplier = 0.1f;
        private Vector3 _groundPosition;
        private Vector3 _startPosition;

        [SerializeField] private BoxCollider _boxCollider;

        protected virtual void Start()
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position, -transform.up, out raycastHit))
            {
                _startPosition = transform.position;
                _groundPosition = raycastHit.point;
                _groundPosition.y += _boxCollider.size.y * transform.localScale.y / 2;
            }
        }
        protected virtual void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<PlayerMove>() != null)
            PickedUp?.Invoke();
        }
        protected virtual IEnumerator Fall()
        {
            while (transform.position.y != _groundPosition.y)
            {
                _lerpMultiplier *= 1.2f;
                transform.position = Vector3.Lerp(_startPosition, _groundPosition, _lerpMultiplier); 
                yield return new WaitForFixedUpdate();
            }

        }
    }
}
