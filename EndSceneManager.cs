using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    [Header("����̃X�R�A")] public Text currentScoreText;
    [Header("�n�C�X�R�A")]  public Text highScoreText;
    [Header("�t�F�[�h")] public FadeImage fade;
    [Header("�Q�[���I�����ɌĂ΂��SE")] public AudioClip finishSE;

    private bool firstPush = false;
    private bool goNextScene = false;

    void Start()
    {
        int currentScore = PlayerPrefs.GetInt("CurrentScore", 0);
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
    }
}
