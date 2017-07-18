using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Substances
{
    public class SubstanceProvider : MonoBehaviour
    {

        public static SubstanceProvider instance;

        static Dictionary<SubstanceType, BaseSubstance> substances;

        public void Awake()
        {

            substances = new Dictionary<SubstanceType, BaseSubstance>();
            var substanceNames = Enum.GetNames(typeof(SubstanceType));

            for (int i = 0; i < substanceNames.Length; i++)
            {
                System.Type t = System.Type.GetType("Substances." + substanceNames[i]);
                if (t != null)
                {

                    var bs = gameObject.AddComponent( t );

                    substances.Add((SubstanceType)i, (BaseSubstance)bs);
                }
                else
                {
                    Debug.LogError("CLASS NOT FOUND: " + substanceNames[i]);
                }

            }
        }

        public static BaseSubstance Get(SubstanceType t)
        {
            return substances[t];
        }

    }
}