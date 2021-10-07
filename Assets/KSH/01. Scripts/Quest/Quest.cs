using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    // �÷��� �� �����ؾ� �� �̼ǵ�

    public string questName; // ����Ʈ �̸�
    public int[] animalId; // ���� 6����
    public List<string> talks = new List<string>();
    public Sprite image;

    public Quest(string name, int[] animal)
    {
        questName = name;
        animalId = animal;

    }

    public void AddTalk(string talk)
    {
        talks.Add(talk);
    }

    public void SetImage(Sprite st)
    {
        image = st;
    }
}
