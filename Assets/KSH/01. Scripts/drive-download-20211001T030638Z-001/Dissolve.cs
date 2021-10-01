using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    float dissolvePower = 0;
    public SkinnedMeshRenderer mr;
    // Start is called before the first frame update
    bool showDissolve;
    void Start()
    {
    }

    

    // Update is called once per frame
    void Update()
    {
        if (showDissolve == false) return;
        mr.material.SetFloat("_Dp", dissolvePower);

        dissolvePower += Time.deltaTime * 0.1f;
        if (dissolvePower > 1)
        {
            Destroy(gameObject);
        }
        
    }
    public void Show()
    {
        showDissolve = true;
    }
}
