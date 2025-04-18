using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float startSize = 0.5f;
    public float targetSize = 10f;
    public float zoomDuration = 4f;
    public float startDelay = 2f;
    public GameObject player;
    public GameObject hud;
    
    private float startTimer = 0f;
    private float zoomTimer = 0f;
    private Camera cam;
    private Vector3 startPosition;

    void Start()
    {
        cam = Camera.main;
        cam.orthographicSize = startSize;
        startPosition = transform.position;
    }

    void Update()
    {
        if (startTimer < startDelay)
        {
            startTimer += Time.deltaTime;
        }
        else if (zoomTimer < zoomDuration)
        {
            zoomTimer += Time.deltaTime;
            float t = zoomTimer / zoomDuration;
            cam.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            transform.position = Vector3.Lerp(startPosition, player.transform.position, t);
        }
        else
        {
            // start follow cam and player input
            FollowCam followCam = GetComponent<FollowCam>();
            followCam.enabled = true;

            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerGravityDrag>().enabled = true;
            
            hud.SetActive(true);
        }
    }
}