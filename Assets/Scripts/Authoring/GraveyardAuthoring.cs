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


        public float EnemySpawnRate => _enemySpawnRate;

        public float3 EnemySpawnOffset => _enemySpawnOffset;

        public GameObject EnemyPrefab => _enemyPrefab;


        [SerializeField] private float2 _dimensions;
        [SerializeField] private int _numberTombstoneToSpawn;
        [SerializeField] private GameObject _tombstonePrefab;
        
        [SerializeField] private uint _randomSeed;
        
        [SerializeField] private float _enemySpawnRate;
        [SerializeField] private float3 _enemySpawnOffset;
        [SerializeField] private GameObject _enemyPrefab;
    }
}