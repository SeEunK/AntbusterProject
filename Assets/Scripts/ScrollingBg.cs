using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBg : MonoBehaviour
{
    public List<GameObject>mMoveAntsBgList = new List<GameObject>();

    

    private float mBgMoveSpeed = 1.5f;



    private void Update()
    {
        if(TitleScene.instance.GetTitleSceneState() == TitleScene.TitleSceneState.In)
        {
            for(int i = 0; i < mMoveAntsBgList.Count; i++)
            {
                Vector3 move = new Vector3(-1, 1, 0).normalized * mBgMoveSpeed * Time.deltaTime;
                mMoveAntsBgList[i].transform.position += move;
               
            }
            
        }
    }


}
