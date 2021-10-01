using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    float dissolvePower = 0;
    public SkinnedMeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
    }

    

    // Update is called once per frame
    void Update()
    {
        dissolvePower += Time.deltaTime * 0.3f;
        if (dissolvePower > 1) dissolvePower = 0;
        mr.material.SetFloat("_Dp", dissolvePower);
        
    }
}
