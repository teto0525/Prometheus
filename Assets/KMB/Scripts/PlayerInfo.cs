using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInfo : MonoBehaviour
{
    // ����
    public GameObject[] Effect;
    public Image[] Img;          // HP,MP�̹���.

    public float MAX_HP;         // HP �ִ�ġ.
    public float MAX_MP;         // MP �ִ�ġ.
    public float p_HP;           // ���� HP.
    public float p_MP;           // ���� MP.

    // �÷��̾� UI���� ����.
    // ���� : (HP��? MP��?, ��������? ü��ȸ���̳�?, ������ Ȥ�� ü��ȸ���� ��ġ�� �󸶸�ŭ�̳�?)
    public void UIUpdate(string _Type, string _InfoType, float _Value)
    {
        float Type = 0;   // HP�Ǵ� MP�� ���� ��ġ.
        float MAXType = 0;   // HP�Ǵ� MP�� �ִ�ġ.
        int Index = 0;   // HP�Ǵ� MP�� �̹��� �ε���.

        switch (_InfoType)
        {
            case "HP":
                {
                    Index = 0;
                    Type = p_HP;
                    MAXType = MAX_HP;

                    // ��Ŀ������ ȸ��
                    if (_Type == "Recover")
                        p_HP += _Value;
                    // ��Ŀ������ �ƴϸ� ������.
                    else
                        p_HP -= _Value;

                    break;
                }
            case "MP":
                {
                    Index = 1;
                    Type = p_MP;
                    MAXType = MAX_MP;

                    if (_Type == "Recover")
                        p_MP += _Value;
                    else
                        p_MP -= _Value;

                    break;
                }
        }

        // �ε��� ��°�� �̹����� ���Ž�Ų��.
        // ����, ��ȭ���Ѿ� �Ǵ� �̹����� HP�̰�, ü���� �ִ�ġ�� 100�̶�� ����.
        // 20�� �������� �޾� ü���� 80���Ҵٰ� ����, �׷��� ü���� 80�� �������� ������� �Ѵ�.
        // fillAmount�� ���� 0 ~ 1�� ���̱� ������ ü���� �Ҽ������� ��ȯ����� ǥ���� ����������.
        // 80 / 100 = 0.8 ������ �ۼ�Ʈ�� �ٲ㼭 ǥ���Ѵ� �����ϸ� �ȴ�.
        Img[Index].fillAmount = Type / MAXType;
    }
}
