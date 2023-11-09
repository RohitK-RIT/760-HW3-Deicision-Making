using Environment;
using UnityEngine;

namespace Characters.Enemy.State_Machine
{
    public class EnemyAttackState : EnemyBaseState
    {
        private Transform _player;
        public EnemyAttackState(EnemyStates key, GameObject gameObject, EnemyStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
        }

        public override void EnterState()
        {
            _player = GameManager.Instance.Player.transform;
        }

        public override void ExitState()
        {
            _player = null;
        }

        public override void UpdateState()
        {
            if (!_player)
                return;

            // Get the nearest node to the target.
            var nearestNodeToTarget = GroundSystem.Instance.GetNearestNode(_player.transform.position);
            // If it's the same as the target node then return.
            if (Agent.Movement.TargetNode != null && Agent.Movement.TargetNode.Equals(nearestNodeToTarget))
                return;

            Agent.Movement.SetTarget(_player.transform.position);
        }

        public override EnemyStates GetNextState()
        {
            if (Agent.HasEnemyInRange())
                return StateKey;

            return EnemyStates.Wander;
        }
    }
}