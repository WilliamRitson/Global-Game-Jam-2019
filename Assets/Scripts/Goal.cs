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

    private NestEnding ne;

    private void Awake()
    {
        endClip = gameObject.GetComponent<AudioSource>();
        GameObject go = GameObject.FindGameObjectWithTag("LoveNest");
        if (go != null)
        {
            ne = go.GetComponent<NestEnding>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.collider.gameObject.CompareTag("Player")) {
            coroutine = EndLevel();
            triggered = true;
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            pc.Stat.GetYouStats(pc);
            if (nextLevel.Equals("Level 3"))
            {
                if (pc.mate)
                {
                    pc.Stat.GetMateScales(pc.mate.GetComponent<ScaleBird>());
                } else
                {
                    pc.die();
                    return;
                }
            }
            pc.ResetHealth();
            Debug.Log("Health should have been reset there:" + pc.Hearts);
            pc.Stat.LoadHeartsEggs(pc);
            if (nextLevel.Equals("Level 1"))
            {
                pc.Stat.GenerateChildStats();
                pc.Stat.MakeChildYou();
                pc.Stat.SetYouStats(pc);
                pc.Stat.ClearHeartsEggs();
                ne.TriggerAnim();
                return;
            }
            
            StartCoroutine(coroutine);
        }
        if (collision.collider.gameObject.CompareTag("Obstacle"))
        {
            GameObject.Destroy(gameObject);
        }
    }

    public void End()
    {
        StartCoroutine(EndLevel());
    }

    private IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(0.2f);
        endClip.Play();
        yield return new WaitForSeconds(endClip.clip.length);
        SceneManager.LoadScene(nextLevel);        
    }
}
