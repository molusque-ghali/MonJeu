using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadGame()
    {
        SceneManager.LoadScene(1);
    }
}
