using UnityEngine;

namespace Enemies
{
    public class PlayerUndergroundValueEnemyActivationCondition : EnemyActivationCondition
    {
        private enum UndergroundCondition
        {
            Underground,
            Overground,
        }

        [SerializeField]
        private UndergroundCondition condition;

        private UndergroundWorldChanger undergroundWorldChanger;

        private void Awake()
        {
            undergroundWorldChanger = FindObjectOfType<UndergroundWorldChanger>();      
        }

        public override bool CheckCondition()
        {
            if (condition == UndergroundCondition.Underground && undergroundWorldChanger.UndergroundValue < 0)
                return true;

            if (condition == UndergroundCondition.Overground && undergroundWorldChanger.UndergroundValue > 0)
                return true;

            return false;
        }
    }



}
