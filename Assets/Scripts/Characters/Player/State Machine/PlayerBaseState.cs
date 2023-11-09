using Characters.Base.State_Machine;
using UnityEngine;

namespace Characters.Player.State_Machine
{
    public abstract class PlayerBaseState : AgentBaseState<PlayerStates, Player, PlayerStateMachine>
    {
        protected PlayerBaseState(PlayerStates key, GameObject gameObject, PlayerStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
        }
    }
}