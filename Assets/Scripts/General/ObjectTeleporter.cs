using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.General
{
    class ObjectTeleporter : MonoBehaviour
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
