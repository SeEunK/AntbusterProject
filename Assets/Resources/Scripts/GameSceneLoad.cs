using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneLoad : MonoBehaviour
{
 public void PlayGameSceneLoad()
    {
        SceneManager.LoadScene("GameScene");

        TitleScene.instance.SetTitleSceneState(TitleScene.TitleSceneState.Out);
    }
}
