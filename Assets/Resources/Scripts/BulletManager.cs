using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;

    public List<GameObject> mBulletsPool = new List<GameObject>();
    public List<GameObject> mBulletPrefabList = new List<GameObject>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }




    public GameObject PopBullet(int bullectIndex)
    {
        if (bullectIndex > mBulletPrefabList.Count)
        {
            Debug.Log("bullet index error");
            return null;
        }
        else
        {
            if (mBulletsPool.Count > 0)
            {
                for (int i = 0; i < mBulletsPool.Count; i++)
                {
                    if (mBulletsPool[i].GetComponent<Bullet>().GetIndex() == bullectIndex)
                    {
                        GameObject temp = mBulletsPool[i];
                        mBulletsPool.Remove(temp);
                        Bullet bullet = temp.GetComponent<Bullet>();
                        temp.SetActive(true);
                        return temp;
                    }
                }
            }

            GameObject newBullet = CreateBullet(bullectIndex);
            newBullet.SetActive(true);
            return newBullet;

        }
    }


    public void PushBullet(GameObject bullet)
    {
        Debug.Log("bullet Push");

        
        bullet.SetActive(false);
        mBulletsPool.Add(bullet);

    }

    public GameObject CreateBullet(int bullectIndex)
    {

        GameObject bulletObject = GameObject.Instantiate<GameObject>(mBulletPrefabList[bullectIndex]);

        bulletObject.SetActive(false);


        return bulletObject;
    }

}
