using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private PlayerMove pMove; // �÷��̾��� ��ũ��Ʈ�� ����� ����.

    // ���������� �÷��̾��� HP ����.
    IEnumerator StartDamage()
    {
        while (true)
        {
            // �÷��̾ ������ �ִ� UIUpdateȣ��.
           // pMove.DamangeAction("Damage", "HP", 0.1);
            yield return null;
        }
    }

    // �浹 ���� ��.
    void OnTriggerEnter(Collider _Col)
    {
        if (_Col.transform.CompareTag("Player"))
        {
            // �÷��̾��� ��ũ��Ʈ ������Ʈ�� �����´�.
            pMove = _Col.GetComponent<PlayerMove>();
            StartCoroutine("StartDamage");
        }
    }

    // �浹�� ������ ��.
    void OnTriggerExit(Collider _Col)
    {
        if (_Col.transform.CompareTag("Player"))
        {
            // �ڷ�ƾ�� �����.
            StopCoroutine("StartDamage");

            // �浹�� ���� �� ������ �÷��̾��� ������ �Լ� ������ ���� �ʿ䰡
            // ���� ������ null�� �ʱ�ȭ ���ش�.
            pMove = null;
        }
    }
}
