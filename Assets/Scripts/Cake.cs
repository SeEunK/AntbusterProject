using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cake : MonoBehaviour
{
    public const int CAKE_MAX_COUNT = 8;
    public List<Sprite> mCakeImageList = new List<Sprite>();
    public SpriteRenderer mCakeImage = null;
    public int mCakeCount = 0;
    
    void Start()
    {
        mCakeImage = gameObject.GetComponent<SpriteRenderer>();
        mCakeCount = CAKE_MAX_COUNT; 
        SetCakeImage(mCakeCount);


    }

    public void SetCakeImage(int cakeCount)
    {
        if (cakeCount == 0)
        {
            mCakeImage.sprite = null;
        }
        else
        {
            mCakeImage.sprite = mCakeImageList[cakeCount - 1];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }

    public void DisCakeCount()
    {
        mCakeCount -= 1;
        SetCakeImage(mCakeCount);
    }
    public void AddCakeCount(int count)
    {
        mCakeCount += count;
        SetCakeImage(mCakeCount);
    }

    public int GetCakeCount()
    {
        return mCakeCount;
    }

}
