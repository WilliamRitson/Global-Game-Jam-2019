using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{

    private float mateFert = 0;
    private float mateStam = 0;
    private float mateManuv = 0;

    private float fScale = 1;
    private float sScale = 1;
    private float mScale = 1;

    private float youFert = 0;
    private float youStam = 0;
    private float youManuv = 0;

    private float childFert = 0;
    private float childStam = 0;
    private float childManuv = 0;

    private int hearts = -1;
    private int eggs = -1;

    private static Stats instance = null;

    public static Stats Instance
    {
        get
        {
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            GameObject.Destroy(gameObject);
        } else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetMateScales (ScaleBird sb)
    {
        fScale = sb.eScale;
        sScale = sb.bScale;
        mScale = sb.wScale;

        mateFert = (fScale / 7) * 100;
        mateStam = (sScale / 3) * 100;
        mateManuv = (mScale / 8.5f) * 100;

    }

    public void SetMateScales(ScaleBird sb)
    {
        sb.eScale = fScale;
        sb.bScale = sScale;
        sb.wScale = mScale;
    }

    public void GetYouStats (PlayerController pc)
    {
        youFert = pc.fertility;
        youStam = pc.stamina;
        youManuv = pc.manuverability;
    }

    public void GenerateChildStats()
    {
        childFert = (youFert + mateFert) / 2;
        childStam = (youStam + mateStam) / 2;
        childManuv = (youManuv + mateManuv) / 2;

    }

    public void MakeChildYou ()
    {
        youFert = childFert;
        youStam = childStam;
        youManuv = childManuv;

    }

    public void SetYouStats(PlayerController pc)
    {
        pc.fertility = youFert;
        pc.stamina = youStam;
        pc.manuverability = youManuv;
    }

    public void LoadHeartsEggs(PlayerController pc)
    {
        hearts = pc.Hearts;
        eggs = pc.Eggs;
    }

    public void SetHeartsEggs(PlayerController pc)
    {
        pc.Hearts = hearts;
        pc.Eggs = eggs;
    }

    public void ClearHeartsEggs()
    {
        hearts = -1;
        eggs = -1;
    }
}
