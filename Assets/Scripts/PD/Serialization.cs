using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SavedGame {

    public int slot = 0; // 0 means it's unused
    public int floor = 0;
    public int level = 0;
    public int dataVersion = 0;
    public bool canContinue = true;

    public float gameTime;

}

public class Serialization : MonoBehaviour {

    public static Serialization instance;

    private static PX px;


    public static GameObject[] allGameObjects;
    public static string[] allGameObjectNames;

    public static SavedGame[] savedGames;

    public static int SAVE_GAME_SLOTS = 64;

    Clock clock;



    //------------------------------------------------------------------------
    void Awake() {

        if (instance == null) {
            Debug.Log("Serialization:Awake");
            instance = this;
            savedGames = new SavedGame[SAVE_GAME_SLOTS];
            px = new PX();
            StartCoroutine(IStart());
            clock = new Clock();
        }
    }
    

    //------------------------------------------------------------------------
    IEnumerator IStart() {
        yield return null;
        ReadIndex();
        InitializeResourceArrays();
        yield return null;
        Debug.Log("Serialization IStart is now done!");

    }

    //------------------------------------------------------------------------
    void InitializeResourceArrays() {

        Debug.Log("initializing resource array");

        Debug.Log("loading all GameObject...");

        allGameObjects = Resources.LoadAll<GameObject>("");
        Debug.Log("setting up allGameObjects array: " + allGameObjects.Length);

        allGameObjectNames = new string[allGameObjects.Length];

        for (int i = 0; i < allGameObjectNames.Length; i++) {
            allGameObjectNames[i] = allGameObjects[i].name;
        }

    }


    //------------------------------------------------------------------------
    void PrintPD(ScriptableObject pd) {

        Debug.Log("VERIFYING ----------------------------------------");

        Debug.Log(">>> " + pd.GetType());
        foreach (var field in pd.GetType().GetFields()) {
            Debug.Log(field.Name + " " + field.GetValue(pd));
        }
    }


    //------------------------------------------------------------------------
    public string FormatGameTime(float t) {

        int hours = Mathf.FloorToInt(t / 3600);
        int minutes = Mathf.FloorToInt(t / 60);
        int seconds = (int)t % 60;

        return hours + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    //------------------------------------------------------------------------
    public bool CanLoadGame()
    {
        ReadIndex();
        if (savedGames[1] != null) return true;
        return false;
    }

    //------------------------------------------------------------------------
    public string GetSlotDisplayString(int slot) {

        if (slot >= SAVE_GAME_SLOTS) return null;

        SavedGame sg = savedGames[slot];

        if (sg == null) {
            return null;
        }

        string s = "";

       // s = Global.FormatCharacterClass(sg.characterClass) + " - Floor " + sg.floor + " - " + FormatGameTime(sg.gameTime);

        return s;
    }

    //------------------------------------------------------------------------
    public string GetFullPathName() {
        return Application.persistentDataPath + "/SaveGame" + Global.saveGameSlot + ".dat";
    }

    //------------------------------------------------------------------------
    public string GetIndexPathName() {
        return Application.persistentDataPath + "/Index.dat";
    }


    //------------------------------------------------------------------------
    public void WriteIndex() {


        if (Global.saveGameSlot >= SAVE_GAME_SLOTS) return;

        SavedGame sg = new SavedGame();

        sg.slot = Global.saveGameSlot;
        sg.dataVersion = GameData.DATA_VERSION;
        sg.gameTime = GameData.instance.gameTime;
        sg.canContinue = GameData.instance.canContinue;

        savedGames[Global.saveGameSlot] = sg;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GetIndexPathName()); 
        bf.Serialize(file, savedGames);
        file.Close();

    }

    //------------------------------------------------------------------------
    public void ReadIndex() {
        Debug.Log("Serialization:ReadIndex");

        if (File.Exists(GetIndexPathName())) {
            BinaryFormatter bf = new BinaryFormatter();
            Debug.Log("opening index...");
            FileStream file = File.Open(GetIndexPathName(), FileMode.Open);
            savedGames = (SavedGame[])bf.Deserialize(file);
            Debug.Log("closing index...");

            file.Close();
        }


        Debug.Log("loading save game slots");

        for (int i = 0; i < SAVE_GAME_SLOTS; i++) {
            SavedGame sg = savedGames[i];

            if (sg!= null) {
                Debug.Log("slot loaded");

                if (sg.dataVersion != GameData.DATA_VERSION ||
                    sg.canContinue == false) {
                    savedGames[i] = null;
                }

            }

        }

        Debug.Log("Done reading index");
    }

    //------------------------------------------------------------------------
    public IEnumerator ISaveData(List<PD> data, string fullPathName)
    {
        clock.Start("ISave");
        BinaryFormatter bf = new BinaryFormatter();
        Debug.Log("Saving to " + fullPathName);

        FileStream file = File.Create(fullPathName);
        foreach (PD pd in data)
        {
            px.pd = pd;
            bf.Serialize(file, px);
            if (clock.FrameHasElapsed()) yield return new WaitForFixedUpdate();
        }
        file.Close();

        Debug.Log("gave saved");

        yield return null;
    }

    //------------------------------------------------------------------------
    public IEnumerator ILoadData(string fullPathName)
    {

        clock.Start("ILoad");

        Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Loading: " + fullPathName);

        if (File.Exists(fullPathName))
        {

            Debug.Log("file exists...");

            GameData.data.Clear();

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(fullPathName, FileMode.Open);
            while (file.Position != file.Length)
            {
                PX data = (PX)bf.Deserialize(file);

                World.staticObjects.Add((PD)(data.pd));
                //GameData.data.Add((PD)(data.pd));
                if (clock.FrameHasElapsed()) yield return new WaitForFixedUpdate();

            }
            file.Close();

            Debug.Log("done loading!");

        }

        yield return null;

    }

    //------------------------------------------------------------------------
    public IEnumerator ISave() {
        clock.Start("ISave");
        BinaryFormatter bf = new BinaryFormatter();

        WriteIndex();

        string fullPathName = GetFullPathName();

        Debug.Log("Saving to "+ fullPathName);

        FileStream file = File.Create(fullPathName);
        foreach(PD pd in GameData.data) {
            px.pd = pd;
            bf.Serialize(file, px);
            if (clock.FrameHasElapsed()) yield return new WaitForFixedUpdate();
        }
        file.Close();

        Debug.Log("gave saved");

        yield return null;
    }


    //------------------------------------------------------------------------
    public IEnumerator ILoad() {

        clock.Start("ILoad");

        string fullPathName = GetFullPathName();

        Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Loading: " + fullPathName);

        if (File.Exists(fullPathName)) {

            Debug.Log("file exists...");

            GameData.data.Clear();

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(fullPathName, FileMode.Open);
            while (file.Position != file.Length) {
                PX data = (PX)bf.Deserialize(file);
                GameData.data.Add((PD)(data.pd));
                if (clock.FrameHasElapsed()) yield return new WaitForFixedUpdate();

            }
            file.Close();

            Debug.Log("done loading!");

        }

        yield return null;

    }


}
