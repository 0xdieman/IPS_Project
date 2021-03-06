using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    private Audio audioRing;
    void Start()
    {
        gameObject.tag = GameManager.RING_TAG;
        audioRing = GameObject.FindGameObjectWithTag(GameManager.AUDIO_TAG).GetComponent<Audio>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.CompareTag(GameManager.SHEEP_TAG)) {
            
            audioRing.winPointSound();
            Debug.Log("A sheep entered the ring");
            FindClosestPlayerAndScore();
        }
    }

    private void FindClosestPlayerAndScore() {
        GameObject sheep = GameObject.FindGameObjectWithTag(GameManager.SHEEP_TAG);
        GameObject closestPlayer = sheep.GetComponent<GhostSheepBehavior>().FindClosestPlayer();
        Debug.Log("Found closest player with tag " + closestPlayer.tag);
        GameObject.FindGameObjectWithTag(GameManager.CONTROLLER_TAG).GetComponent<GameManager>().addScoreToPlayer(closestPlayer, 1);
    }
}
