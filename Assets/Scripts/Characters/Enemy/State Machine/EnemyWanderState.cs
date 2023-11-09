using UnityEngine;

namespace Characters.Enemy.State_Machine
{
    public class EnemyWanderState : EnemyBaseState
    {
        public EnemyWanderState(EnemyStates key, GameObject gameObject, EnemyStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
        }

        public override void EnterState()
        {
            SetRandomTarget();
        }

        private void SetRandomTarget()
        {
            Agent.Movement.SetTarget(GameManager.Instance.GroundSystem.GetRandomPoint());
        }

        public override void UpdateState()
        {
            if (!Agent.Movement.HasTarget)
                SetRandomTarget();
        }

        public override EnemyStates GetNextState()
        {
            if (!Agent.HasEnemyInRange())
                return StateKey;

            return EnemyStates.Attack;
        }
    }
}