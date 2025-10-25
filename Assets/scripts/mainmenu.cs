using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public AudioClip backgroundMusicClip;
    private AudioSource backgroundMusicSource;
    public GameObject mainMenuPanel; // Assignez ce panel dans l'inspecteur

    // Start is called before the first frame update
    void Start()
    {
        if (backgroundMusicClip != null)
        {
            backgroundMusicSource = gameObject.AddComponent<AudioSource>();
            backgroundMusicSource.clip = backgroundMusicClip;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayGame()
    {
        if (backgroundMusicSource != null)
            backgroundMusicSource.Stop();
        // Remplacez "FlappyBird" par le nom exact de votre scène de jeu
        SceneManager.LoadScene("FlappyBird");
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void StopMusic()
    {
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
            backgroundMusicSource.Pause();
    }

    public void ResumeMusic()
    {
        if (backgroundMusicSource != null && !backgroundMusicSource.isPlaying)
            backgroundMusicSource.UnPause();
    }

    public void IncreaseVolume()
    {
        if (backgroundMusicSource != null)
            backgroundMusicSource.volume = Mathf.Clamp01(backgroundMusicSource.volume + 0.1f);
    }

    public void DecreaseVolume()
    {
        if (backgroundMusicSource != null)
            backgroundMusicSource.volume = Mathf.Clamp01(backgroundMusicSource.volume - 0.1f);
    }

    public void GoToMainMenu()
    {
        if (backgroundMusicSource != null)
            backgroundMusicSource.Stop();
        SceneManager.LoadScene("mainmenu"); // Remplacez par le nom exact de votre scène menu si besoin
    }

    public void GoToMainMenuPanel()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
        // Désactivez ici d'autres panels si nécessaire, par exemple :
        // settingsPanel.SetActive(false);
        // pausePanel.SetActive(false);
    }
}
