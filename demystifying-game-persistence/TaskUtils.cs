using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public static class TaskUtils {
  /// <summary>
  /// Helper to yield on task completion
  /// TODO handle task errors
  /// </summary>
  /// <param name="task"></param>
  /// <returns></returns>
  public static IEnumerator YieldTask(Task task) {
    yield return new WaitUntil(() => task.IsCompleted);
  }
}