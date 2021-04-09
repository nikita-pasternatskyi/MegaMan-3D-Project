using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.General
{
    class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _itemToSpawn;
        [SerializeField] private List<GameObject> _itemsToSpawn;
        [SerializeField] private ItemSpawnMode _itemSpawnMode;

        private System.Random _random = new System.Random();

        public void SpawnItem()
        {
            
            switch(_itemSpawnMode)
            {
                case ItemSpawnMode.Random:
                    Instantiate(PickRandomItem(_itemsToSpawn), transform.position, transform.rotation);
                    break;
                case ItemSpawnMode.Concrete:
                    Instantiate(_itemToSpawn, transform.position, transform.rotation);
                    break;
            }
        }

        private GameObject PickRandomItem(List<GameObject> gameObjectList) => gameObjectList[_random.Next(0, gameObjectList.Count)];
    }
}
