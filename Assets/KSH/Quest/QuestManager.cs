using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; // 현재 진행중인 퀘스트 저장
    public GameObject[] gameObjects; // 퀘스트에 필요한 오브젝트

    Dictionary<int, Quest> questList;

    void Start()
    {
        questList = new Dictionary<int, Quest>();
        GenerataData();
    }

    // 현 퀘스트 성공시 다음 퀘스트 진행
    void NextQuest()
    {
        questId += 10; // 다음 퀘스트로 넘어갈 수 있게 값을 더해준다
        questActionIndex = 0; // 현재 진행 퀘스트 초기화
    }

    void Controlobject() // 퀘스트 오브젝트에 있는 오브젝트 조절
    {
        switch (questId)
        { 
            case 10:
                if (questActionIndex == 2)
                    gameObjects[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 1) // 퀘스트 진행이 1이면
                    gameObjects[0].SetActive(false);
                break;
        }
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if (id == questList[questId].animalId.Length)
        {
            NextQuest();
            Debug.Log(questId);
        }

        return questList[questId].questName;
    }

    public string CheckQuest() // 플레이어가 알맞은 샘플 획득하면 questActionIndex를 증가시켜준다
    {
        return questList[questId].questName;
    }

    void GenerataData()
    {
        questList.Add(10, new Quest("1층에서 꽃사슴 샘플을 획득하시오", new int[] { 1000, 2000 }));
        questList.Add(20, new Quest("1층에서 반달가슴곰 샘플을 획득하시오", new int[] { 5000, 2000 }));
        questList.Add(30, new Quest("방주에서 탈출하십시오", new int[] { 0 }));
    }
}
