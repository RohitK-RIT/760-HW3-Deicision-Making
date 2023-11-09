using System.Linq;
using UnityEngine;

namespace Characters.Player.State_Machine
{
    public class PlayerFleeState : PlayerBaseState
    {
        public PlayerFleeState(PlayerStates key, GameObject gameObject, PlayerStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
        }

        public override void EnterState()
        {
            MoveToFleePoint();
        }

        public override void UpdateState()
        {
            if (!Agent.Movement.HasTarget)
                MoveToFleePoint();
        }

        public override PlayerStates GetNextState()
        {
            if (!Agent.HasEnemyInRange())
                return PlayerStates.GoToTarget;

            if (Agent.Health.Normalized > 0.3f)
                return PlayerStates.Attack;

            return StateKey;
        }

        private void MoveToFleePoint()
        {
            var fleePoint = GameManager.Instance.GroundSystem.GetRandomPoint();
            while (GameManager.Instance.Enemies.Any(enemy => Vector2.Distance(enemy.transform.position, fleePoint) <= GameManager.Instance.EnemySearchRadius))
            {
                fleePoint = GameManager.Instance.GroundSystem.GetRandomPoint();
            }

            Agent.Movement.SetTarget(fleePoint);
        }
    }
}