using Unity.Mathematics;
using UnityEngine;

namespace ECS.Zombies.Authoring
{
    public class GraveyardMono : MonoBehaviour
    {
        public float2 Dimensions => _dimensions;

        public int NumberTombstoneToSpawn => _numberTombstoneToSpawn;

        public GameObject TombstonePrefab => _tombstonePrefab;

        public uint RandomSeed => _randomSeed;

        [SerializeField] private float2 _dimensions;
        [SerializeField] private int _numberTombstoneToSpawn;
        [SerializeField] private GameObject _tombstonePrefab;
        [SerializeField] private uint _randomSeed;
    }
}