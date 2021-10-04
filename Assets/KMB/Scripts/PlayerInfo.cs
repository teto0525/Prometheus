using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInfo : MonoBehaviour
{
    // 공개
    public GameObject[] Effect;
    public Image[] Img;          // HP,MP이미지.

    public float MAX_HP;         // HP 최대치.
    public float MAX_MP;         // MP 최대치.
    public float p_HP;           // 현재 HP.
    public float p_MP;           // 현재 MP.

    // 플레이어 UI정보 갱신.
    // 인자 : (HP냐? MP냐?, 데미지냐? 체력회복이냐?, 데미지 혹은 체력회복의 수치는 얼마만큼이냐?)
    public void UIUpdate(string _Type, string _InfoType, float _Value)
    {
        float Type = 0;   // HP또는 MP의 현재 수치.
        float MAXType = 0;   // HP또는 MP의 최대치.
        int Index = 0;   // HP또는 MP의 이미지 인덱스.

        switch (_InfoType)
        {
            case "HP":
                {
                    Index = 0;
                    Type = p_HP;
                    MAXType = MAX_HP;

                    // 리커버리면 회복
                    if (_Type == "Recover")
                        p_HP += _Value;
                    // 리커버리가 아니면 데미지.
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

        // 인덱스 번째의 이미지를 갱신시킨다.
        // 만약, 변화시켜야 되는 이미지가 HP이고, 체력의 최대치가 100이라고 하자.
        // 20의 데미지를 받아 체력이 80남았다고 하자, 그러면 체력이 80이 남은것을 보여줘야 한다.
        // fillAmount의 값은 0 ~ 1가 끝이기 때문에 체력을 소수점으로 변환해줘야 표현이 가능해진다.
        // 80 / 100 = 0.8 요컨데 퍼센트로 바꿔서 표현한다 생각하면 된다.
        Img[Index].fillAmount = Type / MAXType;
    }
}
