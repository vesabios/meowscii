using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Location : ScriptableObject {

    public List<byte> globalTransits = new List<byte>();
    List<byte> othertransits = new List<byte>();

    /*
     * locations need:
     * 
     * - a referencable way for the ink-script to activate it
     * 
     * - at least 1 global transit reference
     * 
     * - a map that can be pre-processed, to identify other transits
     * 
     * 
     */

    public void Init()
    {

    }

}
