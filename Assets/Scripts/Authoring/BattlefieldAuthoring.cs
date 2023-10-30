using Unity.Mathematics;
using UnityEngine;

namespace ECSExperiments.Authoring
{
    public class BattlefieldAuthoring : MonoBehaviour
    {
        public float2 Dimensions => _dimensions;

        public int PositionsCount => _positionsCount;

        public GameObject DemoPrefab => _demoPrefab;

        public uint RandomSeed => _randomSeed;

        public float UnitSpawnRate => _unitSpawnRate;

        public GameObject UnitPrefab => _unitPrefab;

        [Header("Battlefield")] 
        [SerializeField] private float2 _dimensions;
        [SerializeField] private int _positionsCount;
        
        [SerializeField] private GameObject _demoPrefab; // TODO: remove later

        [Header("Common")] 
        [SerializeField] private uint _randomSeed;

        [Header("Units")] 
        [SerializeField] private float _unitSpawnRate;
        [SerializeField] private GameObject _unitPrefab;
    }
}