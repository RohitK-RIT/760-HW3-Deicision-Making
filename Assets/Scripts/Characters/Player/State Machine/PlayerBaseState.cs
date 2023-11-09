using Characters.Base.State_Machine;
using UnityEngine;

namespace Characters.Player.State_Machine
{
    /// <summary>
    /// Player's base state.
    /// </summary>
    public abstract class PlayerBaseState : AgentBaseState<PlayerStates, Player, PlayerStateMachine>
    {
        /// <summary>
        /// Constructor for the player base state.
        /// </summary>
        /// <param name="key">key of the state</param>
        /// <param name="gameObject">gameObject to which the state machine is attached</param>
        /// <param name="stateMachine">owner state machine</param>
        protected PlayerBaseState(PlayerStates key, GameObject gameObject, PlayerStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
        }
    }
}