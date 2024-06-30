using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
    [Header("�t�F�[�h")] public FadeImage fade;
    [Header("�Q�[���X�^�[�g���ɌĂ΂��SE")] public AudioClip startSE;

    private bool firstPush = false;
    private bool goNextScene = false;

    //�X�^�[�g�{�^���������ꂽ��Ă΂��
    public void PressStart()
    {
        Debug.Log("Press Start!");
        if (!firstPush)
        {
            Debug.Log("Go Next Scene!");
            fade.StartFadeOut();
            firstPush = true;
            GManager.instance.PlaySE(startSE);
        }
    }

    private void Update()
    {
        if (!goNextScene && fade.IsFadeOutComplete())
        {
            SceneManager.LoadScene("stage1");
            goNextScene = true;
        }
    }
}