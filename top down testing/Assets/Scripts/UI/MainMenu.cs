using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionMenuUI;

    public void PlayGame()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    public void OpenOptionMenu()
    {
        optionMenuUI.GetComponent<OptionMenu>().SetBackMenuUI(this.gameObject);
        this.gameObject.SetActive(false);
        optionMenuUI.SetActive(true);
    }
}
