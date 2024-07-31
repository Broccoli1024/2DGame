using UnityEngine;
using System.Collections;

public class AnimationTrigger : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("アニメーターコンポーネント")]  public Animator animator;
    [Header("アニメーションのトリガー名")] public string animationTriggerName;
    [Header("アニメーション再生時間")] public float animationDuration = 2.0f;
    [Header("無限ループする")] public bool shouldLoop = false;
    #endregion

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PlayAnimationForDuration());
        }
    }

    IEnumerator PlayAnimationForDuration()
    {
        // アニメーションをトリガーする
        animator.SetTrigger(animationTriggerName);

        if (shouldLoop)
        {
            while (true)
            {
                // 指定の時間が経過するまで待機
                yield return new WaitForSeconds(animationDuration);

                break;
            }
        }
        else
        {
            // アニメーションが指定の時間だけ再生される
            yield return new WaitForSeconds(animationDuration);
        }
    }
}
