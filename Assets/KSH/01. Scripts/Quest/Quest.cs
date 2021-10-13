using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    // 플레이 중 수행해야 할 미션들

    public string questName; // 퀘스트 이름
    public int[] animalId; // 동물 6가지
    public List<string> talks = new List<string>();
    public Sprite image;

    public Quest(string name, int[] animal)
    {
        questName = name; // 퀘스트 이름
        animalId = animal; // 해당 

    }

    // 
    public void AddTalk(string talk)
    {
        talks.Add(talk);
    }

    public void SetImage(Sprite st)
    {
        image = st;
    }
}
