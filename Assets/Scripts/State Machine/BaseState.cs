using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace State_Machine
{
    public abstract class BaseState<TState> where TState : Enum
    {
        public BaseState(TState key, GameObject gameObject, StateMachine<TState> stateMachine)
        {
            StateKey = key;
            GameObject = gameObject;
            StateMachine = stateMachine;
        }

        public TState StateKey { get; private set; }
        protected GameObject GameObject { get; }
        protected Transform Transform => GameObject != null ? GameObject.transform : null;
        protected virtual StateMachine<TState> StateMachine { get; }
        public virtual void EnterState()
        {
            
        }

        public virtual void ExitState()
        {
            
        }

        public virtual void UpdateState()
        {
            
        }

        public abstract TState GetNextState();

        public Coroutine StartCoroutine(IEnumerator enumerator)
        {
            return StateMachine.StartCoroutine(enumerator);
        }

        public void StopCoroutine(Coroutine coroutine)
        {
            StateMachine.StopCoroutine(coroutine);
        }

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

        public void Destroy(Object obj)
        {
            MonoBehaviour.Destroy(obj);
        }

        public void DestroyImmediate(Object obj)
        {
            MonoBehaviour.DestroyImmediate(obj);
        }
    }
}