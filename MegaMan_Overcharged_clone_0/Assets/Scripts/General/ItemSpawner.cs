using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Mirror;

namespace Assets.Scripts.General
{
    class ItemSpawner : NetworkBehaviour
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
