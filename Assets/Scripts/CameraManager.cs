using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraManager : MonoBehaviour
{

    public Camera camera1;
    public Camera camera2;
    public Camera mainCamera;
    public Camera subCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(WaitForSecondsCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator WaitForSecondsCoroutine()
    {
        yield return new WaitForSeconds(63);
        mainCamera.enabled = true;
        camera1.enabled = false;
        camera2.enabled = false;
        subCamera.enabled = false;
    }
}
