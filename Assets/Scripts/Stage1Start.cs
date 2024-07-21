using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Stage1Start : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene("battle", LoadSceneMode.Single);
    }
}
