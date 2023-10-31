using UnityEngine;
using ProjectDawn.Navigation.Hybrid;

namespace ProjectDawn.Navigation.Sample.Scenarios
{
    [RequireComponent(typeof(AgentAuthoring))]
    [DisallowMultipleComponent]
    public class AgentDestinationAuthoring : MonoBehaviour
    {
        public Transform Target;
        public float Radius;
        public bool EveryFrame;

        private void Start()
        {
            var agent = transform.GetComponent<AgentAuthoring>();
            var body = agent.EntityBody;
            body.Destination = Target.position;
            body.IsStopped = false;
            agent.EntityBody = body;
        }

        void Update()
        {
            if (!EveryFrame)
                return;
            Start();
        }
    }
}
