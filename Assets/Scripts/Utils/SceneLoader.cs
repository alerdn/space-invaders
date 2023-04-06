using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string s)
    {
        SceneManager.LoadScene(s);
    }
    
    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
