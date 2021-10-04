using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Animal : MonoBehaviour /*, IPointerEnterHandler, IPointerExitHandler*/
{
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image image5;
    public Image image6;

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //   if(Input.GetButtonDown("Fire2"))
    //    {
    //        UIManager.MyInstance.ShowTooltip();
    //    }
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    UIManager.MyInstance.HideTooltip();
    //}


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Capsule")
        {
            image1.gameObject.SetActive(true);
        }
        else
        {
            image1.gameObject.SetActive(false);
            
        }
    }
}
