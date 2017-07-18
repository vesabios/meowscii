using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public class GameData : MonoBehaviour {

    public static int DATA_VERSION = 1;

    public static GameData instance;
    public PGameProperties gameProperties;
   
    public static List<PD> data;



    //------------------------------------------------------------------------
    void Awake() {
        if (instance==null) instance = this;
    }

    //------------------------------------------------------------------------
    void Start() {




       
    }

    //------------------------------------------------------------------------
    void OnDestroy() {

    }


    public static void CreateNewGame()
    {
        ClearAllData();

        World.GenerateNewWorld();
    }

  

    //------------------------------------------------------------------------
    public IEnumerator ILoadGame() {

        ClearAllData();

        //current savegame slot is assigned to Global.saveGameSlot;
        // this will have been set by a menu somewhere, but defaults to 0

        yield return StartCoroutine(ILoadData());

        
    }



    //------------------------------------------------------------------------
    public IEnumerator IInitGameProperties() {

        gameProperties = ScriptableObject.CreateInstance<PGameProperties>() as PGameProperties;

        deaths = 0;

        if (floorSeeds != null) {
            for (int i = 0; i < 50; i++) {
                floorSeeds.Add(Random.Range(0, 200));
                floorGenerated.Add(false);

            }
        }

        data.Add(gameProperties);

        yield return null;
    }






    //------------------------------------------------------------------------
    public IEnumerator ISaveData() {
        yield return StartCoroutine(Serialization.instance.ISave());
    }

    //------------------------------------------------------------------------
    public IEnumerator ILoadData() {
        yield return StartCoroutine(Serialization.instance.ILoad());
    }






    // UTILITY METHODS 


    //------------------------------------------------------------------------
    public static void ClearAllData() {

        if (data != null) {
            foreach (PD pd in data) {
                Destroy(pd);
            }
            data.Clear();
        } else {
            data = new List<PD>();
        }

    }


    public static List<PD> GetObjectsInZone(System.Guid id)
    {
        List<PD> results = new List<PD>();
        foreach(PD pd in data)
        {
            if (pd.GetType().IsSubclassOf(typeof(PWorldObject)))
            {
                if (((PWorldObject)pd).zoneID == id)
                    results.Add(pd);
            }
               
        }
        return data;
    }



    // GETTERS AND SETTERS FOR PERSISTENT GAME PROPERTIES 

    //------------------------------------------------------------------------
    public bool canContinue
    {
        get
        {
            if (gameProperties == null) return true;
            return gameProperties.canContinue;
        }
        set
        {
            if (gameProperties != null)
            {
                if (gameProperties != null)
                {
                    gameProperties.canContinue = value;
                }
            };
        }
    }



    //------------------------------------------------------------------------
    public float gameTime {
        get {
            if (gameProperties == null) return 0;
            return gameProperties.gameTime;
        }
        set {
            if (gameProperties != null)
            {
                gameProperties.gameTime = value;
            };
        }
    }

    //------------------------------------------------------------------------
    public int deaths {
        get {
            if (gameProperties == null) return 0;
            return gameProperties.deaths;
        }
        set {
            gameProperties.deaths = value;
        }
    }



    //------------------------------------------------------------------------
    public List<int> floorSeeds {
        get {
            if (gameProperties == null) return null;
            return gameProperties.floorSeeds;
        }
        set {
            gameProperties.floorSeeds = value;
        }
    }


    //------------------------------------------------------------------------
    public List<bool> floorGenerated {
        get {
            if (gameProperties == null) return null;
            return gameProperties.floorGenerated;
        }
        set {
            gameProperties.floorGenerated = value;
        }
    }




    //------------------------------------------------------------------------
    public static PActor AddPActor(PActor actor)
    {
        if (actor == null) return null;
        data.Add(actor);
        return actor;
    }

    public static PItem AddPItem(PItem item)
    {
        if (item == null) return null;
        data.Add(item);
        Debug.Log("ADDING ITEM: " + item);
        return item;
    }

    public static PZone AddPZone(PZone zone)
    {
        if (zone == null) return null;
        data.Add(zone);
        return zone;
    }


}
