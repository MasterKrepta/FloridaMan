using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(TagsAndLayers.Tags.Player)) {
            GameEvents.OnWaveStart();
            GameObject.Destroy(this.gameObject);
        }
    }
}
