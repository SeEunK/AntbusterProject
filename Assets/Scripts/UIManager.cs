using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Button mBtnCannonDeploy = null;
    public TMP_Text mTextCountDown = null;
    public TMP_Text mTextPoint = null;
    public TMP_Text mTextMoney = null;
    public TMP_Text mTextLevel = null;

    public int mCountDown = 10;

    public GameObject mCannonInfoPopup = null;

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

    public void Update()
    {
        if(mCountDown == 0)
        {
            mTextCountDown.gameObject.SetActive(false);
        }
    }

    public void SetCannonInfoPopupActive(bool value)
    {
        mCannonInfoPopup.SetActive(value);
    }
    public void SetTextPoint(int value)
    {
        mTextPoint.text = ("Point\n"+value.ToString());
    }
    public void SetTextMoney(int value)
    {
        mTextMoney.text = ("Money\n" + value.ToString());
    }
    public void SetTextLevel(int value)
    {
        mTextLevel.text = ("Level\n" + value.ToString());
    }

    public void SetTextCountDown(int value)
    {
        mTextCountDown.text = (value.ToString());
    }

    //private IEnumerator UpdateCountDown()
    //{
    //    while (mCountDown >0)
    //    {
    //        SetTextCountDown(mCountDown -= 1);

    //        yield return new WaitForSeconds(1.0f);
            
    //    }
      
    //}



}
