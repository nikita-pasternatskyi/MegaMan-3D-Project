using Core.Levels;
using UnityEngine;


namespace Core.General
{
    public static class ObjectSpawner
    {
        public static GameObject SpawnObject(GameObject projectile, Vector3 spawnPosition, Quaternion spawnRotation)
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                return MonoBehaviour.Instantiate(projectile, spawnPosition, spawnRotation);
            }
            return null;
        }

        public static GameObject SpawnRandomObject(GameObject[] gameObjects, Vector3 spawnPosition, Quaternion spawnRotation)
        {
            if (!LevelSettings.Instance.IsPaused)
            {
                var randomObject = PickRandomObject(gameObjects);
                return MonoBehaviour.Instantiate(randomObject, spawnPosition, spawnRotation);
            }
            return null;
        }

        private static GameObject PickRandomObject(GameObject[] gameObjects)
        {
            return gameObjects[UnityEngine.Random.Range(0, gameObjects.Length)];
        }
    }
}
