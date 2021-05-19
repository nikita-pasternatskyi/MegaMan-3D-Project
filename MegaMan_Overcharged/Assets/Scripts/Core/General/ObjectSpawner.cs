using Core.Levels;
using UnityEngine;


namespace Core.General
{
    public static class ObjectSpawner
    {
        public static GameObject SpawnObject(in GameObject projectile, in Vector3 spawnPosition, in Quaternion spawnRotation)
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                return MonoBehaviour.Instantiate(projectile, spawnPosition, spawnRotation);
            }
            return null;
        }

        public static GameObject SpawnRandomObject(in GameObject[] gameObjects, in Vector3 spawnPosition, in Quaternion spawnRotation)
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                var randomObject = PickRandomObject(gameObjects);
                return MonoBehaviour.Instantiate(randomObject, spawnPosition, spawnRotation);
            }
            return null;
        }

        private static GameObject PickRandomObject(in GameObject[] gameObjects)
        {
            return gameObjects[UnityEngine.Random.Range(0, gameObjects.Length)];
        }
    }
}
