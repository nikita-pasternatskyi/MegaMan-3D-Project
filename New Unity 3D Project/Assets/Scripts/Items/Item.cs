using System;
using UnityEngine;

namespace Assets.Scripts.Items
{
    class Item : MonoBehaviour
    {
       
        protected virtual void Start()
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position, -transform.up, out raycastHit))
            { 
            //SetGroundPosition
            }
        }
    }
}
