using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
     public Player player;
     
     public Text scoreText;
     public Text diamondScoreText; // Ce champ doit pointer vers le Text déplacé dans le Canvas
     public GameObject playButton;
     public GameObject gameOver;
     public GameObject mainMenuPanel;
     public GameObject pausePanel; // Assignez ce panel dans l'inspecteur
     public int score ;
     public int diamondScore;      // Score des diamants
     public AudioClip backgroundMusicClip;
     public AudioClip scoreBonusClip; // Ajoutez ce champ et assignez le son dans l'inspecteur
     private AudioSource backgroundMusicSource;
     private bool isGameOver = false;

     public float gameSpeed = 5f; // Vitesse de base
     public float speedIncreaseRate = 0.1f; // Incrément de vitesse par seconde

    private void Awake()
    {
        Application.targetFrameRate = 60;
        PauseGame();
       

        if (backgroundMusicClip != null)
        {
            backgroundMusicSource = gameObject.AddComponent<AudioSource>();
            backgroundMusicSource.clip = backgroundMusicClip;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }

    private void Update()
    {
        // Augmente la vitesse de jeu au cours du temps si le jeu n'est pas en pause ou game over
        if (!isGameOver && Time.timeScale > 0f)
        {
            gameSpeed += speedIncreaseRate * Time.deltaTime;
            Pipes.speed = gameSpeed; // Applique la vitesse globale aux pipes
            Parallax[] parallaxList = FindObjectsOfType<Parallax>();
            foreach (var p in parallaxList)
            {
                p.animationSpeed = gameSpeed * 0.2f; // Ajustez le facteur selon votre ressenti
            }
        }

        if (isGameOver && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            Play();
            isGameOver = false;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        
            
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        // Masque le panel pause même s'il est déjà désactivé
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        if (pausePanel != null)
            pausePanel.SetActive(false);
        SceneManager.LoadScene("mainmenu");
    }

    public void Start()
    {
        PauseGame();
        gameOver.SetActive(false); 
    }




    public void GameOver()
    {
        // N'affiche pas le Game Over si le GameManager est désactivé
        if (!this.gameObject.activeInHierarchy)
            return;

        gameOver.SetActive(true);
        playButton.SetActive(false);
        PauseGame();
        isGameOver = true;

    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();

        // Increase pipe speed every 5 points
        if (score % 5 == 0)
        {
            Pipes.IncreaseSpeed(1f); // Increase speed by 1, adjust as needed
        }

        // Jouer le son bonus si score multiple de 10
        if (score % 10 == 0 && scoreBonusClip != null)
        {
            if (backgroundMusicSource != null)
                backgroundMusicSource.PlayOneShot(scoreBonusClip);
        }
    }
    public void vitesse()
    {
        while (score <= 20)
        {
            if (score % 2 == 0)
            {
                FindObjectOfType<Parallax>().animationSpeed += 0.5f;
            }
        }
    }
    public void Play()
    {
        score = 0;
        diamondScore = 0;
        scoreText.text = score.ToString();
        diamondScoreText.text = diamondScore.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        // Réinitialise la vitesse de jeu
        gameSpeed = 5f;
        Pipes.speed = gameSpeed;
        Parallax[] parallaxList = FindObjectsOfType<Parallax>();
        foreach (var p in parallaxList)
        {
            p.animationSpeed = gameSpeed * 0.2f;
        }

        // Stop la musique de fond
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }

        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        isGameOver = false;
    }

    public void IncreaseDiamondScore()
    {
        diamondScore++;
        diamondScoreText.text = diamondScore.ToString();
    }

}

// Aucun changement de code nécessaire pour changer le sprite du bird.
// Faites-le dans l'inspecteur Unity sur le composant Player.