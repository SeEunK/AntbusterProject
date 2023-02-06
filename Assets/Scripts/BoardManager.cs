using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public enum BoadState { None, Deploy }

    //���� ��ĭ
    public const float UNIT = 1.0f;
    // Ÿ�� ����
    public const int WIDTH = 25;
    // Ÿ�� ����
    public const int LENGTH = 20;

    // ���� ĭ 
    public static int[] Board = new int[] {
        1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1
        };

    public Camera mCamera = null;
    public Transform mSellectPos = null;
    public BoadState mBoardState = BoadState.None;


    private void Update()
    {
        if(mBoardState == BoadState.Deploy)
        {
            UpdateDeployMode();
        }
    }

    public void UpdateDeployMode()
    {
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

        // ���콺Ŭ��
        if (Input.GetMouseButtonDown(0))
        {
            // 0(��ġ������ ��) �� ��� 
            if (Board[index] == 0)
            {
                Board[index] = 2; // ��ġ�����κ���

                GameObject clone = GameObject.Instantiate<GameObject>(mSellectPos.gameObject);

                clone.transform.position = mousePos;
            }
            else 
            {
                // Ŭ���Ѱ��� 0�̾ƴѰ��(��ġ�Ұ����� or �̹� ������ġ�Ȱ�� or ���̰� �ִ°�� ó��)
            }
        }


    }



}
