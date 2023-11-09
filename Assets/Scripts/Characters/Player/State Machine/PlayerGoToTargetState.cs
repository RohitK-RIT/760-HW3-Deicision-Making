using UnityEngine;

namespace Characters.Player.State_Machine
{
    /// <summary>
    /// Player's go to target state.
    /// </summary>
    public class PlayerGoToTargetState : PlayerBaseState
    {
        /// <summary>
        /// Constructor for player's go to target state.
        /// </summary>
        /// <param name="key">key of the state</param>
        /// <param name="gameObject">gameObject to which the state machine is attached</param>
        /// <param name="stateMachine">owner state machine</param>
        public PlayerGoToTargetState(PlayerStates key, GameObject gameObject, PlayerStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
        }

        public override void EnterState()
        {
            // If player has a target, then set it as the target for the movement component.
            if (GameManager.Instance.Target)
                Agent.Movement.SetTarget(GameManager.Instance.Target.position);
        }

        public override PlayerStates GetNextState()
        {
            // If there is not target then go to idle state.
            if (!GameManager.Instance.Target)
                return PlayerStates.Idle;

            // If an enemy is in range then according to the health attack or flee.
            if (Agent.HasEnemyInRange())
                return Agent.Health.Normalized < 0.3 ? PlayerStates.Flee : PlayerStates.Attack;
            
            // Else stay in the go to target state.
            return StateKey;
        }

        public override void OnCollisionEnter2D(Collision2D other)
        {
            // Check for collision with target.
            if (!other.gameObject.CompareTag("Target")) return;
            
            // If yes then destroy the target.
            Destroy(other.gameObject);
            // And send a notification to player component that the target has been achieved.
            Agent.TargetAchieved();
        }
    }
}