// using System;
// using UnityEngine;
//
// namespace Leopotam.EcsLite.PhysicsExtensions
// {
//     internal sealed class TriggerDispatcher : MonoBehaviour
//     {
//         [SerializeField]
//         private GameObject source;
//         
//         [SerializeField]
//         private bool enqueueEvents = true;
//
//         private void OnTriggerEnter(Collider collider)
//         {
//             PhysicsWorld.OnTriggerEnter(this.source, collider, this.enqueueEvents);
//         }
//
//         private void OnTriggerExit(Collider collider)
//         {
//             PhysicsWorld.OnTriggerExit(this.source, collider, this.enqueueEvents);
//         }
//
//         private void Reset()
//         {
//             this.source = this.gameObject;
//         }
//     }
// }
