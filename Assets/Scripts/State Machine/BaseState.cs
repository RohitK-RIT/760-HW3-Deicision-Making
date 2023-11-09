using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace State_Machine
{
    /// <summary>
    /// Base state of a generic state machine
    /// </summary>
    /// <typeparam name="TState">State Enum</typeparam>
    public abstract class BaseState<TState> where TState : Enum
    {
        /// <summary>
        /// Constructor for the base state.
        /// </summary>
        /// <param name="key">key of the state</param>
        /// <param name="gameObject">gameObject to which the state machine is attached</param>
        /// <param name="stateMachine">owner state machine</param>
        public BaseState(TState key, GameObject gameObject, StateMachine<TState> stateMachine)
        {
            StateKey = key;
            GameObject = gameObject;
            StateMachine = stateMachine;
        }

        /// <summary>
        /// Property of state key of the state.
        /// </summary>
        public TState StateKey { get; private set; }
        /// <summary>
        /// Property to access the gameObject the state machine is attached to.
        /// </summary>
        protected GameObject GameObject { get; }
        /// <summary>
        /// Transform of the gameObject the state machine is attached to.
        /// </summary>
        protected Transform Transform => GameObject != null ? GameObject.transform : null;
        /// <summary>
        /// Property to access the owner state machine.
        /// </summary>
        protected StateMachine<TState> StateMachine { get; }
        
        /// <summary>
        /// Function called when entering the state.
        /// </summary>
        public virtual void EnterState()
        {
            
        }

        /// <summary>
        /// Function called when exiting the state.
        /// </summary>
        public virtual void ExitState()
        {
            
        }
        
        /// <summary>
        /// Function called to update the state.
        /// </summary>
        public virtual void UpdateState()
        {
            
        }

        /// <summary>
        /// Function to get the next state.
        /// </summary>
        /// <returns>next state's key</returns>
        public abstract TState GetNextState();

        /// <summary>
        /// Function to start a coroutine in a state.
        /// </summary>
        /// <param name="enumerator">Coroutine to be run</param>
        public Coroutine StartCoroutine(IEnumerator enumerator)
        {
            return StateMachine.StartCoroutine(enumerator);
        }

        /// <summary>
        /// Function to stop a coroutine in a state.
        /// </summary>
        /// <param name="coroutine"></param>
        public void StopCoroutine(Coroutine coroutine)
        {
            StateMachine.StopCoroutine(coroutine);
        }

        /// <summary>
        /// Function to stop all the running coroutines.
        /// </summary>
        public void StopAllCoroutine()
        {
            StateMachine.StopAllCoroutines();
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
        }

        public virtual void OnTriggerStay2D(Collider2D other)
        {
        }

        public virtual void OnTriggerExit2D(Collider2D other)
        {
        }

        public virtual void OnCollisionEnter2D(Collision2D other)
        {
        }

        public virtual void OnCollisionStay2D(Collision2D other)
        {
        }

        public virtual void OnCollisionExit2D(Collision2D other)
        {
        }

        /// <summary>
        /// Function to destroy an object in a state.
        /// </summary>
        /// <param name="obj">object to be destroyed</param>
        public void Destroy(Object obj)
        {
            MonoBehaviour.Destroy(obj);
        }

        /// <summary>
        /// Function to immediately destroy an object in a state.
        /// </summary>
        /// <param name="obj">object to be destroyed</param>
        public void DestroyImmediate(Object obj)
        {
            MonoBehaviour.DestroyImmediate(obj);
        }
    }
}