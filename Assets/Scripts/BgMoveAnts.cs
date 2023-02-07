using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMoveAnts : MonoBehaviour
{

    private float mHight;
    private float mWidth;
    
    private Vector3 mConditionPos;
    


    void Awake()
    {
        mConditionPos = new Vector3(-6, 6, 0);
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        //mWidth = boxCollider.size.x;
        //mHight = boxCollider.size.y;
        
        
    }

  
    void Update()
    {
        if (transform.position.y >= mConditionPos.y) //y ��ǥ�� ���� ���� ũ�ų� ������ (ī�޶� ���� �����ŭ ���� �� ���)
        {
            Debug.Log($" y position : {transform.position.y}");
            Reposition();
        }
      
    }

    private void Reposition()
    {

        Vector3 offset = new Vector3(mConditionPos.x * -3.0f, mConditionPos.y * - 3.0f, 0);
        transform.position = (Vector3)transform.position + offset;
        Debug.Log($" reposition y pos : {transform.position.y}");
      

    }
}
