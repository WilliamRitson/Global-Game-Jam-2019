using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{

    public string nextLevel;
    private AudioSource endClip;
    private IEnumerator coroutine;
    private bool triggered = false;

    private void Awake()
    {
        endClip = gameObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.collider.gameObject.CompareTag("Player")) {
            coroutine = EndLevel();
            triggered = true;
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(0.2f);
        endClip.Play();
        yield return new WaitForSeconds(endClip.clip.length);
        SceneManager.LoadScene(nextLevel);        
    }
}
