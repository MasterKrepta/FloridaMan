using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownEffect : MonoBehaviour
{
    Transform player;
    [Range (0,1)]
    [SerializeField] float slowDownTime = .5f;
    [SerializeField] float animateTime = 1f;
    [SerializeField] float camSpeed = 5f;
    Camera mainCam;
    
    [SerializeField] Vector3 camOffset = new Vector3(3,0,0);
    Vector3 origCameraPos;

    CameraFollow cameraFollow;
    PlayerMovement playerMove;
    
    private void Awake() {
        player = GameObject.FindWithTag("Player").transform;
        playerMove = player.GetComponent<PlayerMovement>();
        cameraFollow = GetComponent<CameraFollow>();
        GameEvents.OnEffectMode_ON += SlowDownTime;
        GameEvents.OnEffectMode_ON += AnimateCameraEffect;
        GameEvents.OnEffectMode_OFF += ResetCameraAndTime;
        mainCam = Camera.main;
        origCameraPos = mainCam.transform.position;

    }
    void SlowDownTime() {
        Time.timeScale = slowDownTime;
    }

    void AnimateCameraEffect() {
        origCameraPos = mainCam.transform.position;
        playerMove.enabled = false;
        cameraFollow.enabled = false;
        StartCoroutine(AnimateCamera());
    }

    IEnumerator AnimateCamera() {

        float duration = 1.0f;
        for (float t = 0.0f; t < duration; t += Time.deltaTime) {
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, player.position + camOffset, Time.deltaTime * camSpeed);
            mainCam.transform.LookAt(player);
            yield return 0;
        }
    }

    void ResetCameraAndTime() {
        StartCoroutine(ResetCamera());
    }

    IEnumerator ResetCamera() {
        float duration = .5f;
        for (float t = 0.0f; t < duration; t += Time.deltaTime) {
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, origCameraPos, Time.deltaTime * camSpeed);
            mainCam.transform.rotation = Quaternion.identity;
            //mainCam.transform.LookAt(player);
            yield return 0;
        }
        
        Time.timeScale = 1;
        
        playerMove.enabled = true;
        cameraFollow.enabled = true;
        //mainCam.transform.rotation = Quaternion.EulerAngles(Vector3.zero);
    }
    private void OnDestroy() {
        GameEvents.OnEffectMode_ON -= SlowDownTime;
        GameEvents.OnEffectMode_ON -= AnimateCameraEffect;
    }
}
