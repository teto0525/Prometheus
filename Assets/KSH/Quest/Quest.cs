using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    // �÷��� �� �����ؾ� �� �̼ǵ�

    public string questName; // ����Ʈ �̸�
    public int[] animalId; // ���� 6����

    public Quest(string name, int[] animal)
    {
        questName = name;
        animalId = animal;
    }
}
