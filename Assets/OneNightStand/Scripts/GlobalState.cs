using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState {

    public static float currentBattery = 100f;
    public static bool[] generators = {false, false, false, false, false};
    public static int killCount = 0;

    public static int spawnMax = 10;
    public static int currentSpawn = 0;

    public static void Reset() {
        killCount = 0;
        currentBattery = 100f;
        for (int i = 0; i < 5; i++) {
            generators[i] = false;
        }
        Time.timeScale = 1;
    }
}
