
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SampleManager : MonoBehaviour
{
    // ½Ì±ÛÅæ ¼±¾ð
    public static SampleManager sm = null;

  

    public void Awake()
    {
        if (sm == null) sm = this;
    }

    // ÆË¾÷ ÀÌ¹ÌÁö ¹è¿­
    public Image[] popUps;
    private int index = 0;

    // Bool ¼±¾ð
    public bool setPopup = false;

    // Start is called before the first frame update;
    void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        setPopup = true;
       index = 0;
       popUps[index].gameObject.SetActive(true);
    }

    public void PopUps()
    {
        int newIndex = index + 1;
        newIndex %= popUps.Length;

        popUps[newIndex].gameObject.SetActive(true);
        setPopup = true;

        index = newIndex;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCompleteDialog()
    {
        StartCoroutine(delay());
    }


    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.3f);
        popUps[index].gameObject.SetActive(false);

    }
}
