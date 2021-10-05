using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public Quest quest = new Quest();

    // Start is called before the first frame update
    void Start()
    {
        // create each event
        QuestEvent a = quest.AddQuestEvent("test1", "description 1");
        QuestEvent b = quest.AddQuestEvent("test1", "description 2");
        QuestEvent c = quest.AddQuestEvent("test1", "description 3");
        QuestEvent d = quest.AddQuestEvent("test1", "description 4");
        QuestEvent e = quest.AddQuestEvent("test1", "description 5");

        // define the path between the events - e.g. the order they must be completed
        quest.AddPath(a.GetId(), b.GetId());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
