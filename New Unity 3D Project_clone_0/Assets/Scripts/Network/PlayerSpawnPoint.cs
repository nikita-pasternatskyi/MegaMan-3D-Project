﻿using System.Collections;
using UnityEngine;
using Assets.Scripts.Network;

namespace Assets.Scripts.Network
{
    public class PlayerSpawnPoint : MonoBehaviour
    {

        private void Awake() => PlayerSpawnSystem.AddSpawnPoint(transform);
        private void OnDestroy() => PlayerSpawnSystem.RemoveSpawnPoint(transform);

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 1f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
        }
    }
}