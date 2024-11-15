using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTextManager : PlayFabLogin
{
    [SerializeField] InputField inputField;
    [SerializeField] Text text;
    [SerializeField] GameObject renamePop;
    private bool initialFlag = false;//���O�C����̐ݒ肪�����������ǂ���

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
        //�e�L�X�g��inputField�̓��e�𔽉f
        text.text = "���݂̖��O: " + displayName;
    }
}