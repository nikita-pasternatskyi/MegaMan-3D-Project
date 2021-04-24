using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Player;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerScriptableObject", order = 1)]
public class PlayerScriptableObject : ScriptableObject
{
    public PlayerMovement MovementClass;
    public PlayerSpecialAbility ClassSpecialAbility;

    public float Gravity;
    public float Mass;
    public float Speed;
    public float JumpHeight;

}
