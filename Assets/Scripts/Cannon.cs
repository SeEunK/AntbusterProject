using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cannon : MonoBehaviour
{
    public enum CannonState { None, Attack}

    public CannonState mState = CannonState.None;

    public GameObject mCannonHead = null;
    
    public int mCannonLevel = 0;
    public GameObject mAttackRangeArea = null;
    public float mAttackRange = 2.0f;

    public float mTime = 0.0f;
    public GameObject mBulletPrefabs = null;
    public float mAttackSpeed = 3.0f;
    public GameObject mTaget = null;

    public Vector3 mCenter = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        mCenter = gameObject.transform.GetChild(3).transform.position;
        SetAttackRange(mAttackRange);
       mCannonHead = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
  
        if (mState == CannonState.None)
        {
            GameObject findAnt = GameObject.FindWithTag("Ant");

            if (findAnt != null)
            {
                Debug.Log("find tag ant ");

                if (IsInRange(findAnt) == true)
                {
                    Debug.Log("in Range ");
                    // 찾은 ant를 타겟으로 설정.
                    mTaget = findAnt;

                    ChangeColor(mTaget);

                    mState = CannonState.Attack;
                }

            }
            else
            {
                Debug.Log("Can not find tag ant");
            }
        }

        if (mState == CannonState.Attack)
        {
            mTime += Time.deltaTime;
            if(mTime >= mAttackSpeed)
            {
                mTime = 0.0f;
                
                ShotTarget();
                mState = CannonState.None;
           
            }
        }
    }
    public void ShotTarget()
    {
        if (mTaget != null)
        {
            GameObject bullet = GameObject.Instantiate<GameObject>(mBulletPrefabs);

            // 총알 시작 포지션을 쏘는 Cannon 위치로 설정
            bullet.transform.position = gameObject.transform.GetChild(3).transform.position;
            bullet.GetComponent<Bullet>().UpdateTarget(mTaget.transform);
            bullet.SetActive(true);
        }
    }
    public bool IsInRange(GameObject target)
    {
        //타겟 까지 거리
        Vector3 distance = target.transform.position - mCenter;

        // 타겟이 공격범위안에 있음
        if (distance.magnitude <= mAttackRange)
        {
            // 타겟 바라보게 head rotation
            LookTarget(distance);
            return true;
        }
        else
        {
            return false;
        }
       
    }
    public void SetAttackRange(float range)
    {
        // 범위 영역 sprite 사이즈 조정.
        mAttackRangeArea.transform.localScale = new Vector3(range* 2.0f, range* 2.0f, 0.0f);
       
        mAttackRange = range;
    }

    public void ChangeColor(GameObject targetOb)
    {
        Debug.Log("ChangeColor");
        targetOb.GetComponent<SpriteRenderer>().color = Color.red;
       

    }

    public void LookTarget(Vector3 distance)
    {
       
        Vector3 quaternionDistance = Quaternion.Euler(0, 0, 0) * distance;

        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, quaternionDistance);

        mCannonHead.GetComponent<Transform>().rotation = lookRotation;
     


    }

}
