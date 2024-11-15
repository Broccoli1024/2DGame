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
    private static PlayFabLogin _instance;//�V���O���g���C���X�^���X
    private bool isLoggedIn = false; //���O�C����Ԃ��ǂ����̃`�F�b�N
    

    public static PlayFabLogin Instance;
    public bool IsClientLoggedIn
    {
        get { return isLoggedIn; }
    }

    // Awake�ŃV���O���g���̐ݒ�
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
        Debug.Log("���O�C������");
        isLoggedIn = true;
        player = PlayFabSettings.staticPlayer;
    }
    private void PlayFabAuthService_OnPlayFabError(PlayFabError error)
    {
        Debug.Log("���O�C�����s");
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
            Debug.Log($"�X�R�A {playerScore} ���M�����I");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void GetLeaderboard()
    {
        Debug.Log("�����L���O�̎擾");
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "HighScore"
        }, result =>
        {
            // ���X�g���N���A
            leaderboardEntries.Clear();

            // �����L���O�f�[�^�����X�g�ɒǉ�
            foreach (var item in result.Leaderboard)
            {
                string displayName = item.DisplayName ?? "NoName";
                int rank = item.Position + 1;
                int score = item.StatValue;

                // ���X�g�ɃG���g���[��ǉ�
                leaderboardEntries.Add(new LeaderboardEntry(rank, displayName, score));
            }

            // ScrollManager�ɕ\���X�V���˗�
            FindObjectOfType<ScrollManager>().UpdateLeaderboard(leaderboardEntries);

        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }
}

// �����L���O�̃G���g����\���N���X
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