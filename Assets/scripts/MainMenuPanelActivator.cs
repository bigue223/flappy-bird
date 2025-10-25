using UnityEngine;

public class MainMenuPanelActivator : MonoBehaviour
{
    public GameObject mainMenuPanel;

    void Start()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
    }
}
