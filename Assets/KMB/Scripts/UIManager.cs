using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tooltip1;
    [SerializeField]
    private GameObject tooltip2;
    [SerializeField]
    private GameObject tooltip3;
    [SerializeField]
    private GameObject tooltip4;
    [SerializeField]
    private GameObject tooltip5;
    [SerializeField]
    private GameObject tooltip6;



    private static UIManager instance;

    public static UIManager MyInstance;
    

    //ÅøÆÁ UI È°¼ºÈ­
    public void ShowTooltip()
    {
        tooltip1.SetActive(true);
        tooltip2.SetActive(true);
        tooltip3.SetActive(true);
        tooltip4.SetActive(true);
        tooltip5.SetActive(true);
        tooltip6.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltip1.SetActive(false);
        tooltip2.SetActive(false);
        tooltip3.SetActive(false);
        tooltip4.SetActive(false);
        tooltip5.SetActive(false);
        tooltip6.SetActive(false);

    }
}
