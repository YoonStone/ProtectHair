using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//완전 어렵게
public class Level3 : MonoBehaviour
{
    public Transform[] rainSpawns, umSpawns, thunder_po, spark_po;

    public float minDelay_R, maxDelay_R,
        minDelay_U, maxDelay_U,
        minDelay_I, maxDelay_I,
        minDelay_T, maxDelay_T;

    void Start()
    {
        Invoke("StartDelay", 1f);
    }

    // 1초 후 게임시작
    void StartDelay()
    {
        MakeRain();
        StartCoroutine("DelayUmbrella");
        StartCoroutine("DelayItem");
    }

    //비 생성
    void MakeRain()
    {
        int rand = Random.Range(0, 8); //파란비:검정비:황사비 = 5:2:1

        if (rand == 1)
            Instantiate(Resources.Load("Prefabs/2D/YellowRain"), rainSpawns[Random.Range(0, 6)]);
        else if (rand == 2 || rand == 3)
            Instantiate(Resources.Load("Prefabs/2D/BlackRain"), rainSpawns[Random.Range(0, 6)]);
        else
            Instantiate(Resources.Load("Prefabs/2D/BlueRain"), rainSpawns[Random.Range(0, 6)]);

        StartCoroutine("DelayRain");
    }

    //아이템 생성
    void MakeItem()
    {
        int rand = Random.Range(0, 9); //햄버거:아몬드:소주:발모제 = 4:3:2:1

        if (rand == 0 || rand == 1)
            Instantiate(Resources.Load("Prefabs/2D/Soju"), rainSpawns[Random.Range(0, 6)]);
        else if (rand == 2 || rand == 3 || rand == 4)
            Instantiate(Resources.Load("Prefabs/2D/Almond"), rainSpawns[Random.Range(0, 6)]);
        else
            Instantiate(Resources.Load("Prefabs/2D/Hamburger"), rainSpawns[Random.Range(0, 6)]);

        StartCoroutine("DelayItem");
    }

    //번개 생성
    void MakeThunder()
    {
        int rand = Random.Range(0, 5);
        Object thuder = Instantiate(Resources.Load("Prefabs/2D/Thunder_po"), thunder_po[rand]);
        StartCoroutine("DelaySpark", rand);
        Destroy(thuder, 3.6f);
        StartCoroutine("DelayThunder");
    }

    //우산 생성
    void MakeUmbrella()
    {
        int rand = Random.Range(0, 2); //일반우산:찢어진우산 = 1:1
        Object umbrella;
        if (rand == 1)
        {
            umbrella = Instantiate(Resources.Load("Prefabs/2D/Umbrella_x"), umSpawns[Random.Range(0, 5)]);
            Destroy(umbrella, 3f);
        }
        else
        {
            umbrella = Instantiate(Resources.Load("Prefabs/2D/Umbrella_o"), umSpawns[Random.Range(0, 5)]);
            Destroy(umbrella, 3f);
        }


        StartCoroutine("DelayUmbrella");
    }

    //비 딜레이시간
    IEnumerator DelayRain()
    {
        float delay = Random.Range(minDelay_R, maxDelay_R);
        yield return new WaitForSeconds(delay);
        MakeRain();
    }

    //아이템 딜레이시간
    IEnumerator DelayItem()
    {
        float delay = Random.Range(minDelay_I, maxDelay_I);
        yield return new WaitForSeconds(delay);
        MakeItem();
    }

    //우산 딜레이시간
    IEnumerator DelayUmbrella()
    {
        float delay = Random.Range(minDelay_U, maxDelay_U);
        yield return new WaitForSeconds(delay);
        MakeUmbrella();
    }

    //번개자국 딜레이시간
    IEnumerator DelayThunder()
    {
        yield return new WaitForSeconds(Random.Range(minDelay_T, maxDelay_T));
        MakeThunder();
    }

    //번개 딜레이 후 생성
    IEnumerator DelaySpark(int rand)
    {
        yield return new WaitForSeconds(3f);
        Object spark = Instantiate(Resources.Load("Prefabs/2D/Spark"), spark_po[rand]);
        GameObject spark_go = GameObject.FindWithTag("Spark") as GameObject;
        spark_go.GetComponent<BoxCollider2D>().enabled = true;
        Destroy(spark, 0.6f);
    }

}
