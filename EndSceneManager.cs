using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneManager : PlayFabLogin
{
    [Header("����̃X�R�A")] public Text currentScoreText;
    [Header("�n�C�X�R�A")]  public Text highScoreText;
    [Header("�t�F�[�h")] public FadeImage fade;
    [Header("�Q�[���I�����ɌĂ΂��SE")] public AudioClip finishSE;
    private int currentScore = 0;
    private bool initialFlag = false;//���O�C����̐ݒ肪�������Ă��邩

    private bool firstPush = false;
    private bool goNextScene = false;

    void Start()
    {
        currentScore = PlayerPrefs.GetInt("CurrentScore", 0);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        currentScoreText.text = "Score: " + currentScore;
        highScoreText.text = "High Score: " + highScore;
    }

    public void PressBack()
    {
        Debug.Log("Press Start!");
        if (!firstPush)
        {
            Debug.Log("Back To Title");
            fade.StartFadeOut();
            firstPush = true;
            GManager.instance.PlaySE(finishSE);
        }
    }

    private void Update()
    {
        if (!goNextScene && fade.IsFadeOutComplete())
        {
            GManager.instance.RetryGame();
            SceneManager.LoadScene("titleScene");
            goNextScene = true;
        }

        if (Instance.IsClientLoggedIn && !initialFlag)
        {
            SubmitScore(currentScore);
            initialFlag = true;
        }
    }
}
