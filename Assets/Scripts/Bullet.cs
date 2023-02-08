using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

        // Å¸°ÙÆ÷Áö¼Ç ±îÁöÀÇ °Å¸®
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
            this.gameObject.SetActive(false);
            Ant target = other.gameObject.GetComponent<Ant>();
            if (target != null)
            {
                target.OnDamage(100);
            }

            // °³¹Ì ÇÇ ±ï°í
            // ÃÑ¾Ë active ²ô°í
            // push

        }
    }

 
}
