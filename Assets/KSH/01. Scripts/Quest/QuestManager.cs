using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager qm;
    public List<int> completeQuest = new List<int>();

    public int questId;
    public int questActionIndex; // ���� �������� ����Ʈ ����
    public GameObject[] gameObjects; // ����Ʈ�� �ʿ��� ������Ʈ

    Dictionary<int, Quest> questList;

    public Sprite[] questImage;

    private void Awake()
    {
        if (qm == null) qm = this;
    }

    void Start()
    {
        questList = new Dictionary<int, Quest>();
        GenerataData();
    }

    // �� ����Ʈ ������ ���� ����Ʈ ����
    void NextQuest()
    {
        questId += 10; // ���� ����Ʈ�� �Ѿ �� �ְ� ���� �����ش�
        questActionIndex = 0; // ���� ���� ����Ʈ �ʱ�ȭ
    }

    void Controlobject() // ����Ʈ ������Ʈ�� �ִ� ������Ʈ ����
    {
        switch (questId)
        { 
            case 10:
                if (questActionIndex == 2)
                    gameObjects[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 1) // ����Ʈ ������ 1�̸�
                    gameObjects[0].SetActive(false);
                break;
        }
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    //public string CheckQuest(int id)
    //{
    //    if (id == questList[questId].animalId.Length)
    //    {
    //        NextQuest();
    //        Debug.Log(questId);
    //    }

    //    return questList[questId].questName;
    //}

    public string CheckQuest(int q) // �÷��̾ �˸��� ���� ȹ���ϸ� questActionIndex�� ���������ش�
    {
        return questList[q].questName;
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


    void GenerataData()
    {
        Quest quest = new Quest("1������ �ɻ罿 ������ ȹ���Ͻÿ�", new int[] { 1000, 2000 });
        quest.AddTalk("ù��° ��ȭ�Դϴ� ������ ��������");
        quest.AddTalk("�ι�° ��ȭ�Դϴ� ������ ��������");
        quest.AddTalk("����° ��ȭ�Դϴ� ������ ��������");
        quest.SetImage(questImage[questList.Count]);
        questList.Add(10, quest);

        quest = new Quest("1������ �ݴް����� ������ ȹ���Ͻÿ�", new int[] { 5000, 2000 });
        quest.AddTalk("1111111111111111111111111111");
        quest.AddTalk("22222222222222222222");
        quest.SetImage(questImage[questList.Count]);
        questList.Add(20, quest);

        quest = new Quest("���ֿ��� Ż���Ͻʽÿ�", new int[] { 0 });
        quest.AddTalk("aaaaaaaaaaaaaaaaaaa");
        quest.AddTalk("bbbbbbbbbbbbbbbbbbb");
        quest.AddTalk("cccccccccccccccccccccc");
        quest.SetImage(questImage[questList.Count]);
        questList.Add(30, quest);
    }
}
