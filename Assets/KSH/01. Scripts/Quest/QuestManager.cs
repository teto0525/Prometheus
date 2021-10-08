using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager qm;
    public List<int> completeQuest = new List<int>();

    public int questId;
    public int questActionIndex; // 대화 순서
    public GameObject[] gameObjects; // 퀘스트에 필요한 오브젝트

    Dictionary<int, Quest> questList;

    public Sprite[] questImage;

    private void Awake() // 싱글톤
    {
        if (qm == null) qm = this;
    }

    void Start()
    {
        questList = new Dictionary<int, Quest>();
        GenerataData();
    }

    /* 다음 퀘스트 진행 */
    // 현 퀘스트 성공시 다음 퀘스트 진행
    void NextQuest()
    {
        questId += 10; // 다음 퀘스트로 넘어갈 수 있게 값을 더해준다
        questActionIndex = 0; // 현재 진행 퀘스트 초기화
    }

    void Controlobject() // 퀘스트 오브젝트에 있는 오브젝트 조절
    {
        switch (questId) // 퀘스트 아이디
        { 
            case 10: // 퀘스트 10일때
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


    public string CheckQuest(int q) // 플레이어가 알맞은 샘플 획득하면 questActionIndex를 증가시켜준다
    {
        return questList[q].questName; // 퀘스트 리스트에서 퀘스트 이름을 리턴해준다
    }

    public string GetTalkId(int q, int talkOrder)
    {
        string order = "";

        if(talkOrder < questList[q].talks.Count)
        {
            order = questList[q].talks[talkOrder];
        }

        return order;
    }

    public Sprite GetQuestImage(int q)
    {
        return questList[q].image;
    }

    public void CompleteQuest()
    {
        completeQuest.Add(questId);
        questId += 10;
    }


    void GenerataData() // 대화문 내용
    {
        Quest quest = new Quest("1층에서 꽃사슴 샘플을 획득하시오", new int[] { 1000, 2000 });
        quest.AddTalk("첫번째 대화입니다 다음을 누르세요");
        quest.AddTalk("두번째 대화입니다 다음을 누르세요");
        quest.AddTalk("세번째 대화입니다 다음을 누르세요");
        quest.SetImage(questImage[questList.Count]);
        questList.Add(10, quest);

        quest = new Quest("1층에서 반달가슴곰 샘플을 획득하시오", new int[] { 5000, 2000 });
        quest.AddTalk("1111111111111111111111111111");
        quest.AddTalk("22222222222222222222");
        quest.SetImage(questImage[questList.Count]);
        questList.Add(20, quest);

        quest = new Quest("방주에서 탈출하십시오", new int[] { 0 });
        quest.AddTalk("aaaaaaaaaaaaaaaaaaa");
        quest.AddTalk("bbbbbbbbbbbbbbbbbbb");
        quest.AddTalk("cccccccccccccccccccccc");
        quest.SetImage(questImage[questList.Count]);
        questList.Add(30, quest);
    }
}
