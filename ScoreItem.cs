using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    #region // インスペクターで設定する
    [Header("加算するスコア")] public int myScore;
    [Header("プレイヤーの判定")] public PlayerTriggerCheck playerCheck;
    [Header("アイテム取得時に鳴らすSE")] public AudioClip itemSE;
    #endregion

    #region // プライベート変数
    private bool isGet = false;
    private Animator anim = null;
    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // プレイヤーが判定内に入ったときの処理
        if (playerCheck.isOn)
        {
            if (GManager.instance != null && isGet == false)
            {
                GManager.instance.score += myScore;
                GManager.instance.PlaySE(itemSE);
                anim.Play("Item-feedback-Animation");
                isGet = true;
                StartCoroutine(DestroyAfterAnimation());
            }
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // アニメーションの長さを取得
        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;
        // アニメーションが終わるまで待機
        yield return new WaitForSeconds(animationLength);
        // オブジェクトを破壊
        Destroy(gameObject);
    }
}
