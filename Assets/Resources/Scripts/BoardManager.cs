using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;
    public enum BoadState { None, Deploy }

    //보드 한칸
    public const float UNIT = 1.0f;
    // 타일 가로
    public const int WIDTH = 25;
    // 타일 가로
    public const int LENGTH = 20;

    // 보드 칸 
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

    // 리트라이 -> 로드 씬 할때 제대로 초기화 되지않거나 missing 되는경우 있음... 
    void OnSceneLoaded()
    {
        mSellectPos = GameObject.Find("/Prefabs/SellectPos").gameObject.transform;
        mCamera = Camera.main;
    }

    private void Update()
    {
        
        if (mBoardState == BoadState.Deploy)
        {
            UpdateDeployMode();
        }
    }

    public void UpdateDeployMode()
    {
        mSellectPos.gameObject.SetActive(true);

        // 마우스 좌표를 월드좌표로 변환
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 마우스월드 좌표를 보드안의 인덱스로 변환
        int posX = Mathf.Clamp((int)(mousePos.x / UNIT), 0, WIDTH - 1);
        int posY = Mathf.Clamp((int)(mousePos.y / UNIT), 0, LENGTH - 1);

        int index = posY * WIDTH + posX;

        mousePos.z = 0.0f;
        mousePos.x = posX;
        mousePos.y = posY;

        mSellectPos.position = mousePos;

        // 마우스 커서 위치 보드칸이 배치 가능한 경우 
        if (Board[index] == 0)
        {
            mSellectPos.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            mSellectPos.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }

        // 마우스클릭
        if (Input.GetMouseButtonDown(0))
        {
            // 0(배치가능한 곳) 인 경우 
            if (Board[index] == 0)
            {
                Board[index] = 2; // 배치함으로변경

                //
                GameObject deployCannon = GameObject.Instantiate<GameObject>(mCannonPrefab);
                //GameObject deployCannon = GameObject.Instantiate<GameObject>(mSellectPos.gameObject);
                deployCannon.transform.position = mousePos;

                SetBoardState(BoadState.None);
            }
            else 
            {
                // 클릭한곳이 0이아닌경우(배치불가지역 or 이미 뭔가배치된경우 or 개미가 있는경우 처리)
            }
        }

    }

    public void SetBoardState(BoadState value)
    {
        mBoardState= value;
    }



}
