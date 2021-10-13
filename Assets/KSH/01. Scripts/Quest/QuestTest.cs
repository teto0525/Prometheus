using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTest : MonoBehaviour
{
    int questId;
    int talkOrder = 0;

    string title;

    string talk;

    public Image im;



    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = title + "\n" + talk;

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            talkOrder++;
            talk = QuestManager.qm.GetTalkId(questId, talkOrder);
            if(talk.Length == 0)
            {

                QuestManager.qm.CompleteQuest();
                gameObject.SetActive(false);
                

            }
        }
    }

    public void SetQuestId(int qId)
    {
        questId = qId;
        talkOrder = 0;

        //Ÿ��Ʋ
        title = QuestManager.qm.CheckQuest(questId);

        //��ȭ����
        talk = QuestManager.qm.GetTalkId(questId, talkOrder);

        //����Ʈ�̹���
        im.sprite = QuestManager.qm.GetQuestImage(questId);

    }

    
}
