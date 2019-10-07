using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Fade");
        }
    }
    public void btnStartGame()
    {
        animator.SetTrigger("Fade");
    }

    public void btnExit()
    {
        Application.Quit();
    }


    public void OnFadeComplete()
    {
        SceneManager.LoadScene(1);
    }
}
