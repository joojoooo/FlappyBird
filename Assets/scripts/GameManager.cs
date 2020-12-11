using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum GameState { Menu, Playing, GameOver, Fade };

    private PipeSpawner pipeSpawner;
    public GameObject getReady;
    public GameObject canvas;
    public GameObject flash;
    public GameObject blackFade;
    public Text scoreText;
    public GameObject[] birdPrefabs;
    private Bird[] birds;
    private int birdCnt = 0;
    private int birdsAliveCnt = 0;
    private int[] controllerBirdMap;
    private GameState gameState = GameState.Menu;
    private bool waitingForRestartGameFadeIn = false;

    void Start()
    {
        birds = new Bird[4];
        controllerBirdMap = new int[4];
        SpawnBird();

        pipeSpawner = FindObjectOfType<PipeSpawner>();
    }

    void Update()
    {
        if (birdCnt < 4 && gameState == GameState.Menu)
        {
            if (controllerBirdMap[1] == 0 && Input.GetButton("Jump1") == true) { controllerBirdMap[1] = birdCnt; SpawnBird(1, 1); }
            else if (controllerBirdMap[2] == 0 && Input.GetButton("Jump2") == true) { controllerBirdMap[2] = birdCnt; SpawnBird(2, 1); }
            else if (controllerBirdMap[3] == 0 && Input.GetButton("Jump3") == true) { controllerBirdMap[3] = birdCnt; SpawnBird(3, 1); }
        }
    }

    private void SpawnBird(int controllerNumber = 0, int type = 0)
    {
        birds[birdCnt] = Instantiate(birdPrefabs[type]).GetComponent<Bird>();
        birds[birdCnt].birdNumber = birdCnt;
        birds[birdCnt].controllerNumber = controllerNumber;
        birdCnt++;
    }

    public bool StartGame(int birdNumber)
    {
        if (gameState != GameState.Menu && gameState != GameState.Playing) return false;
        birdsAliveCnt++;
        if (gameState == GameState.Playing) return true;
        gameState = GameState.Playing;
        pipeSpawner.enabled = true;
        getReady.GetComponent<SpriteFade>().StartFade();
        return true;
    }

    public void SetScore(int birdNumber, int score)
    {
        scoreText.text = score.ToString("0");
    }

    public void birdDeath(int birdNumber)
    {
        GameObject flashInstance = Instantiate(flash);
        flashInstance.transform.SetParent(canvas.transform, false);
        birdsAliveCnt--;

        if (birdCnt == 1 || birdsAliveCnt == 0)
        {
            pipeSpawner.enabled = false;
            foreach (PipeMove movePipe in FindObjectsOfType<PipeMove>()) movePipe.enabled = false;
            FindObjectOfType<GroundMove>().enabled = false;
            gameState = GameState.GameOver;
        }
    }

    public void RestartGameRequest(int birdNumber)
    {
        if (gameState == GameState.GameOver && (birdCnt == 1 || birdsAliveCnt == 0))
        {
            gameState = GameState.Fade;
            Invoke("RestartGame", 0.5f);
        }
    }

    private void RestartGame()
    {
        GameObject blackFadeInstance = Instantiate(blackFade);
        blackFadeInstance.transform.SetParent(canvas.transform, false);
        BlackFade bf = blackFadeInstance.GetComponent<BlackFade>();
        bf.gameManager = this;
        bf.FadeIn();
        waitingForRestartGameFadeIn = true;
    }

    public void FadeInDone(BlackFade blackFade)
    {
        if (waitingForRestartGameFadeIn)
        {
            foreach (PipeMove movePipe in FindObjectsOfType<PipeMove>()) movePipe.DestroyPipe();
            FindObjectOfType<GroundMove>().enabled = true;
            for (int i = 0; i < birdCnt; i++) { birds[i].ResetBird(); SetScore(i, 0); }
            getReady.SetActive(true);

            waitingForRestartGameFadeIn = false;
            blackFade.FadeOut();
            gameState = GameState.Menu;
        }
    }
}