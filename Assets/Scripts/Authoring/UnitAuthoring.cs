using ECSExperiments.Components.Unit;
using UnityEngine;

namespace ECSExperiments.Authoring
{
    public class UnitAuthoring : MonoBehaviour
    {
        public UnitTeamID UnitTeamID => _unitTeamID;

        public float MaxLife => _maxLife;

        public GameObject Prefab => _prefab;

        [SerializeField] private UnitTeamID _unitTeamID;
        [SerializeField] private float _maxLife;
        [SerializeField] private GameObject _prefab;
    }
}