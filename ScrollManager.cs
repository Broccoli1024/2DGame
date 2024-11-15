using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollManager : MonoBehaviour
{
    [SerializeField] public GameObject rankPrefab; // 順位用プレハブ
    [SerializeField] public GameObject namePrefab; // 名前用プレハブ
    [SerializeField] public GameObject scorePrefab; // スコア用プレハブ
    [SerializeField] public GameObject content; // スクロールのContent

    // ランキングデータを受け取り表示するメソッド
    public void UpdateLeaderboard(List<LeaderboardEntry> leaderboardEntries)
    {
        // 現在のコンテンツをクリア
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        // リストのデータを基にセルを生成
        foreach (var entry in leaderboardEntries)
        {
            // 順位、名前、スコアをそれぞれインスタンス化
            GameObject rankInstance = Instantiate(rankPrefab, content.transform);
            GameObject nameInstance = Instantiate(namePrefab, content.transform);
            GameObject scoreInstance = Instantiate(scorePrefab, content.transform);

            // 順位、名前、スコアのテキストを設定
            TextMeshProUGUI rankText = rankInstance.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI nameText = nameInstance.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI scoreText = scoreInstance.GetComponentInChildren<TextMeshProUGUI>();

            // テキストにデータをセット
            rankText.text = $"{entry.rank}位";
            nameText.text = entry.displayName;
            scoreText.text = $"{entry.score}";
        }
    }
}
