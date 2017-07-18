using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;


public class ItemUtility  {

    [MenuItem("Assets/Initialize Abilities")]
    static public void InitializeAbilities()
    {
        System.Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
        System.Type[] possible = (from System.Type type in types where type.IsSubclassOf(typeof(BaseAbility)) select type).ToArray();

        Debug.Log("startup");
        foreach (System.Type t in possible)
        {
            Debug.Log(t);
            var o = ScriptableObjectUtility.CreateFromAsset(ScriptableObject.CreateInstance(t));
            Debug.Log(AssetDatabase.GetAssetPath(o));
        }
    }



    [MenuItem("Assets/Create/PD/PStaticZone")]
    static public void CreatePStaticZone()
    {
        ScriptableObjectUtility.CreateAsset<PStaticZone>();
    }

    [MenuItem("Assets/Create/PD/PDynamicZone")]
    static public void CreatePDynamicZone()
    {
        ScriptableObjectUtility.CreateAsset<PDynamicZone>();
    }

    [MenuItem("Assets/Create/PD/PActor")]
    static public void CreatePActor()
    {
        ScriptableObjectUtility.CreateAsset<PActor>();
    }

    [MenuItem("Assets/Create/PD/Equipment/PHelmet")]
    static public void CreatePHelmet()
    {
        ScriptableObjectUtility.CreateAsset<PHelmet>();
    }

    [MenuItem("Assets/Create/PD/Equipment/PMask")]
    static public void CreatePMask()
    {
        ScriptableObjectUtility.CreateAsset<PMask>();
    }

    [MenuItem("Assets/Create/PD/Equipment/PHat")]
    static public void CreatePHat()
    {
        ScriptableObjectUtility.CreateAsset<PHat>();
    }

    [MenuItem("Assets/Create/PD/Equipment/PAmulet")]
    static public void CreatePAmulet()
    {
        ScriptableObjectUtility.CreateAsset<PAmulet>();
    }

    [MenuItem("Assets/Create/PD/Equipment/PRing")]
    static public void CreatePRing()
    {
        ScriptableObjectUtility.CreateAsset<PRing>();
    }


    [MenuItem("Assets/Create/PD/Equipment/PArmor")]
    static public void CreatePArmor()
    {
        ScriptableObjectUtility.CreateAsset<PArmor>();
    }

    [MenuItem("Assets/Create/PD/Equipment/PRobe")]
    static public void CreatePRobe()
    {
        ScriptableObjectUtility.CreateAsset<PRobe>();
    }

    [MenuItem("Assets/Create/PD/Equipment/PCloak")]
    static public void CreatePCloak()
    {
        ScriptableObjectUtility.CreateAsset<PCloak>();
    }

    [MenuItem("Assets/Create/PD/Equipment/PLeggings")]
    static public void CreatePLeggings()
    {
        ScriptableObjectUtility.CreateAsset<PLeggings>();
    }

    [MenuItem("Assets/Create/PD/Equipment/PBoots")]
    static public void CreatePBoots()
    {
        ScriptableObjectUtility.CreateAsset<PBoots>();
    }

    [MenuItem("Assets/Create/PD/Equipment/PGloves")]
    static public void CreatePGloves()
    {
        ScriptableObjectUtility.CreateAsset<PGloves>();
    }

    [MenuItem("Assets/Create/PD/Equipment/PBracers")]
    static public void CreatePBracers()
    {
        ScriptableObjectUtility.CreateAsset<PBracers>();
    }

    [MenuItem("Assets/Create/PD/Equipment/PBracelet")]
    static public void CreatePBracelet()
    {
        ScriptableObjectUtility.CreateAsset<PBracelet>();
    }

    [MenuItem("Assets/Create/PD/Weapons/PWeapon")]
    static public void CreatePWeapon()
    {
        ScriptableObjectUtility.CreateAsset<PWeapon>();
    }

    [MenuItem("Assets/Create/PD/Weapons/PStaff")]
    static public void CreatePStaff()
    {
        ScriptableObjectUtility.CreateAsset<PStaff>();
    }

    [MenuItem("Assets/Create/PD/Equipment/PShield")]
    static public void CreatePShield()
    {
        ScriptableObjectUtility.CreateAsset<PShield>();
    }



}

