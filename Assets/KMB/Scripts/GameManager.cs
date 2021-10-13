using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /* KSH �߰����� : ��ȭâ */
    public Text talkText;
    public GameObject TalkImage;

    public GameObject scanObject;
    public Image portraitImage;

    public bool isMove;
    public int talkIndex; //�� ��° ��ȭ?

    public void ShowText(GameObject scanObj)
    {
        Capsule c = scanObj.GetComponent<Capsule>();
        if(c.questId == QuestManager.qm.questId)
        {

            TalkImage.SetActive(true);

            QuestTest qt = TalkImage.GetComponent<QuestTest>();
            qt.SetQuestId(c.questId);
        }

    }



    /* KMB */
    float currTime;

    float createTime;

    //���� ���� UI ����
    public GameObject gameLabel;

    //���ӻ��� UI�ؽ�Ʈ ������Ʈ ����
    Text gameText;

    //�̱��� ����
    public static GameManager gm;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    //���� ���� ����
    public enum GameState
    {
        Ready,
        Run,
        //Pause,
        GameOver
    }

    //���� ���� ���� ����
    public GameState gState;

    //�÷��̾� ���� Ŭ���� ����
    PlayerMove player;


    ////�ɼ�ȭ�� UI ������Ʈ ����
    //public GameObject gameOption;

    //�ɼ� ȭ�� �ѱ�
    //public void OpenOptionWindow()
    //{
    //    //�ɼ� â�� Ȱ��ȭ �Ѵ�.
    //    gameOption.SetActive(true);
    //    //���Ӽӵ��� 0�������� ��ȯ�Ѵ�.
    //    Time.timeScale = 0f;
    //    //���� ���¸� �Ͻ����� ���·� �����Ѵ�.
    //    gState = GameState.Pause;
    //}

    //�����ϱ� �ɼ�
    //public void CloseOptionWindow()
    //{
    //    //�ɼ�â�� ��Ȱ��ȭ �Ѵ�
    //    gameOption.SetActive(false);
    //    //���Ӽӵ��� 1�������� ��ȯ�Ѵ�.
    //    Time.timeScale = 1f;
    //    //���ӻ��¸� ������ ���·� �����Ѵ�.
    //    gState = GameState.Run;
    //}

    //�ٽ��ϱ� �ɼ�
    public void RestartGame()
    {
        //���� �ӵ��� 1�������� ��ȯ�Ѵ�
        Time.timeScale = 1f;
        //������ ��ȣ�� �ٽ� �ε��Ѵ�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //���� ���� �ɼ�
    public void QuitGame()
    {
        //���ø����̼��� �����Ѵ�.
        Application.Quit();
    }

    public GameObject cursorlock;

    // Start is called before the first frame update
    void Start()
    {
        //�ʱ� ���� ���´� �غ� ���·� �����Ѵ�.
        gState = GameState.Ready;

        //���� ���� UI ������Ʈ���� Text ������Ʈ�� �����´�.
        gameText = gameLabel.GetComponent<Text>();

        //���� �ؽ�Ʈ�� ������ "Ready"���Ѵ�.
        gameText.text = "Ready...";

        //�����ؽ�Ʈ�� ������ ��Ȳ������ �Ѵ�.
        gameText.color = new Color32(255, 185, 0, 255);

        //���� �غ� -> �غ��� ���·� ��ȯ�ϱ�
        StartCoroutine(ReadyToStart());

        //�÷��̾� ������Ʈ�� ã�� �� �÷��̾��� PlayerMove ������Ʈ �޾ƿ���
        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    IEnumerator ReadyToStart()
    {
        //2�ʰ� �����Ѵ�..
        yield return new WaitForSeconds(2f);

        //���� �ؽ�Ʈ�� ������ "Go!"�� �Ѵ�.
        gameText.text = "Go!";

        //0.5�ʰ� �����Ѵ�..
        yield return new WaitForSeconds(0.5f);

        //���� �ؽ�Ʈ�� ��Ȱ��ȭ �Ѵ�.
        gameLabel.SetActive(false);

        //���¸� ������ ���·� �����Ѵ�.
        gState = GameState.Run;
    }

    public GameObject btn;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(TalkImage.activeSelf == false)
            {
                TalkImage.SetActive(true);

                QuestTest qt = TalkImage.GetComponent<QuestTest>();
                qt.SetQuestId(QuestManager.qm.questId);
            }
        }

        // �ð��� �帣�� ����
        //currTime += Time.deltaTime;

        //���� �÷��̾��� hp�� 0 ���϶���....
        //if (player.hp <= 0)
        //{

        //    //�÷��̾��� �ִϸ��̼��� ������
        //    player.GetComponentInChildren<Animator>().SetFloat("MoveMotion", 0f);

            //���� �ؽ�Ʈ�� Ȱ��ȭ �Ѵ�
            gameLabel.SetActive(true);

            //�����ؽ�Ʈ�� ������ "���ӿ���"�� �Ѵ�.
            gameText.text = "Game Over";

            //���� �ؽ�Ʈ�� ������ ���� ������ �Ѵ�.
            gameText.color = new Color32(255, 0, 0, 255);


            //btn.SetActive(true);
            ////���� �ؽ�Ʈ�� �ڽ� ������Ʈ�� Ʈ������ ������Ʈ�� �����´�
            //Transform buttons = gameText.transform.GetChild(0);

            ////��ư ������Ʈ�� Ȱ��ȭ�Ѵ�
            //buttons.gameObject.SetActive(true);

            //���콺 Ȱ��ȭ �Լ��� �����Ѵ�.
            //Cursorlock�̶��� ��ũ��Ʈ�� �ҷ�����
            // Cursorlock_Rio cl = cursorlock.GetComponent<Cursorlock_Rio>();
            //�� �ȿ� �ִ� Ŀ������ ������Ű��
            // cl.CursorOn();
            //���¸� '���ӿ���' ���·� �����Ѵ�.

            gState = GameState.GameOver;
        }
    
}
