using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum GameState { Menu, Playing, GameOver };

    public PipeSpawner pipeSpawner;
    public GameObject getReady;
    public GameObject canvas;
    public GameObject flash;
    public GameObject blackFade;
    public Text scoreText;
    public GameObject bird;
    private Bird[] birds;
    private int birdCnt = 0;
    private GameState gameState = GameState.Menu;
    private bool waitingForFadeIn = false;

    void Start()
    {
        birds = new Bird[4];
        GameObject birdInstance = Instantiate(bird);
        birds[birdCnt++] = birdInstance.GetComponent<Bird>();

        pipeSpawner = FindObjectOfType<PipeSpawner>();
    }

    void Update()
    {
    }

    public void StartGame(int birdNumber)
    {
        if (gameState == GameState.Playing) return;
        gameState = GameState.Playing;
        pipeSpawner.enabled = true;
        getReady.GetComponent<SpriteFade>().StartFade();
    }

    public void SetScore(int birdNumber, int score)
    {
        scoreText.text = score.ToString("0");
    }

    public void birdDeath(int birdNumber)
    {
        GameObject flashInstance = Instantiate(flash);
        flashInstance.transform.SetParent(canvas.transform, false);

        if (birdCnt == 1)
        {
            pipeSpawner.enabled = false;
            foreach (PipeMove movePipe in FindObjectsOfType<PipeMove>()) movePipe.enabled = false;
            FindObjectOfType<GroundMove>().enabled = false;
            gameState = GameState.GameOver;
        }
    }

    public void RestartGameRequest(int birdNumber)
    {
        if (birdCnt == 1)
        {
            Invoke("RestartSingleGame", 0.5f);
        }
        else
        {
        }
    }

    private void RestartSingleGame()
    {
        GameObject blackFadeInstance = Instantiate(blackFade);
        blackFadeInstance.transform.SetParent(canvas.transform, false);
        BlackFade bf = blackFadeInstance.GetComponent<BlackFade>();
        bf.gameManager = this;
        bf.FadeIn();
        waitingForFadeIn = true;
    }

    public void FadeInDone(BlackFade blackFade)
    {
        if (!waitingForFadeIn) return;
        foreach (PipeMove movePipe in FindObjectsOfType<PipeMove>()) movePipe.DestroyPipe();
        FindObjectOfType<GroundMove>().enabled = true;
        for (int i = 0; i < birdCnt; i++) { birds[i].ResetBird(); SetScore(i, 0); }
        getReady.SetActive(true);

        waitingForFadeIn = false;
        blackFade.FadeOut();
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}