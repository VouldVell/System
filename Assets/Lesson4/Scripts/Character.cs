using System;
using Unity.Netcode;
using UnityEngine;


namespace System_Programming.Lesson4
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class Character : NetworkBehaviour
    {
        protected Action OnUpdateAction { get; set; }
        protected abstract FireAction FireAction { get; set; }
        protected NetworkVariable<Vector3> _serverPosition = new NetworkVariable<Vector3>(Vector3.zero,
            NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        protected NetworkVariable<Quaternion> _serverRotation = new NetworkVariable<Quaternion>(Quaternion.identity,
            NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        

        protected virtual void Initiate()
        {
            OnUpdateAction += Movement;
        }

        private void Update()
        {
            OnUpdateAction?.Invoke();
        }

        public abstract void Movement();
    }
}