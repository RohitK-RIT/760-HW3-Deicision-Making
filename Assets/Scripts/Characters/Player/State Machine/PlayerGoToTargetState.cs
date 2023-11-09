using System.Linq;
using UnityEngine;

namespace Characters.Player.State_Machine
{
    public class PlayerGoToTargetState : PlayerBaseState
    {
        public PlayerGoToTargetState(PlayerStates key, GameObject gameObject, PlayerStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
        }

        public override void EnterState()
        {
            if (GameManager.Instance.Target)
                Agent.Movement.SetTarget(GameManager.Instance.Target.position);
        }

        public override PlayerStates GetNextState()
        {
            if (!GameManager.Instance.Target)
                return PlayerStates.Idle;

            if (Agent.HasEnemyInRange())
                return Agent.Health.Normalized < 0.3 ? PlayerStates.Flee : PlayerStates.Attack;
            
            return StateKey;
        }

        public override void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Target")) return;
            
            Destroy(other.gameObject);
            Agent.TargetAchieved();
        }
    }
}