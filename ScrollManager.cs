using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollManager : MonoBehaviour
{
    [SerializeField] public GameObject rankPrefab; // ���ʗp�v���n�u
    [SerializeField] public GameObject namePrefab; // ���O�p�v���n�u
    [SerializeField] public GameObject scorePrefab; // �X�R�A�p�v���n�u
    [SerializeField] public GameObject content; // �X�N���[����Content

    // �����L���O�f�[�^���󂯎��\�����郁�\�b�h
    public void UpdateLeaderboard(List<LeaderboardEntry> leaderboardEntries)
    {
        // ���݂̃R���e���c���N���A
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        // ���X�g�̃f�[�^����ɃZ���𐶐�
        foreach (var entry in leaderboardEntries)
        {
            // ���ʁA���O�A�X�R�A�����ꂼ��C���X�^���X��
            GameObject rankInstance = Instantiate(rankPrefab, content.transform);
            GameObject nameInstance = Instantiate(namePrefab, content.transform);
            GameObject scoreInstance = Instantiate(scorePrefab, content.transform);

            // ���ʁA���O�A�X�R�A�̃e�L�X�g��ݒ�
            TextMeshProUGUI rankText = rankInstance.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI nameText = nameInstance.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI scoreText = scoreInstance.GetComponentInChildren<TextMeshProUGUI>();

            // �e�L�X�g�Ƀf�[�^���Z�b�g
            rankText.text = $"{entry.rank}��";
            nameText.text = entry.displayName;
            scoreText.text = $"{entry.score}";
        }
    }
}
