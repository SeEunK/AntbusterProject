using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{

    public static TitleScene instance;

    public enum TitleSceneState { In, Out }
    private TitleSceneState mState = TitleSceneState.Out;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Start()
    {
        SetTitleSceneState(TitleSceneState.In);
    }


    public void SetTitleSceneState(TitleSceneState state)
    {
        mState = state;
    }

    public TitleSceneState GetTitleSceneState()
    {
        return mState;
    }
}
