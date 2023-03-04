using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject optionMenuUI;

    public void OpenOptionMenu()
    {
        optionMenuUI.GetComponent<OptionMenu>().SetBackMenuUI(this.gameObject);
        this.gameObject.SetActive(false);
        optionMenuUI.SetActive(true);
    }

    public void Quit()
    {
        // PlayerPrefs.SetFloat("pyrX", player.transform.localPosition.x);
        // PlayerPrefs.SetFloat("pyrY", player.transform.localPosition.y);

        // AudioSource song = player.GetComponentInChildren<AudioSource>();
        // float songTime = (float)song.time;
        // PlayerPrefs.SetFloat("songTime", songTime);
        Application.Quit();
    }
}
