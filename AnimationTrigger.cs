using UnityEngine;
using System.Collections;

public class AnimationTrigger : MonoBehaviour
{
    #region//�C���X�y�N�^�[�Őݒ肷��
    [Header("�A�j���[�^�[�R���|�[�l���g")]  public Animator animator;
    [Header("�A�j���[�V�����̃g���K�[��")] public string animationTriggerName;
    [Header("�A�j���[�V�����Đ�����")] public float animationDuration = 2.0f;
    [Header("�������[�v����")] public bool shouldLoop = false;
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
        // �A�j���[�V�������g���K�[����
        animator.SetTrigger(animationTriggerName);

        if (shouldLoop)
        {
            while (true)
            {
                // �w��̎��Ԃ��o�߂���܂őҋ@
                yield return new WaitForSeconds(animationDuration);

                break;
            }
        }
        else
        {
            // �A�j���[�V�������w��̎��Ԃ����Đ������
            yield return new WaitForSeconds(animationDuration);
        }
    }
}
