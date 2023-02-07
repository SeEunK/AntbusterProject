using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   public Vector3 mTargetPos = Vector3.zero;

   
    void Update()
    {
        this.transform.Translate(Vector3.up * 0.01f);
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
}
