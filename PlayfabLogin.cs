using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;

public class PlayFabLogin : MonoBehaviour
{
    protected PlayFabAuthenticationContext player;
    protected string displayName;
    [SerializeField] private List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();
    private static PlayFabLogin _instance;//シングルトンインスタンス
    private bool isLoggedIn = false; //ログイン状態かどうかのチェック
    

    public static PlayFabLogin Instance;
    public bool IsClientLoggedIn
    {
        get { return isLoggedIn; }
    }

    // Awakeでシングルトンの設定
    private void Awake()
    {
        if (_instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void OnEnable()
    {
        PlayFabAuthService.OnLoginSuccess += PlayFabAuthService_OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError += PlayFabAuthService_OnPlayFabError;
    }

    private void OnDisable()
    {
        PlayFabAuthService.OnLoginSuccess -= PlayFabAuthService_OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError -= PlayFabAuthService_OnPlayFabError;
    }

    private void PlayFabAuthService_OnLoginSuccess(LoginResult success)
    {
        Debug.Log("ログイン成功");
        isLoggedIn = true;
        player = PlayFabSettings.staticPlayer;
    }
    private void PlayFabAuthService_OnPlayFabError(PlayFabError error)
    {
        Debug.Log("ログイン失敗");
    }
    void Start()
    {
        PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
        isLoggedIn = false;
    }

    public void SetDisplayName(string userName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = userName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSuccess, OnError);

        void OnSuccess(UpdateUserTitleDisplayNameResult result)
        {
            Debug.Log("success!");
            GetDisplayName(player.PlayFabId);
        }

        void OnError(PlayFabError error)
        {
            Debug.Log($"{error.Error}");
        }
    }

    public void GetDisplayName(string playfabId)
    {
        PlayFabClientAPI.GetPlayerProfile(
            new GetPlayerProfileRequest
            {
                PlayFabId = playfabId,
                ProfileConstraints = new PlayerProfileViewConstraints
                {
                    ShowDisplayName = true
                }
            },
            result => {
                displayName = result.PlayerProfile.DisplayName;
                Debug.Log($"DisplayName: {displayName}");
            },
            error => {
                Debug.LogError(error.GenerateErrorReport());
            }
        );
    }

    public void SubmitScore(int playerScore)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "HighScore",
                    Value = playerScore
                }
            }
        }, result =>
        {
            Debug.Log($"スコア {playerScore} 送信完了！");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void GetLeaderboard()
    {
        Debug.Log("ランキングの取得");
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "HighScore"
        }, result =>
        {
            // リストをクリア
            leaderboardEntries.Clear();

            // ランキングデータをリストに追加
            foreach (var item in result.Leaderboard)
            {
                string displayName = item.DisplayName ?? "NoName";
                int rank = item.Position + 1;
                int score = item.StatValue;

                // リストにエントリーを追加
                leaderboardEntries.Add(new LeaderboardEntry(rank, displayName, score));
            }

            // ScrollManagerに表示更新を依頼
            FindObjectOfType<ScrollManager>().UpdateLeaderboard(leaderboardEntries);

        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }
}

// ランキングのエントリを表すクラス
public class LeaderboardEntry
{
    public int rank;
    public string displayName;
    public int score;

    public LeaderboardEntry(int rank, string displayName, int score)
    {
        this.rank = rank;
        this.displayName = displayName;
        this.score = score;
    }
}