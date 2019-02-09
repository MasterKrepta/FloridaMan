using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] float RespawnTime;
    // Start is called before the first frame update
    void Awake()
    {
        GameEvents.OnPlayerRespawn += BeginRespawn;
    }

    
    void BeginRespawn() {
        StartCoroutine(RespawnDelay());
    }
    void RespawnPlayer() {
        GameObject player = FindObjectOfType<PlayerMovement>().gameObject;
        Renderer[] rends = player.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in rends) {
            renderer.enabled = true;
        }
        player.transform.position = Vector3.zero;
        MonoBehaviour[] allScripts = player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour m in allScripts) {
            m.enabled = true;
        }
        GameEvents.OnPlayerAlive();
        //player.enabled = true;

        //player.SetActive(true);
    }



    IEnumerator RespawnDelay() {
        yield return new WaitForSeconds(RespawnTime);
        RespawnPlayer();
    }

    private void OnDestroy() {
        GameEvents.OnPlayerRespawn -= BeginRespawn;
    }
}
