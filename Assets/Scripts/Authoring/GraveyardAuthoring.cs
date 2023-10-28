using Unity.Mathematics;
using UnityEngine;

namespace ECSExperiments.Authoring
{
    public class GraveyardAuthoring : MonoBehaviour
    {
        public float2 Dimensions => _dimensions;

        public int NumberTombstoneToSpawn => _numberTombstoneToSpawn;

        public GameObject TombstonePrefab => _tombstonePrefab;

        public uint RandomSeed => _randomSeed;

        public GameObject EnemyPrefab => _enemyPrefab;

        public float EnemySpawnRate => _enemySpawnRate;

        [SerializeField] private float2 _dimensions;
        [SerializeField] private int _numberTombstoneToSpawn;
        [SerializeField] private GameObject _tombstonePrefab;
        [SerializeField] private uint _randomSeed;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _enemySpawnRate;
    }
}