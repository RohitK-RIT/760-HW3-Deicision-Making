using UnityEngine;

namespace Characters.Player.State_Machine
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStates key, GameObject gameObject, PlayerStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
        }

        public override PlayerStates GetNextState()
        {
            if (GameManager.Instance.Target)
                return PlayerStates.GoToTarget;
            
            if (Agent.HasEnemyInRange())
                return Agent.Health.Normalized < 0.3 ? PlayerStates.Flee : PlayerStates.Attack;

            return StateKey;
        }
    }
}