using Packages.com.estenis.unityexts.Runtime.Coroutines;
using System;
using System.Collections;
using UnityEngine;

namespace Packages.Estenis.UnityExts_ {
  public static class CoroutineHelpers {

    public static IEnumerator ExecuteEvery(Action action, float seconds) {
      while (true) {
        yield return new WaitForSeconds(seconds);
        action();
      }
    }

    public static IEnumerator ExecuteOnceAfter(Action action, float seconds) {
      yield return new WaitForSeconds(seconds);
      action();
    }

    public static IEnumerator ExecuteOnceAfterUnscaled(Action action, float seconds) {
      yield return new WaitForSecondsRealtime(seconds);
      action();
    }

    public static IEnumerator ExecuteOnceAfterFrames(Action action, int frames) {
      for (int i = 0; i < frames; i++) {
        yield return new WaitForFrames(frames);
      }
      action();
    }

    public static IEnumerator ExecuteEveryAfter(Action action, float every, float after) {
      yield return new WaitForSeconds(after);
      while (true) {
        action();
        yield return new WaitForSeconds(every);
      }
    }

    internal static IEnumerator ExecuteEveryFor(Action action, float everySeconds, float forSeconds, Action executeAfter) {
      int iterations = (int)(forSeconds / everySeconds);
      float remainderSeconds = forSeconds % everySeconds;

      for (int i = 0; i < iterations; i++) {
        yield return new WaitForSeconds(everySeconds);
        action();
      }

      if (remainderSeconds > 0) {
        yield return new WaitForSeconds(everySeconds);
        action();
      }

      executeAfter?.Invoke();
    }

    internal static IEnumerator ExecuteEveryForOrUntil(Action action, float everySeconds, float forSeconds, Func<bool> until, Action onBreakExecute) {
      int iterations = (int)(forSeconds / everySeconds);
      float remainderSeconds = forSeconds % everySeconds;

      for (int i = 0; i < iterations; i++) {
        if (until()) {
          onBreakExecute?.Invoke();
          yield break;
        }

        yield return new WaitForSeconds(everySeconds);
        action();
      }

      if (remainderSeconds > 0) {
        if (until()) {
          onBreakExecute?.Invoke();
          yield break;
        }

        yield return new WaitForSeconds(everySeconds);
        action();
      }

      onBreakExecute?.Invoke();
      yield break;
    }

    public static IEnumerator ExecuteEveryUntil(Action action, Func<bool> until, float everySeconds, Action onUntilExecute) {
      while (!until()) {
        action();
        yield return new WaitForSeconds(everySeconds);
      }

      onUntilExecute?.Invoke();
    }

    public static IEnumerator ExecuteEveryAfterUntil(Action action, float everySeconds, float afterSeconds, Func<bool> until, Action onUntilExecute) {
      yield return new WaitForSeconds(afterSeconds);
      while (!until()) {
        action();
        yield return new WaitForSeconds(everySeconds);
      }

      onUntilExecute?.Invoke();
    }
  }
}
