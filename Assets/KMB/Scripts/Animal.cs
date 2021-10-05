using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Animal : MonoBehaviour /*, IPointerEnterHandler, IPointerExitHandler*/
{
    float currentTime;
    float creatTime;

    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image image5;
    public Image image6;

    bool isActive = false;

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
        currentTime += Time.deltaTime;

        if (other.tag == "Player")
        {      
            StartCoroutine(Image());
        }

        IEnumerator Image()
        { 
            print("Active");
            isActive = true;
            image1.gameObject.SetActive(true);

            yield return new WaitForSeconds(3f);

            isActive = false;
            image1.gameObject.SetActive(false);
        }
    }
}

