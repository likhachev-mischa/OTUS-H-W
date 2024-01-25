// using UnityEngine;
//
// namespace Leopotam.EcsLite.PhysicsExtensions
// {
//     internal sealed class CollisionDispatcher : MonoBehaviour
//     {
//         [SerializeField]
//         private GameObject source;
//
//         [SerializeField]
//         private bool enqueueEvents = true;
//
//         private void OnCollisionEnter(Collision collision)
//         {
//             PhysicsWorld.OnCollisionEnter(this.source, collision, this.enqueueEvents);
//         }
//
//         private void OnCollisionExit(Collision collision)
//         {
//             PhysicsWorld.OnCollisionExit(this.source, collision, this.enqueueEvents);
//         }
//
//         private void Reset()
//         {
//             this.source = this.gameObject;
//         }
//     }
// }