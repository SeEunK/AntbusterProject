using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int mCakeCount = 0;
    public enum GameState { GameReady, GameStart, GameOver }
    public GameState mGameState = GameState.GameReady;

    public float mTimer = 0.0f;
    public int mCountDown = 10;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }


    private void Update()
    {

        if (mGameState == GameState.GameReady)
        {
            mTimer += Time.deltaTime;
            if (mTimer >= 1.0f)
            {
                mTimer = 0.0f;

                mCountDown -= 1;
                UIManager.instance.SetTextCountDown(mCountDown);

                if (mCountDown == 0)
                {
                    UIManager.instance.mTextCountDown.SetActive(false);
                    mGameState = GameState.GameStart;
                }
            }
        }
        if (mGameState == GameState.GameStart && mCakeCount >= 8)
        {
            mGameState = GameState.GameOver;
            UIManager.instance.GameOverPanelSetActive(true);
            mCountDown = 10;
            mCakeCount = 0;
            UIManager.instance.mTextCountDown.SetActive(true);
        }

    }

}
