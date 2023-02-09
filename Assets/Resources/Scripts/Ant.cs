using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BoardManager;
using UnityEngine.UI;
using Unity.VisualScripting;


public class Ant : MonoBehaviour
{

    public enum AntState { None, GoToCake, Die, GoToHome}
    public AntState mState = AntState.None;
    public Vector3 mStartPos = Vector3.zero;
    public Vector3 mTargetPos = Vector3.zero;
    public Vector3 mcakePos = Vector3.zero;
    
    public Transform mAntImage = null;
    public Animator  mAntAnimator = null;
    public Camera mCamera = null;

    public float mSpeed = 2.0f;
    public Cake mCake = null;
    public Image mImgHp = null;
    public int mHp = 0;
    public int mMaxHp = 0;

    public AntSpawner mSpawner = null;
    public int mDieCount = 0;
    // Start is called before the first frame update

    void Start()
    {
        mAntAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      
        if (mState == AntState.GoToCake || mState == AntState.GoToHome)
        {
            UpdateTarget();

            this.transform.position = Vector3.MoveTowards(this.transform.position, mTargetPos, Time.deltaTime * mSpeed);
           
        }
        if (mState == AntState.GoToHome)
        {
            if (this.transform.position == mStartPos)
            {
                if (IsCakeEnable() == true)
                {
                    GameManager.instance.mCakeCount += 1;
                }
                mSpawner.PushAnt(this.gameObject);
            }
        }
        
    }

    public void InitAnt(int maxHp, Cake cake)
    {
        mCake = cake;
        SetState(AntState.GoToCake);
        
        mcakePos = cake.transform.position;
        SetTargetPos(mcakePos);
        
        mStartPos = mSpawner.transform.position;
        this.transform.position = mStartPos;

        mMaxHp = maxHp;
        mHp = mMaxHp;
        mImgHp.fillAmount = (float)mHp / (float)mMaxHp;

        SetCakeEnable(false);

    }

    public void SetState(AntState value)
    {
        mState = value;
    }

    public void SetCakeEnable(bool enable)
    {
        this.transform.GetChild(0).gameObject.SetActive(enable);
    }
    public bool IsCakeEnable()
    {
        return this.transform.GetChild(0).gameObject.activeSelf;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("triggerEnter!!!! ");
        if (other.gameObject.CompareTag("Cake"))
        {
            Debug.Log("triggerEnter!!!! --> Cake");

            if (other.gameObject.GetComponent<Cake>().GetCakeCount() > 0)
            {
                Debug.Log("ant get cake !!!!! ");
                other.gameObject.GetComponent<Cake>().DisCakeCount(); // 케익 하나 깍고
                SetCakeEnable(true);

            }
            else
            {
                Debug.Log("cake empty!!!!! ");
            }
            SetTargetPos(mStartPos); // 집 포지션 타겟 포스 변경.
            SetState(AntState.GoToHome); //집으로 !


            UpdateTarget();
        }

   
    }

    public void OnDamage(int damage)
    {
        mHp -= damage;

        mImgHp.fillAmount =(float)mHp/(float)mMaxHp;
        if(mHp <= 0)
        {
            SetState(AntState.Die);
            Die();
        }
    }

 


    public void Die()
    {
        if (IsCakeEnable() == true)
        {
            mCake.AddCakeCount(1);
        }
        
        mAntAnimator.SetTrigger("isDead");
        mSpawner.PushAnt(this.gameObject);
        mDieCount += 1;

    }

    public void Reposition()
    {
        transform.position = mStartPos;

    }


    public void SetTargetPos(Vector3 targetPos)
    {
        mTargetPos = targetPos;
    }

    public void UpdateTarget()
    {
      
        // 타겟포지션 까지의 거리
        Vector3 targetDistance = mTargetPos - this.transform.position;

        Vector3 quaternionDistance = Quaternion.Euler(0, 0, 0) * targetDistance;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, quaternionDistance);


        mAntImage.transform.rotation = lookRotation;

    }


    
}
