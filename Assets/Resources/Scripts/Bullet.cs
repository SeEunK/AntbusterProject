using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   public Vector3 mTargetPos = Vector3.zero;

    public int mIndex = -1;
    public float mSpeed = 0.0f;

    public int mDamage = 0;
    public float mAttackRange = 0.0f;
   
    public Vector3 mStartPos = Vector3.zero;
  
    void Update()
    {
        this.transform.Translate(Vector3.up * mSpeed);
      
        Vector3 distance = this.transform.position - mStartPos;
        // 공격 범위 넘어감.
        if(distance.magnitude >= mAttackRange)
        {
            BulletManager.instance.PushBullet(this.gameObject);
        }

    }
    public void Init(int index, int damage, float speed, Vector3 startPos, float range)
    {
        SetIndex(index);
        SetDamage(damage);
        SetSpeed(speed);
        SetStartPos(startPos);
        SetAttackRange(range);

        this.gameObject.SetActive(false);
    }
    public void SetIndex(int index)
    {
        mIndex = index;
    }



    public int GetIndex()
    {
        return mIndex;
    }
    public void SetAttackRange(float range)
    {
        mAttackRange = range;
    }
    public void SetStartPos(Vector3 startPos)
    {
        mStartPos = startPos;
        this.transform.position = mStartPos;
    }

    public void SetDamage(int damage)
    {
        mDamage = damage;
    }
    public void SetSpeed(float speed)
    {
        mSpeed = speed;
    }
    public void UpdateTarget(Transform TargetPos)
    {
        mTargetPos = TargetPos.position;

        // 타겟포지션 까지의 거리
        Vector3 targetDistance = mTargetPos - this.transform.position;
        
        Vector3 quaternionDistance = Quaternion.Euler(0, 0, 0) * targetDistance;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, quaternionDistance);

        this.transform.rotation = lookRotation;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ant")
        {
            Debug.Log("ant");
            Ant target = other.gameObject.GetComponent<Ant>();
            if (target != null)
            {
                target.OnDamage(mDamage);
                

                BulletManager.instance.PushBullet(this.gameObject);
                
            }

        }
    }

 
}
