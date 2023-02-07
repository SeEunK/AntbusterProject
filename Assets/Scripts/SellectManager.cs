using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellectManager : MonoBehaviour
{
    public GameObject prevClickObject = null;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("click mouse");
            CheckClickObject();


        }
    }

    public void CheckClickObject()
    {
        Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hitInfo = Physics2D.Raycast(clickPos, Camera.main.transform.forward);

        if (hitInfo.collider != null)
        {

            GameObject clickObject = hitInfo.transform.gameObject;
            Debug.Log("click object" + clickObject.name);

            if (clickObject.tag == "Cannon")
            {
                if (prevClickObject != null)
                {
                    prevClickObject.transform.GetChild(1).gameObject.SetActive(false);

                }

                prevClickObject = clickObject;
                clickObject.transform.GetChild(1).gameObject.SetActive(true);

            }
        }
    }

}
