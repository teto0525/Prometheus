using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; // ���� �������� ����Ʈ ����
    public GameObject[] gameObjects; // ����Ʈ�� �ʿ��� ������Ʈ

    Dictionary<int, Quest> questList;

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
            case 10:
            if (questActionIndex == 2)
                questObject[0].SetActive(true);
            break;
        case 20:
            if (questActionIndex == 1) // ����Ʈ ������ 1�̸�
                questObject[0].SetActive(false);
        }
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if(id == questList[questId].animalId.Length)
        {
            NextQuest();
            Debug.Log(questId);
        }

        return questList[questId].questName;
    }

    public string CheckQuest() // �÷��̾ �˸��� ���� ȹ���ϸ� questActionIndex�� ���������ش�
    {
        return questList[questId].questName;
    }

    void GenerataData()
    {
        questList.Add(10, new Quest("1������ �ɻ罿 ������ ȹ���Ͻÿ�", new int[] { 1000, 2000 }));
        questList.Add(20, new Quest("1������ �ݴް����� ������ ȹ���Ͻÿ�", new int[] { 5000, 2000 }));
        questList.Add(30, new Quest("���ֿ��� Ż���Ͻʽÿ�", new int[] { 0 }));
    }
}
