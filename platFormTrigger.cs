using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platFormTrigger : MonoBehaviour
{
    [Header("ìÆÇ©Ç∑è∞")] public GameObject[] platforms;
    [Header("ê⁄êGîªíË")] public PlayerTriggerCheck pTrigger;

    private bool began = false;
    private MoveObject[] mObj;

    void Start()
    {
        mObj = new MoveObject[platforms.Length];
        for (int i = 0; i < platforms.Length; i++)
        {
            mObj[i] = platforms[i].GetComponent<MoveObject>();
            Debug.Log(platforms[i]);
            Debug.Log(mObj[i].isStay);
        }
    }

    void Update()
    {
        if (pTrigger.isOn && !began)
        {
            for (int i = 0; i < mObj.Length; i++)
            {
                mObj[i].isStay = false;
            }
            began = true;
        }
    }
}
