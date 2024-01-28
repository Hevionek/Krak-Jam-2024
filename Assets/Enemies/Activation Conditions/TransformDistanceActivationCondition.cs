using UnityEngine;

namespace Enemies
{
    public class TransformDistanceActivationCondition : DistanceActivationCondition
    {
        [SerializeField, Tooltip("Transform which will revive enemy when nearby")]
        private Transform activator;
        protected override Transform Activator => activator;
    }

}
