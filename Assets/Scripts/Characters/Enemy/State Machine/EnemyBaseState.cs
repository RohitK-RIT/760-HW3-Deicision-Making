using Characters.Base.State_Machine;
using UnityEngine;

namespace Characters.Enemy.State_Machine
{
    public abstract class EnemyBaseState : AgentBaseState<EnemyStates, Enemy, EnemyStateMachine>
    {
        protected EnemyBaseState(EnemyStates key, GameObject gameObject, EnemyStateMachine stateMachine) : base(key, gameObject, stateMachine)
        {
        }
    }
}