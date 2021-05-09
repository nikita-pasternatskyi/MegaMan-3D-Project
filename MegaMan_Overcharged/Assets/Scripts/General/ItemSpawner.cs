using System.Collections.Generic;
using UnityEngine;

namespace Core.General
{
    class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _itemToSpawn;
        [SerializeField] private List<GameObject> _itemsToSpawn;
        [SerializeField] private ItemSpawnMode _itemSpawnMode;

        public void SpawnItem()
        {     
            switch(_itemSpawnMode)
            {
                case ItemSpawnMode.Random:
                    var randomObject = PickRandomItem(_itemsToSpawn);
                    if (randomObject != null)
                        Instantiate(randomObject, transform.position, transform.rotation);
                    break;
                case ItemSpawnMode.Concrete:
                    Instantiate(_itemToSpawn, transform.position, transform.rotation);
                    break;
            }
        }

        private GameObject PickRandomItem(List<GameObject> gameObjectList)
        {
            return gameObjectList[UnityEngine.Random.Range(0, gameObjectList.Count)];
        }
    }
}
