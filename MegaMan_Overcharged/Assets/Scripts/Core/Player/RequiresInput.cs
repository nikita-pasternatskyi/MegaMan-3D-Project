using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player
{
    public abstract class RequiresInput : MonoBehaviour
    {
        protected virtual void OnSwitchRushType()
        { 
        
        }

        protected virtual void OnMovement(InputValue value)
        { 
        
        }
        protected virtual void OnMainFire()
        { 
        
        }
        protected virtual void OnAlternateFire()
        { 
        
        }
        protected virtual void OnJump()
        { 
        
        }
        protected virtual void OnSprint()
        { 
        
        }

        protected virtual void OnCrouch()
        { 
        
        }
        protected virtual void OnRushCall()
        { 
        
        }
        protected virtual void OnMouseLook(InputValue value)
        {

        }
        protected virtual void OnSpecialAbility()
        { 
        
        }
        protected virtual void OnPauseGame()
        {

        }

        protected virtual void OnSwitchWeaponType()
        { 
        
        }

        protected virtual void OnSwitchWeapon(InputValue value)
        { }
    }
}
