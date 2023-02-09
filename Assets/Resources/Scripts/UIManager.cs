using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Button mBtnCannonDeploy = null;
    public GameObject mTextCountDown = null;
    public GameObject mTextPoint = null;
    public GameObject mTextMoney = null;
    public GameObject mTextLevel = null;
    public GameObject mBtnUpgrade = null;
   

    public GameObject mCannonInfoPopup = null;
    public GameObject mSellectCannon = null;

    public GameObject mPnlGameOver = null;
    public Button mBtnExit = null;
    public Button mBtnRetry= null;

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

    private void Start()
    {
        InItUI();
        //StartCoroutine(UpdateCountDown());
        SetCannonInfoPopupActive(false);
        SetUpgradeButtonEnable(false);
        GameOverPanelSetActive(false); 


    }
    public void CannonDeployButtonOnClick()
    {

        BoardManager.instance.SetBoardState(BoardManager.BoadState.Deploy);
    }

    public void InItUI()
    {
        SetTextPoint(0);
        SetTextMoney(500);
        SetTextLevel(1);

    }
    public void SetUpgradeButtonEnable(bool enable)
    {
        mBtnUpgrade.SetActive(enable);
    }
    
    public void SetSellectCannon(GameObject sellectCannon)
    {
        mSellectCannon = sellectCannon;
    }

    public void CannonUpgradeButtonOnClick()
    {
        mSellectCannon.GetComponent<Cannon>().CannonUpgrade();
        SetUpgradeButtonEnable(false);
    }

    public void GameOverPanelSetActive(bool value)
    {
        Debug.Log($"gameover setActive : {value}");
        mPnlGameOver.SetActive(value);
    }

 
  

    public void SetCannonInfoPopupActive(bool value)
    {
        mCannonInfoPopup.SetActive(value);
        // 캐논 1렙 내용을 설정하고 보여줘야함
    }
    public void SetTextPoint(int value)
    {

        mTextPoint.SetTMPText($"Point\n {value}");
        //mTextPoint.text = ("Point\n"+value.ToString());
    }
    public void SetTextMoney(int value)
    {
        mTextMoney.SetTMPText($"Money\n {value}");
        //mTextMoney.text = ("Money\n" + value.ToString());
    }
    public void SetTextLevel(int value)
    {
        mTextLevel.SetTMPText($"Level\n {value}");
        //mTextLevel.text = ("Level\n" + value.ToString());
    }

    public void SetTextCountDown(int value)
    {
        mTextCountDown.SetTMPText(value);
        //mTextCountDown.text = (value.ToString());
    }

    public void QuitThisGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();

    }

    public void Retry()
    {
        GameOverPanelSetActive(false);
        GameManager.instance.mGameState = GameManager.GameState.GameReady;
        SceneManager.LoadScene("TitleScene");

    }


}
