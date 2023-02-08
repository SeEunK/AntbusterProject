using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawner : MonoBehaviour
{
    public List<GameObject> mAntsPool = new List<GameObject>();

    public GameObject mAntPrefab = null;       
    public GameObject PopAnt()
    {
        if(mAntsPool.Count > 0)
        {
            GameObject temp = mAntsPool[0];
            mAntsPool.Remove(temp);
            temp.SetActive(true);
            return temp;
        }
        else
        {
            GameObject newAnt = CreateAnt();
            newAnt.SetActive(true);
            return newAnt;
        }
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

        return antObject;

    }
}
