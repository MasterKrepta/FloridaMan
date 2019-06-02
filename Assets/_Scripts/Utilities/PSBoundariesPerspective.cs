using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSBoundariesPerspective : MonoBehaviour {
    public Camera MainCamera; //be sure to assign this in the inspector to your main camera
    public CameraFollow camFollow;

    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    private bool LockScreen = false;

    private void OnEnable() {
        GameEvents.OnWaveStart += ToggleLockScreen;
        GameEvents.OnWaveEnd += ToggleLockScreen;
    }

    // Use this for initialization
    void Start() {
        UpdateBounds();
        //objectWidth = transform.GetComponentInChildren<Renderer>().bounds.extents.x; //extents = size of width / 2
        //objectHeight = transform.GetComponentInChildren<Renderer>().bounds.extents.y; //extents = size of height / 2

        objectWidth = -4;//TODO limit this properly instead of hardcoding
        objectHeight = -4;//TODO limit this properly instead of hardcoding
        camFollow = MainCamera.GetComponent<CameraFollow>();
    }

    private void UpdateBounds() {
        Debug.Log("New bounds");
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
    }

    // Update is called once per frame
    void LateUpdate(){
        if (LockScreen) {
            Vector3 viewPos = transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + objectWidth, screenBounds.x * -1 - objectWidth);
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight, screenBounds.y * -1 - objectHeight);
            transform.position = viewPos;
        }
        
    }

   void ToggleLockScreen() {
        UpdateBounds();
        LockScreen = !LockScreen;
        if (camFollow.enabled) {
            camFollow.enabled = false;
        }
        else {
            camFollow.enabled = true;
        }

    }
}