using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;
    public enum BoadState { None, Deploy }

    // ���߿� ���Ӹ޴��� ����� �̵��� ����---------------
    public int mCakeCount = 0;
    
    public enum GameState { GameReady, GameStart, GameOver}
    public GameState mGameState = GameState.GameReady;

    public float mTimer = 0.0f;
    public int mCountDown = 10;

    public void UpdateGame()
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
                    mGameState = GameState.GameStart;
                }
            }
        }

    }

    //------------------------------------------


    //���� ��ĭ
    public const float UNIT = 1.0f;
    // Ÿ�� ����
    public const int WIDTH = 25;
    // Ÿ�� ����
    public const int LENGTH = 20;

    // ���� ĭ 
    public static int[] Board = new int[] {
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
        };

    public Camera mCamera = null;
    public Transform mSellectPos = null;
    public BoadState mBoardState = BoadState.None;

    public GameObject mCannonPrefab = null;

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
        UpdateGame(); // 
        if (mBoardState == BoadState.Deploy)
        {
            UpdateDeployMode();
        }
    }

    public void UpdateDeployMode()
    {
        mSellectPos.gameObject.SetActive(true);

        // ���콺 ��ǥ�� ������ǥ�� ��ȯ
        Vector3 mousePos = mCamera.ScreenToWorldPoint(Input.mousePosition);

        // ���콺���� ��ǥ�� ������� �ε����� ��ȯ
        int posX = Mathf.Clamp((int)(mousePos.x / UNIT), 0, WIDTH - 1);
        int posY = Mathf.Clamp((int)(mousePos.y / UNIT), 0, LENGTH - 1);

        int index = posY * WIDTH + posX;

        mousePos.z = 0.0f;
        mousePos.x = posX;
        mousePos.y = posY;

        mSellectPos.position = mousePos;

        // ���콺 Ŀ�� ��ġ ����ĭ�� ��ġ ������ ��� 
        if (Board[index] == 0)
        {
            mSellectPos.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            mSellectPos.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }

        // ���콺Ŭ��
        if (Input.GetMouseButtonDown(0))
        {
            // 0(��ġ������ ��) �� ��� 
            if (Board[index] == 0)
            {
                Board[index] = 2; // ��ġ�����κ���

                //
                GameObject deployCannon = GameObject.Instantiate<GameObject>(mCannonPrefab);
                //GameObject deployCannon = GameObject.Instantiate<GameObject>(mSellectPos.gameObject);
                deployCannon.transform.position = mousePos;

                SetBoardState(BoadState.None);
            }
            else 
            {
                // Ŭ���Ѱ��� 0�̾ƴѰ��(��ġ�Ұ����� or �̹� ������ġ�Ȱ�� or ���̰� �ִ°�� ó��)
            }
        }

    }

    public void SetBoardState(BoadState value)
    {
        mBoardState= value;
    }



}
