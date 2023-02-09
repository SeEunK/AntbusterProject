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
    public float mAttackSpeed = 1.0f;
    public float mBulltetSpeed = 0.05f;
    public int mBulletIndex = -1;
    public int mHeadIndex = -1;
    public GameObject mTaget = null;

    public Vector3 mCenter = Vector3.zero;

    public List<Sprite> mCannonHeadImageList = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        mCenter = gameObject.transform.GetChild(3).transform.position;
        mCannonHead = transform.GetChild(0).gameObject;
        SetAttackRange(mAttackRange);
        SetAttackSpeed(mAttackSpeed);
        SetCannonLevel(1);
        SetCannonHeadImage(0);
     
        SetBulletIndex(mCannonLevel - 1);


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
    // pop
    public void ShotTarget()
    {
        if (mTaget != null)
        {
            //GameObject bulletObject = GameObject.Instantiate<GameObject>(mBulletPrefabs);

            //bullect manager 에서 bulletindex 에 맞는 bullet pop!
            GameObject bulletObject = BulletManager.instance.PopBullet(mBulletIndex);

            // 총알 세팅은 캐논이 하고, Active 하고 쏘기!
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.Init(mBulletIndex, mCannonLevel, mBulltetSpeed, mCenter , mAttackRange);
          
            bullet.UpdateTarget(mTaget.transform);
            bulletObject.SetActive(true);
        }
    }
    public void SetCannonLevel(int level)
    {
        mCannonLevel = level;
    }

    public void SetCannonHeadImage(int index)
    {
        mHeadIndex= index;
        mCannonHead.GetComponent<SpriteRenderer>().sprite = mCannonHeadImageList[index];
    }
    public void CannonUpgrade()
    {
        // 레벨  (총알 데미지도 이걸로 처리)
        SetCannonLevel(mCannonLevel+ 1);
        // 공속
        if (mAttackSpeed >= 0.3f)
        {
            SetAttackSpeed(mAttackSpeed - 0.02f);
        }
        // 범위
        SetAttackRange(mAttackRange * 2);
        // 총알 종류
        if (mBulletIndex < BulletManager.instance.mBulletPrefabList.Count)
        {
            SetBulletIndex(mBulletIndex + 1);
        }
        if (mHeadIndex < mCannonHeadImageList.Count)
        {
            SetCannonHeadImage(mHeadIndex+1);
        }

    }


    public void SetBulletIndex(int index)
    {
        mBulletIndex = index;
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

    public void SetAttackSpeed(float speed)
    {
        mAttackSpeed = speed;
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
