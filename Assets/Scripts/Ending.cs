using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public PauseMenu pauseMenu;

    private void Awake()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuEnter()
    {
        pauseMenu.MainMenuButton();
    }
}
