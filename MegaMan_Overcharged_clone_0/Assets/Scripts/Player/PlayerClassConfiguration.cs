using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Player;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerClassConfiguration", order = 1)]
public class PlayerClassConfiguration : ScriptableObject
{
    [Header("Physics")]
    public float Gravity;
    public float Mass;
    public float AirDrag;
    public float GroundDrag;

    [Header("Movement")]
    public float Speed;
    public float JumpHeight;
    public float AirMovementControlMultiplier;
}
