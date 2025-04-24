using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameField;
    [SerializeField] private Button connectButton;

    public Action OnConnectButtonClicked;

    private void Start()
    {
        nicknameField.onValueChanged.AddListener(CheckNickname);
        connectButton.onClick.AddListener(HandleConnectClick);
    }

    private void CheckNickname(string arg0)
    {
        ToggleConnectButton(arg0.Length != 0);
    }

    private void ToggleConnectButton(bool active)
    {
        connectButton.interactable = active;
    }

    private void HandleConnectClick()
    {
        OnConnectButtonClicked?.Invoke();
        ConnectionManager.instance.SetNickName(nicknameField.text);
        ConnectionManager.instance.ConnectToServer(GoToLobby);
    }

    private void GoToLobby()
    {
        MySceneManager.instance.LoadScene(ScenesEnum.Lobby);
    }

}
