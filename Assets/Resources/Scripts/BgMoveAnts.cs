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
        if (transform.position.y >= mConditionPos.y) //y 좌표가 조건 보다 크거나 같으면 (카메라 영역 벗어난만큼 위로 간 경우)
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
