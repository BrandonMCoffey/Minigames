using UnityEngine;
using UnityEngine.Events;
using Utility_Scripts.GameEvents.Logic;

namespace Utility_Scripts.GameEvents
{
    public class ActionOnEvent : GameEventListener
    {
        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent Response;

        public override void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}