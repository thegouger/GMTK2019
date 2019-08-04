using System;
using System.Collections.Generic;
public class GlobalState {

    public static float currentBattery = 50f;
    public static bool[] generators = {false, false, false, false, false};

    public void Reset() {
        currentBattery = 100f;
        for (int i = 0; i < 5; i++) {
            generators[i] = false;
        }
    }
}
