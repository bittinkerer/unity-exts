using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Packages.Estenis.UnityExts_
{
    public static class MonoBehaviourExtensions
    {
        /// <summary>
        /// Run routine every 'seconds'
        /// </summary>
        /// <param name="monoBehaviour"></param>
        /// <param name="action"></param>
        /// <param name="seconds"></param>
        public static Coroutine RunCoroutineEvery(this MonoBehaviour monoBehaviour, Action action, float seconds) =>
            monoBehaviour.StartCoroutine(
                CoroutineHelpers.ExecuteEvery(action, seconds));

        public static Coroutine RunCoroutineEveryForOrUntil(this MonoBehaviour monoBehaviour, Action action, float everySeconds, float forSeconds, Func<bool> until, Action onBreakExecute) =>
            monoBehaviour.StartCoroutine(
                CoroutineHelpers.ExecuteEveryForOrUntil(action, everySeconds, forSeconds, until, onBreakExecute));
       
        public static Coroutine RunCoroutineEveryUntil(this MonoBehaviour monoBehaviour, Action action, float everySeconds, Func<bool> until, Action onUntilExecute = null) =>
            monoBehaviour.StartCoroutine(
                CoroutineHelpers.ExecuteEveryUntil(action, until, everySeconds, onUntilExecute));

        public static Coroutine RunCoroutineEveryFor(this MonoBehaviour monoBehaviour, Action action, float everySeconds, float forSeconds) =>
            monoBehaviour.StartCoroutine(
                CoroutineHelpers.ExecuteEveryFor(action, everySeconds, forSeconds, () => { }));

        public static Coroutine RunCoroutineEveryFor(this MonoBehaviour monoBehaviour, Action action, float everySeconds, float forSeconds, Action executeAfter) =>
            monoBehaviour.StartCoroutine(
                CoroutineHelpers.ExecuteEveryFor(action, everySeconds, forSeconds, executeAfter));

        public static Coroutine RunCoroutineOnceAfter(this MonoBehaviour monoBehaviour, Action action, float seconds)
        {
            if(monoBehaviour == null || !monoBehaviour.gameObject.activeSelf)
            {
                Debug.LogWarning($"{monoBehaviour?.gameObject.name ?? ""}.{nameof(RunCoroutineOnceAfter)} called when GameObject is not active.");
                return null;
            }
            return monoBehaviour.StartCoroutine(CoroutineHelpers.ExecuteOnceAfter(action, seconds));
        }

        public static Coroutine RunCoroutineOnceAfter(this MonoBehaviour monoBehaviour, Action action, Func<bool> afterCondition, float conditionCheckFrequency = 0.03f) =>
            monoBehaviour.StartCoroutine(
                CoroutineHelpers.ExecuteEveryUntil(() => { }, afterCondition, conditionCheckFrequency, action));

        public static Coroutine RunCoroutineEveryAfter(this MonoBehaviour monoBehaviour, Action action, float every, float after) =>
            monoBehaviour.StartCoroutine(
                CoroutineHelpers.ExecuteEveryAfter(action, every, after));

        public static Coroutine RunCoroutineOnceAfterFrames(this MonoBehaviour monoBehaviour, Action action, int frames) =>
            monoBehaviour.StartCoroutine(
                CoroutineHelpers.ExecuteOnceAfterFrames(action, frames));


        public static bool ValidateAgainstNull(this MonoBehaviour monoBehaviour, GameObject arg, string argName, Action onValidateFail)
        {
            if (!arg)
            {
                Debug.LogError($"{monoBehaviour.name}.{argName} cannot be null on {monoBehaviour.gameObject.name}");
                onValidateFail();
                return false;
            }
            return true;
        }

        public static bool ValidateAgainstNull(this MonoBehaviour monoBehaviour, Component component, string argName, Action onValidateFail)
        {
            if (!component)
            {
                Debug.LogError($"{monoBehaviour.name}.{argName} cannot be null on {monoBehaviour.gameObject.name}");
                onValidateFail();
                return false;
            }
            return true;
        }

        public static bool ValidateAgainstNull(this MonoBehaviour monoBehaviour, object component, string argName, Action onValidateFail)
        {
            if (component == null)
            {
                Debug.LogError($"{monoBehaviour.name}.{argName} cannot be null.");
                onValidateFail();
                return false;
            }
            return true;
        }

        public static void DisableComponentsAndDestroy(this MonoBehaviour monoBehaviour)
        {
            foreach (var component in monoBehaviour.GetComponents<MonoBehaviour>()
                ?? Array.Empty<MonoBehaviour>())
            {
                component.enabled = false;
            }
            UnityEngine.Object.Destroy(monoBehaviour.gameObject);
        }

        public static void DisableComponents(this MonoBehaviour monoBehaviour, IEnumerable<Func<MonoBehaviour, bool>> except)
        {
            foreach (var component in monoBehaviour.GetComponents<MonoBehaviour>()
                .Where(c => !except.Any(f => f(c)))
                ?? Array.Empty<MonoBehaviour>())
            {
                component.enabled = false;
            }
        }

        public static bool IsNull(this MonoBehaviour monoBehaviour)
        {
            return (object)monoBehaviour == null;
        }

        

    }
}
