using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;

public class Clock {

    private string s;
    private int lap;

    private long startTime;

    public bool running = false;

    public Clock() {}

    public void Start(string label) {
        s = label;
        Start();
    }

    public void Start() {
        running = true;
        lap = 0;
        startTime = ms();
    }

    public long ms() {
        return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }

    public bool FrameHasElapsed() {
        long milliseconds = ms();

        long delta = milliseconds - startTime;
        if (delta>32 ) {
            startTime = milliseconds;
            return true;
        }
        return false;
    }

    public long ElapsedMilliseconds() {
        return ms() - startTime;
    }

    public void Stop() {
        running = false;
        //Debug.Log("STOP: " + s + " >> " + ElapsedMilliseconds());
    }

    public void Lap() {
        //Debug.Log("LAP " + lap + ": " + s + " >> " + ElapsedMilliseconds());
        lap++;
        startTime = ms();
    }

}
