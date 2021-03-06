﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;
    [SerializeField]Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(TagsAndLayers.Player).transform;
    }

    // Update is called once per frame
    void Update()
    {
        cameraOffset = new Vector3(player.position.x, 6.5f, -20f);
        transform.position = cameraOffset;
        transform.rotation = Quaternion.identity;
    }
}
