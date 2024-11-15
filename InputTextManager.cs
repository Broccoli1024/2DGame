using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTextManager : PlayFabLogin
{
    [SerializeField] InputField inputField;
    [SerializeField] Text text;
    [SerializeField] GameObject renamePop;
    private bool initialFlag = false;//ログイン後の設定が完了したかどうか

    void Start()
    {
        inputField = inputField.GetComponent<InputField>();
        text = text.GetComponent<Text>();
    }

    private void Update()
    {
        if (Instance.IsClientLoggedIn && !initialFlag)
        {
            GetDisplayName(player.PlayFabId);
            SetDisplayName(displayName);
            GetLeaderboard();
            initialFlag = true;
        }
    }

    public void InputText()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            renamePop.SetActive(false);
            GetDisplayName(player.PlayFabId);
            SetDisplayName(inputField.text);
            GetLeaderboard();
        }
        //テキストにinputFieldの内容を反映
        text.text = "現在の名前: " + displayName;
    }
}