using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AntSpawner : MonoBehaviour
{
    public List<GameObject> mAntsPool = new List<GameObject>();

    public Cake mCake = null;

    public GameObject mAntPrefab = null;
    public float mTimer = 0;
    public float mCreateTurm = 1.0f;

    public void Start()
    {

        for (int i = 0; i < 6; i++)
        {
            GameObject newAnt = CreateAnt();
            newAnt.SetActive(false);
            mAntsPool.Add(newAnt);
        }
    }
    public void Update()
    {
        if (BoardManager.instance.mGameState == BoardManager.GameState.GameReady)
        {
            return;
        }
        if (BoardManager.instance.mCakeCount >= 8)
        { 
            return; 
        }

        if (mAntsPool.Count > 0)
        {
            mTimer += Time.deltaTime;

            if (mTimer >= mCreateTurm)
            {
                mTimer = 0;

                PopAnt();

            }
        }

    }


    public GameObject PopAnt()
    {

        if (mAntsPool.Count > 0)
        {
            GameObject temp = mAntsPool[0];
            mAntsPool.Remove(temp);
            Ant ant = temp.GetComponent<Ant>();
            ant.InitAnt(10 + ant.mDieCount, mCake);

            temp.SetActive(true);
            return temp;
        }
        return null;

    }


    public void PushAnt(GameObject ant)
    {
        Debug.Log("Push");


        ant.GetComponent<Ant>().Reposition();
        ant.SetActive(false);
        mAntsPool.Add(ant);

    }

    public GameObject CreateAnt()
    {
        Debug.Log("Create");

        GameObject antObject = Instantiate(mAntPrefab);
        antObject.SetActive(false);
        
        Ant ant = antObject.GetComponent<Ant>();
        ant.mSpawner = this;
        
        ant.InitAnt(10, mCake);

        return antObject;

    }
}
