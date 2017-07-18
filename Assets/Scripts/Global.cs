using UnityEngine;
using System.Collections.Generic;


public struct StartingAttributes {

    public int strength;
    public int dexterity;
    public int mind;
    public int shadow;
    public int speed;

    public string leftHandItemName;
    public string rightHandItemName;
    
}

public class Global : MonoBehaviour {



    public enum SceneMode {
        NULL,
        GAMEPLAY,
        INTERSTITIAL,
        CONTINUE
    }

    public enum GameMode
    {
        NORMAL,
        TUTORIAL
    }


    public enum CharacterClass {
        FIGHTER,
        ROGUE,
        WIZARD,
        CLERIC,
        DRUID,
        BARBARIAN
    }

    public enum Difficulty {
        EASY,
        NORMAL,
        HEROIC
    }

    public static Global instance;
    public static bool fullscreen = false;
    public static int resolutionWidth = 1920;
    public static int resolutionHeight = 1080;

    public static bool continueGame = false;
    public static int saveGameSlot = 0;


    public static GameMode gameMode = GameMode.NORMAL;

    public static SceneMode sceneMode = SceneMode.NULL;

    public static Difficulty difficulty = Difficulty.NORMAL;

    public static CharacterClass characterClass = CharacterClass.WIZARD;


    private static Dictionary<CharacterClass, StartingAttributes> startingAttributes;


    void Awake() {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
            InitDisplaySettings();
            InitStartingAttributes();

        } else {
            Destroy(transform.gameObject);
        }


    }

    static void InitDisplaySettings()
    {

        if (PlayerPrefs.HasKey("fullscreen"))
        {
            fullscreen = PlayerPrefs.GetInt("fullscreen") == 1;
        }

        if (PlayerPrefs.HasKey("resolutionWidth"))
        {
            resolutionWidth = PlayerPrefs.GetInt("resolutionWidth");
        }

        if (PlayerPrefs.HasKey("resolutionHeight"))
        {
            resolutionHeight = PlayerPrefs.GetInt("resolutionHeight");
        }

        ApplyDisplayChanges();
    }

    static void InitStartingAttributes() {

        startingAttributes = new Dictionary<CharacterClass, StartingAttributes>();

        StartingAttributes fighter;
        StartingAttributes rogue;
        StartingAttributes wizard;
        StartingAttributes cleric;
        StartingAttributes druid;
        StartingAttributes barbarian;

        fighter.strength = 16;
        fighter.dexterity = 12;
        fighter.mind = 10;
        fighter.shadow = 3;
        fighter.speed = 28;

        fighter.leftHandItemName = "WeaponFocus";
        fighter.rightHandItemName = "Sword";
        
        rogue.strength = 12;
        rogue.dexterity = 14;
        rogue.mind = 12;
        rogue.shadow = 6;
        rogue.speed = 32;

        rogue.leftHandItemName = "Bow";
        rogue.rightHandItemName = "ShortSword";
        
        wizard.strength = 14;
        wizard.dexterity = 12;
        wizard.mind = 16;
        wizard.shadow = 2;
        wizard.speed = 30;

        wizard.leftHandItemName = "MinorArcana";
        wizard.rightHandItemName = "LongSword";

        cleric.strength = 14;
        cleric.dexterity = 14;
        cleric.mind = 14;
        cleric.shadow = 4;
        cleric.speed = 30;

        cleric.leftHandItemName = "DivineHealing";
        cleric.rightHandItemName = "Staff";


        druid.strength = 14;
        druid.dexterity = 14;
        druid.mind = 14;
        druid.shadow = 4;
        druid.speed = 30;

        druid.leftHandItemName = "MeatOfSummoning";
        druid.rightHandItemName = "Falchion";

        barbarian.strength = 16;
        barbarian.dexterity = 12;
        barbarian.mind = 10;
        barbarian.shadow = -1;
        barbarian.speed = 32;

        // barbarian.leftHandItemName = "OffHandDagger";
        barbarian.leftHandItemName = "Punch";

        barbarian.rightHandItemName = "Sword";

        startingAttributes.Add(CharacterClass.FIGHTER, fighter);
        startingAttributes.Add(CharacterClass.ROGUE, rogue);
        startingAttributes.Add(CharacterClass.WIZARD, wizard);
        startingAttributes.Add(CharacterClass.CLERIC, cleric);
        startingAttributes.Add(CharacterClass.DRUID, druid);
        startingAttributes.Add(CharacterClass.BARBARIAN, barbarian);

    }


    public static StartingAttributes GetStartingAttributes() {
        return startingAttributes[characterClass];
    }


    public void OnLevelWasLoaded(int level) {

    }

    public static void SetDifficulty(Difficulty d) {
        difficulty = d;
        Messenger.Broadcast("NotifyNewDifficulty");

    }


    public static void ApplyDisplayChanges()
    {

        //Screen.SetResolution(resolutionWidth, resolutionHeight, fullscreen);

        PlayerPrefs.SetInt("fullscreen", fullscreen ? 1 : 0);
        PlayerPrefs.SetInt("resolutionWidth", resolutionWidth);
        PlayerPrefs.SetInt("resolutionHeight", resolutionHeight);

        PlayerPrefs.Save();

    }

    public static void SetFullscreen(bool b)
    {
        fullscreen = b;
    }

    public static void SetResolution(int w, int h)
    {
        resolutionWidth = w;
        resolutionHeight = h;
    }

  


    public static string FormatCharacterClass(Global.CharacterClass c) {

        switch (c) {

            case Global.CharacterClass.WIZARD: {
                    return "Wizard";
                }
                break;
            case Global.CharacterClass.FIGHTER: {
                    return "Fighter";
                }
                break;
            case Global.CharacterClass.DRUID: {
                    return "Druid";
                }
                break;
            case Global.CharacterClass.ROGUE: {
                    return "Rogue";
                }
                break;
            case Global.CharacterClass.CLERIC: {
                    return "Cleric";
                }
                break;
            case Global.CharacterClass.BARBARIAN:
                {
                    return "Barbarian";
                }
                break;
            default:
                break;
        }

        return "Player";
    }


    public static string FormatDifficulty(Global.Difficulty d) {

        switch (d) {

            case Global.Difficulty.EASY: {
                    return "Easy";
                }
                break;
            case Global.Difficulty.NORMAL: {
                    return "Normal";
                }
                break;
            case Global.Difficulty.HEROIC: {
                    return "Heroic";
                }
                break;

            default:
                break;
        }

        return "Meh";
    }

}
