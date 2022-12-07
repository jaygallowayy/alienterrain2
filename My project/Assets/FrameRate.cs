using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;

public class FrameRate : MonoBehaviour
{
    [SerializeField]
    private Text frameText;
    [SerializeField]
    private Text memoryText;
    
    
    float frameCount = 0f;
    float dt = 0.0f;
    public float fps = 0.0f;
    float updateRate = 4.0f;  // 4 updates per second

    float memoryAllocated = 0.0f;
    float memoryReserved = 0.0f;

    void Start()
    {
       
    }

    void Update()
    {
        memoryAllocated = Profiler.GetTotalAllocatedMemoryLong() / 1048576;
        memoryReserved = Profiler.GetTotalReservedMemoryLong() / 1048576;
        memoryText.text = ("Total Memory Allocated: " + memoryAllocated + "mb" + "\nTotal Memory Reserved: " + memoryReserved + "mb");

        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0f / updateRate)
        {
            fps = frameCount / dt;
            frameText.text = ("FPS: " + fps);
            frameCount = 0;
            dt -= 1.0f / updateRate;
        }
    }

}
