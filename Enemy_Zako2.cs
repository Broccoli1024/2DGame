using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Zako2 : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("加算スコア")] public int myScore;
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("接触判定")] public EnemyCollisionCheck checkCollision;
    [Header("やられた時に鳴らすSE")] public AudioClip deadSE;
    [Header("プレイヤー")] public Transform player;
    [Header("移動させる")] public bool move;
    [Header("動く幅")] public float moveDis = 3.0f;
    [Header("初期位相")] public float topology = 0;
    #endregion

    #region//プライベート変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private ObjectCollision oc = null;
    private BoxCollider2D col = null;
    private bool isDead = false;
    private float kakudo = 1.0f;
    private Vector3 defaultPos;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        oc = GetComponent<ObjectCollision>();
        col = GetComponent<BoxCollider2D>();
        defaultPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!oc.playerStepOn)
        {
            if (sr.isVisible)
            {
                if (player.transform.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            if (move)
            {
                transform.position = defaultPos + Vector3.up * moveDis * Mathf.Sin((kakudo+topology) * Mathf.Deg2Rad);
                kakudo += 180.0f * Time.deltaTime * speed;
            }
        }
        else
        {
            if (!isDead)
            {
                if (GManager.instance != null)
                {

                    GManager.instance.score += myScore;
                    GManager.instance.PlaySE(deadSE);
                }

                isDead = true;
                col.enabled = false;
                anim.Play("Eagle-hurt-Animation");
                anim.Play("Enemt-death-Animation");
                Destroy(gameObject, 0.4f);
            }
        }

    }
}