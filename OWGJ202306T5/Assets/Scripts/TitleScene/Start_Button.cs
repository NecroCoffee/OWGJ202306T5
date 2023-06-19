using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_Button : MonoBehaviour
{

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Stage1");
    }
}
