using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{

   public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void ShowPanel(GameObject panel) {
        panel.SetActive(true);
    }

    public void Back(GameObject btn) {
        GameObject panel = btn.transform.parent.gameObject;
        panel.gameObject.SetActive(false);
    }
    public void QuitGame() {
        Application.Quit();
    }
}
