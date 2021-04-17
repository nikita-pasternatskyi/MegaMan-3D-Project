﻿using Mirror;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.General
{
    class ObjectTeleporter : NetworkBehaviour
    {
        public static ObjectTeleporter Instance;
        private Dictionary<GameObject, Vector3> _gameObjectsToSpawn;

        private void Start()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else {
                Instance = this;
            }

            _gameObjectsToSpawn = new Dictionary<GameObject, Vector3>();
        }

        public static void RequestObject(GameObject objectToRequest, Vector3 whereToSpawn)
        {
            Instance._gameObjectsToSpawn.Add(objectToRequest, whereToSpawn);
        }


    }
}