using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class HomeScreen : MonoBehaviour
{
    [SerializeField] GameObject shopMenu;

    public void PlayGame()
    {
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenShop()
    {
        AudioManager.instance.Play("Click");
        shopMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
