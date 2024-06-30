using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    #region // �C���X�y�N�^�[�Őݒ肷��
    [Header("���Z����X�R�A")] public int myScore;
    [Header("�v���C���[�̔���")] public PlayerTriggerCheck playerCheck;
    [Header("�A�C�e���擾���ɖ炷SE")] public AudioClip itemSE;
    #endregion

    #region // �v���C�x�[�g�ϐ�
    private bool isGet = false;
    private Animator anim = null;
    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // �v���C���[��������ɓ������Ƃ��̏���
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
        // �A�j���[�V�����̒������擾
        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;
        // �A�j���[�V�������I���܂őҋ@
        yield return new WaitForSeconds(animationLength);
        // �I�u�W�F�N�g��j��
        Destroy(gameObject);
    }
}
