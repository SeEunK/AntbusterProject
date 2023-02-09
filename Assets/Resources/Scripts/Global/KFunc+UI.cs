using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static partial class KFunc
{
    public static void SetTMPText(this GameObject tmpObject, string text)
    {
        tmpObject.GetComponent<TMP_Text>().text = text;
    }
    public static void SetTMPText(this GameObject tmpObject, int intValue)
    {
        tmpObject.GetComponent<TMP_Text>().text = intValue.ToString();
    }
}
