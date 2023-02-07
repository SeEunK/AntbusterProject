using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cannon : MonoBehaviour
{
    public enum CannonState { None, Attack}

    public CannonState mState = CannonState.None;

    
    public int mCannonLevel = 0;
    public GameObject mAttackRangeArea = null;
    public float mAttackRange = 1.0f;

    public float mTime = 0.0f;
    public GameObject mBulletPrefabs = null;
    public float mAttackSpeed = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
       SetAttackRange(mAttackRange);
    }

    // Update is called once per frame
    void Update()
    {

        GameObject ant = GameObject.FindWithTag("Ant");
        if(ant != null)
        {
            Debug.Log("find tag ant");
        }
        else
        {
            Debug.Log("Can not find tag ant");
        }

        Vector3 distance = ant.transform.position - this.transform.position;

        if (mState == CannonState.None)
        {
            
            if (distance.magnitude <= mAttackRange)
            {
                mState = CannonState.Attack;
            }
        }

        if(mState == CannonState.Attack)
        {
            mTime += Time.deltaTime;
            if(mTime >= mAttackSpeed)
            {
                mTime = 0.0f;

               
                
                //공격 범위 내에 들어온경우
                if(distance.magnitude <= mAttackRange)
                {
                    GameObject bullect = GameObject.Instantiate<GameObject>(mBulletPrefabs);
                    
                    // 총알 시작 포지션을 쏘는 Cannon 위치로 설정
                    bullect.transform.position = this.transform.position;
                    bullect.GetComponent<Bullet>().UpdateTarget(ant.transform);
                    bullect.SetActive(true);

                }
            }
        }
    }

    public void SetAttackRange(float range)
    {
        mAttackRangeArea.transform.localScale = new Vector3(range* 2.0f, range* 2.0f, 0.0f);
       
        mAttackRange = range;
    }


    //public void OnMouseUp()
    //{
       
    //    mAttackRangeArea.SetActive(true);
    //}

    //public void SetAttackRangeAreaActive(bool value)
    //{
        
    //    mAttackRangeArea.SetActive(value);
    //}

}
