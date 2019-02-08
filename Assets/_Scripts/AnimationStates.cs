﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStates : MonoBehaviour
{
    [SerializeField] GameObject RightHand, LeftHand, RightLeg, LeftLeg, GrabPoint;

    void RightHand_ON() {
        RightHand.SetActive(true);
    }
    void RightHand_OFF() {
        RightHand.SetActive(false);
    }
    void LeftHand_ON() {
        LeftHand.SetActive(true);
    }
    void LeftHand_OFF() {
        LeftHand.SetActive(false);
    }

    void RightLeg_ON() {
        RightLeg.SetActive(true);
    }
    void RightLeg_OFF() {
        RightLeg.SetActive(false);
    }

    void LeftLeg_ON() {
        LeftLeg.SetActive(true);
    }
    void LeftLeg_OFF() {
        LeftLeg.SetActive(false);
    }

    void Grab_ON() {
        GrabPoint.SetActive(true);
    }

    void Grab_OFF() {
        GrabPoint.SetActive(false);
    }
}