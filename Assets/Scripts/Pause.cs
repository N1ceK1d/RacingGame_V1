using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool isPause = false;
    public Canvas canvas;
    
    private void Start() {
        canvas = GetComponent<Canvas>();
        Debug.Log(canvas);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ShowHideMenu();
        }
    }
    public void ShowHideMenu()
    {
    	isPause = !isPause;
    	canvas.enabled = isPause;
        Time.timeScale = isPause ? 0f : 1f;
    }
}
