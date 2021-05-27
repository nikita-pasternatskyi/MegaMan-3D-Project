using UnityEngine;

namespace Core.General
{
    public class DestroyObject : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObjectToDestroy;
        public void DestroyGameObject() => Destroy(_gameObjectToDestroy ? _gameObjectToDestroy : this.gameObject);
    }
}
