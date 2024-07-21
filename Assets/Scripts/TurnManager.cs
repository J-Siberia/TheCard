using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public List<GameObject> players; // �v���C���[�L�����N�^�[�̃��X�g
    public List<GameObject> enemies; // �G�L�����N�^�[�̃��X�g
    public int pNum;
    public int eNum;
    public GameObject hero;
    public GameObject enemy01;
    public GameObject enemy02;

    public GameObject deck;
    public GameObject MatchManager;
    public GameObject BattleManager;
    //public GameObject ArrowManager;
    public GameObject AudioManager;
    public GameObject battleWindow;
    public GameObject endWindow;
    public TextMeshProUGUI endMes;
    public GameObject emotionPanic;
    public bool isPanic = false;

    //private int currentPlayerIndex = 0; // ���݂̃v���C���[�̃C���f�b�N�X
    //private int currentEnemyIndex = 0; // ���݂̓G�̃C���f�b�N�X
    //private bool isPlayerTurn = true; // �^�[�����v���C���[�̂��̂��ǂ���

    public int currentTurn = 0; // ���݂̃^�[����
    public bool isMatchPhase = false; // �}�b�`�t�F�[�Y�i����\�j���ǂ���

    //private bool isPaused = false; // ������~���邩�ǂ����������t���O
    private Hero heroComponent;
    private Enemy01 enemy01Component;
    private Enemy02 enemy02Component;

    private Deck deckComponent;
    private MatchManager matchManager;
    private BattleManager battleManager;
    // private ArrowManager arrowManager;
    private AudioManager audioManager;

    public List<MatchData> matchDataList;

    public bool isSkipButton = false;

    public int slimeMimic = 0;

    void Awake()
    {
        players.Add(hero);
        enemies.Add(enemy01);
        enemies.Add(enemy02);

        pNum = players.Count;
        eNum = enemies.Count;

        MatchManager = GameObject.Find("MatchManager");
        matchManager = MatchManager.GetComponent<MatchManager>();

        BattleManager = GameObject.Find("BattleManager");
        battleManager = BattleManager.GetComponent<BattleManager>();

        AudioManager = GameObject.Find("AudioManager");
        audioManager = AudioManager.GetComponent<AudioManager>();

        //ArrowManager = GameObject.Find("ArrowManager");
        //arrowManager = ArrowManager.GetComponent<ArrowManager>();

        battleWindow = GameObject.Find("BattleWindow");

        emotionPanic = GameObject.Find("EmotePanic");

        heroComponent = hero.GetComponent<Hero>();
        enemy01Component = enemy01.GetComponent<Enemy01>();
        enemy02Component = enemy02.GetComponent<Enemy02>();

        endWindow.SetActive(false);
        emotionPanic.SetActive(false);
    }

    void Start()
    {
        Debug.Log("�Q�[���J�n");
      
        deckComponent = deck.GetComponent<Deck>();
        deckComponent.DrawCard(7);

        // �Q�[���J�n���ɍŏ��̃v���C���[�^�[�����J�n
        StartTurn();
    }

    private int CalculateNumDice(int mental)
    {
        int absoluteMental = Mathf.Abs(mental);

        switch (absoluteMental)
        {
            case int n when n < 4:
                return 0;
            case int n when n >= 4 && n < 9:
                return 1;
            case int n when n >= 9 && n < 20:
                return 2;
            case int n when n >= 20 && n < 37:
                return 3;
            case int n when n >= 37 && n < 48:
                return 4;
            case int n when n >= 48:
                return 5;
            default:
                return 0;
        }
    }
    
    private void StartTurn()
    {
        // �v���C���[�̍s���I����UI�̍X�V�Ȃǂ̏����������ɒǉ�
        currentTurn++;

        deckComponent.DrawCard(1);

        if(hero != null)
        {
            heroComponent.GenerateDice(hero, true, 4 + CalculateNumDice(heroComponent.mentalPoint));
        }
        if(enemy01 != null)
        {
            enemy01Component.GenerateDice(enemy01, false, 3 + CalculateNumDice(enemy01Component.mentalPoint));
        }
        if(enemy02 != null)
        {
            enemy02Component.GenerateDice(enemy02, false, 1 + CalculateNumDice(enemy02Component.mentalPoint));
        } 

        List<MatchData> matchDataList = new List<MatchData>();

        Debug.Log("�^�[���J�n");
        isMatchPhase = true;
        battleWindow.SetActive(false);

        if (heroComponent.mentalPoint >= 50 || heroComponent.mentalPoint <= -50)
        {
            isPanic = true;
        }
        else
        {
            isPanic = false;
        }
    }

    public void StartBattle()
    {
        isMatchPhase = false;
        battleWindow.SetActive(true);

        foreach (GameObject enemy in enemies)
        {
            matchManager.RandomMatch(enemy, hero);
        }
        matchDataList = matchManager.SortMatch();

        Debug.Log("�o�g���J�n");
        audioManager.ToggleBGM();
        StartCoroutine(BattleCoroutine());
    }

    private IEnumerator BattleCoroutine()
    {
        for (int i = 0; i < matchDataList.Count; i++)
        {
            battleManager.Battle(matchDataList[i]);

            isSkipButton = true;
            while (isSkipButton)
            {
                //yield return null;
                yield return new WaitForSeconds(0.1f);
            }
        }

        Debug.Log("�o�g���I��");

        EndTurn();
    }


    private void EndTurn()
    {
        Debug.Log("�^�[���I��");

        matchManager.matchDataList = new List<MatchData>();

        if (isPanic)
        {
            heroComponent.mentalPoint = 0;
        }

        audioManager.ToggleBGM();
        StartTurn();
    }

    void Update()
    {
        if(isMatchPhase && isPanic)
        {
            emotionPanic.SetActive(true);
        }
        else
        {
            emotionPanic.SetActive(false);
        }
    }

    public void GameEnd(bool isEnemy)
    {
        if (isEnemy)
        {
            endWindow.SetActive(true);
            Time.timeScale = 0;
            endMes.text = "You Win!";
            audioManager.GameEnd(true);
        }
        else
        {
            endWindow.SetActive(true);
            Time.timeScale = 0;
            endMes.text = "You Lose...";
            audioManager.GameEnd(false);
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0.0f; // ���Ԃ̐i�s���~
    }

    private void ResumeGame()
    {
        Time.timeScale = 1.0f; // ���Ԃ̐i�s��ʏ�ɖ߂�
    }

    // �w�肵���b����ɍĊJ�����
    private void ResumeGameAfterDelay(float pauseDuration)
    {
        StartCoroutine(ResumeAfterDelay(pauseDuration));
    }

    private IEnumerator ResumeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResumeGame();
    }

}
