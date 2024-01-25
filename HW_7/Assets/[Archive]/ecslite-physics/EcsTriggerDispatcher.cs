// using UnityEngine;
//
// namespace EcsEngine
// {
//     internal sealed class EcsTriggerDispatcher : MonoBehaviour
//     {
//         private void OnTriggerEnter(Collider other)
//         {
//             Debug.Log($"ON TRIGGER ENTER {other.name}", this);
//             EcsFacade.Instance.TriggerEnter(this.gameObject, other);
//         }
//     }
// }

// internal void TriggerEnter(GameObject source, Collider other)
// {
//     EcsPool<TriggerEnterEvent> eventPool = _events.GetPool<TriggerEnterEvent>();
//
//     int @event = _events.NewEntity();
//     eventPool.Add(@event) = new TriggerEnterEvent
//     {
//         source = source,
//         collider = other
//     };
// }