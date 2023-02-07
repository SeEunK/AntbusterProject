using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCannon : MonoBehaviour
{
    UIManager uIManager;

    public Renderer btnRenderer;

    void Start()
    {
        btnRenderer = GetComponent<Renderer>();
    }

    public void OnMouseEnter()
    {
        uIManager.SetCannonInfoPopupActive(true);
    }
    public void OnMouseExit()
    {
        uIManager.SetCannonInfoPopupActive(false);
    }
}
