using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologScroll : MonoBehaviour
{
    // public RectTransform textRect;
    public Transform textTransform;

    public float scrollSpeed = 2f;
    public float scrollSpeedSkip = 15f;

    // public float endYPosition = 1000f;
    public float endY = 17.5f;
    public string nextSceneName = "Level1"; // Replace with your scene name

    private bool animating = true;
    
    void Update()
    {
        if (animating)
        {
            float currentScrollSpeed = scrollSpeed;
            if (Input.GetButton("Jump"))
            {
                currentScrollSpeed = scrollSpeedSkip;
            }

            textTransform.position += new Vector3(0f, 0.5f, 0.5f) * (currentScrollSpeed * Time.deltaTime);
            if (textTransform.position.y > endY)
            {
                animating = false;
                LoadNextScene();
            }
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}