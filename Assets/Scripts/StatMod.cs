using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public delegate bool EffectDelegate(PActor effectActor = null);
public delegate bool FinishedDelegate(PActor effectActor = null);
public delegate void OnHitDelegate(PActor sourceActor, PActor targetActor);


[System.Serializable]
public class StatMod {

	public enum Type {
		INDEFINITE,
		TIMED,
		TIMED_DELEGATE,
        FINISHED_DELEGATE
	}

	public int moveSpeed = 0;
	public int attackSpeed = 0;

	public bool permanent = false;

	public int str = 0;
	public int dex = 0;
	public int mind = 0;
	public int shadow = 0;

	public int maxHealth = 0;
	public int maxMana = 0;
	public int healthRegen = 0;
	public int manaRegen = 0;

	public int armorBonus = 0;
	public int attackBonus = 0;

    public float physicalDM = 1.0f;
    public float arcaneDM = 1.0f;
    public float fireDM = 1.0f;
    public float poisonDM = 1.0f;

    public bool burning = false;
	public bool scared = false;
	public bool charmed = false;
	public bool planar = false;
	public bool paralyzed = false;
	public bool frenzy = false;
	public bool confused = false;
	public bool blinded = false;
	public bool invisible = false;
    public bool poisoned = false;
    public bool pariah = false;
    public bool silenced = false;
    public bool darkness = false;

    [NonSerialized]
	public EffectDelegate effectDelegate = null;
    [NonSerialized]
    public OnHitDelegate onHitDelegate = null;
    [NonSerialized]
    public FinishedDelegate finishedDelegate = null;

    public float timeLeft = -1.0f;
	public float interval = 0.5f;
	public float pulse = 0.0f;

    [NonSerialized]
    public GameObject vfx = null;

	public Type type = Type.INDEFINITE;

	public StatMod() {}


	public static StatMod operator +(StatMod m1, StatMod m2) {

		StatMod m = new StatMod();

	    m.moveSpeed 	= m1.moveSpeed 		+ m2.moveSpeed;
	    m.attackSpeed	= m1.attackSpeed 	+ m2.attackSpeed;

	    m.str 			= m1.str 			+ m2.str;
	    m.dex 			= m1.dex 			+ m2.dex;
	    m.mind 			= m1.mind 			+ m2.mind;
	    m.shadow 	    = m1.shadow 	    + m2.shadow;

	    m.maxHealth 	= m1.maxHealth 		+ m2.maxHealth;
	    m.maxMana 		= m1.maxMana 		+ m2.maxMana;
	    m.healthRegen 	= m1.healthRegen 	+ m2.healthRegen;
	    m.manaRegen 	= m1.manaRegen 		+ m2.manaRegen;

	    m.armorBonus	= m1.armorBonus 	+ m2.armorBonus;
	    m.attackBonus	= m1.attackBonus 	+ m2.attackBonus;

        m.physicalDM    = m1.physicalDM     * m2.physicalDM;
        m.arcaneDM      = m1.arcaneDM       * m2.arcaneDM;
        m.fireDM        = m1.fireDM         * m2.fireDM;
        m.poisonDM      = m1.poisonDM       * m2.poisonDM;

        m.charmed		= m1.charmed		|| m2.charmed;
	    m.scared		= m1.scared			|| m2.scared;
	    m.burning		= m1.burning		|| m2.burning;
        m.poisoned      = m1.poisoned       || m2.poisoned;

	    m.planar		= m1.planar			|| m2.planar;
	    m.paralyzed		= m1.paralyzed		|| m2.paralyzed;
	    m.frenzy		= m1.frenzy 		|| m2.frenzy; 
	    m.confused		= m1.confused		|| m2.confused;
	    m.blinded		= m1.blinded		|| m2.blinded;
	    m.invisible		= m1.invisible		|| m2.invisible;
        m.pariah        = m1.pariah         || m2.pariah;
        m.silenced      = m1.silenced       || m2.silenced;
        m.darkness      = m1.darkness       || m2.darkness;

        return m;
	}	

}



