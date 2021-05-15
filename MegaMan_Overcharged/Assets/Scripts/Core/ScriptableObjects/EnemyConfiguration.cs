using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyConfigurations", menuName = "ScriptableObjects/Actors", order = 1)]
    public class EnemyConfiguration : ScriptableObject
    {
        public int Health;
    }
}
