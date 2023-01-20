using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    private PlayerEntity player;
    //public static TimeManager Instance { get => instance; set => instance = value; }

    public TimeManager(PlayerEntity playerEntity) 
    {
        this.player = playerEntity;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public void StartTimer(float time)
    {
        time += Time.deltaTime;
    }

    public void StartCountDown(float time)
    {
        time -= Time.deltaTime;
        if(time <= 0) 
        {
            time = 0;
        }
    }

    public void ResetTimer(float time)
    {
        time = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
