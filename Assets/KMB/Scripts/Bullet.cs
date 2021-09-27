using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //�ӵ�
    public float speed = 5;

    //���� ȿ�� ���� 
    public GameObject exploFactory;

    Vector3 dir; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�ڽ��� �չ������� �̵��ϰ�ʹ�. 
        //p = p0+vt
        transform.position += transform.forward * speed * Time.deltaTime; 
    }

    private void OnTriggerEnter(Collider other)
    {
        //1. ����ȿ�� ���忡�� ���� ȿ���� �����. 
        GameObject explo = Instantiate(exploFactory);
        //2. ���� ȿ���� ��(bullet)�� ��ġ�� ���´�. 
        explo.transform.position = transform.position;
        //3. 3�� �ڿ� ���� ȿ���� ���ش�. 
        Destroy(explo, 3);
        //4. ���� ���ش�. 
        Destroy(gameObject);
    }
}
