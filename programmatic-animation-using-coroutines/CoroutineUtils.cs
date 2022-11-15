using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Offers util methods for coroutines
/// </summary>
public static class CoroutineUtil {

  /// <summary>
  /// provides a util to easily control the timing of a lerp over a duration
  /// </summary>
  /// <param name="duration">How long our lerp will take</param>
  /// <param name="action">The action to perform per frame of the lerp, is given the progress t in [0,1]</param>
  /// <param name="curve">If we want out time curve to follow a specific animation curve</param>
  /// <returns></returns>
  public static IEnumerator Lerp(float duration, Action<float> action, bool realTime = false, bool smooth = false, AnimationCurve curve = null, bool inverse = false) {
    float time = 0;
    Func<float, float> tEval = t => t;
    if (smooth) tEval = t => Mathf.SmoothStep(0, 1, t);
    if (curve != null) tEval = t => curve.Evaluate(t);
    while (time < duration) {
      float delta = realTime ? Time.fixedDeltaTime : Time.deltaTime;
      float t = (time + delta > duration) ? 1 : (time / duration);
      if (inverse)
        t = 1 - t;
      action(tEval(t));
      time += delta;
      yield return null;
    }
    action(tEval(inverse ? 0 : 1));
  }

  /// <summary>
  /// Starts the lerp routine
  /// </summary>
  /// <param name="obj"></param>
  /// <param name="duration"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static Coroutine Lerp(MonoBehaviour obj, float duration, Action<float> action) {
    return obj.StartCoroutine(Lerp(duration, action));
  }
}