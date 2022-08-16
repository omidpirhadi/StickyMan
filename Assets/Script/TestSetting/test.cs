using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class test : MonoBehaviour
{
    public Button btn;
    public GameObject prefab;
    public List<GameObject> omid;
    void Start()
    {
        btn.onClick.AddListener(() => {


            StartCoroutine(JOB());

        });

        async_omid();
       
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator JOB()
    {
        async_omid();
        yield return null;
        Task_job3();

    }
    public async void async_omid()
    {
         await Task_job3();
        await Task.WhenAll(Task_job1(), Task_job2());

        // await Task_job2();
    }
    public async void async_omid2()
    {
        await Task_job3();

        // await Task_job2();
    }

    private Task Task_job1()
    {
        Task t = Task.Run(() =>
        {

            job();
        });
        return t;
    }
    private Task Task_job2()
    {
        Task t = Task.Run(() =>
        {

            job3();
        });
        return t;
    }
    private Task Task_job3()
    {
        Task t = Task.Run(() =>
        {

            job2();
        });
        return t;
    }
    private void job()
    {
        for (int i = 0; i < 1000; i++)
        {
            Debug.Log("Job1:::" + i);
        }

    }
    private void job3()
    {
        for (int i = 0; i < 1000; i++)
        {
            Debug.Log("Job3:::" + i);
        }
    }
    private void job4()
    {
        for (int i = 0; i < 1000; i++)
        {
            Debug.Log("Job4:::" + i);
        }
    }
    private void job5()
    {
        for (int i = 0; i < 1000; i++)
        {
            Debug.Log("Job5:::" + i);
        }
    }
    private void job2()
    {
        GameObject u = new GameObject("OMIDDDD");
        omid = new List<GameObject>();
        Vector3 vector = new Vector3();
        for (int i = 0; i < 100; i++)
        {
            vector = new Vector3(0, 1 * i, 0);
            var o = Instantiate(prefab, vector, Quaternion.identity, u.transform);
            omid.Add(o);
            Debug.Log("Job2::::" + i);

        }
    }
}
