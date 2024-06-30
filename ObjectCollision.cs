using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("これを踏んだ時のプレイヤーが跳ねる高さ")] public float boundHeight;
    /// <summary>
    /// このオブジェクトをプレイヤーが踏んだかどうか
    /// </summary>
    [HideInInspector] public bool playerStepOn;
}
