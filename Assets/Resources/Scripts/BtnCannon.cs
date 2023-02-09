using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCannon : MonoBehaviour
{

    public Renderer btnRenderer;

    void Start()
    {
        btnRenderer = GetComponent<Renderer>();
    }

    public void OnMouseEnter()
    {
        UIManager.instance.SetCannonInfoPopupActive(true);
    }
    public void OnMouseExit()
    {
        UIManager.instance.SetCannonInfoPopupActive(false);
    }
}
