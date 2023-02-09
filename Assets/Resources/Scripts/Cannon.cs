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
                    // ã�� ant�� Ÿ������ ����.
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

            //bullect manager ���� bulletindex �� �´� bullet pop!
            GameObject bulletObject = BulletManager.instance.PopBullet(mBulletIndex);

            // �Ѿ� ������ ĳ���� �ϰ�, Active �ϰ� ���!
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
        // ����  (�Ѿ� �������� �̰ɷ� ó��)
        SetCannonLevel(mCannonLevel+ 1);
        // ����
        if (mAttackSpeed >= 0.3f)
        {
            SetAttackSpeed(mAttackSpeed - 0.02f);
        }
        // ����
        SetAttackRange(mAttackRange * 2);
        // �Ѿ� ����
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
        //Ÿ�� ���� �Ÿ�
        Vector3 distance = target.transform.position - mCenter;

        // Ÿ���� ���ݹ����ȿ� ����
        if (distance.magnitude <= mAttackRange)
        {
            // Ÿ�� �ٶ󺸰� head rotation
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
        // ���� ���� sprite ������ ����.
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
