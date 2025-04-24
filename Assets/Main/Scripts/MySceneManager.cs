using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MySceneManager : Singleton<MySceneManager>
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(ScenesEnum sceneEnum)
    {
        string sceneName = GetSceneName(sceneEnum);
        SceneManager.LoadScene(sceneName); 
    }

    public void LoadSceneForAll(ScenesEnum sceneEnum)
    {
        string sceneName = GetSceneName(sceneEnum);
    }

    private string GetSceneName(ScenesEnum sceneEnum)
    {
        switch(sceneEnum)
        {
            case ScenesEnum.MainMenu:
                return "MainMenu";
            case ScenesEnum.Lobby:
                return "Lobby";
            case ScenesEnum.Game:
                return "Game";
        }

        return "";
    }


}

public enum ScenesEnum
{
    MainMenu,
    Lobby,
    Game,
}