using NaughtyAttributes;
using UnityEngine;

namespace Enemies
{
    public class TagSingletonDistanceActivationCondition : DistanceActivationCondition
    {
        [SerializeField, Tag]
        private string singletonTag;

        private Transform activator;
        protected override Transform Activator
        {
            get
            {
                if (activator == null)
                    activator = GameObject.FindGameObjectWithTag(singletonTag).transform;
                return activator;
            }
        }
    }



}
