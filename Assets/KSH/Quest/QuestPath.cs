using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPath : MonoBehaviour
{
    public QuestEvent startEvent;
    public QuestEvent endEvent;

    public QuestPath(QuestEvent from, QuestEvent to)
    {
        startEvent = from;
        endEvent = to;
    }
}
