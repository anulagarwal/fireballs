using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallModifier : MonoBehaviour
{
    public enum MODIFIER_TYPE
    {
        ADDER,
        MULTIPLIER,
        SUBTRACTOR,
        DIVIDER
    }

    [SerializeField]
    public MODIFIER_TYPE currentModifier = MODIFIER_TYPE.ADDER;
    void Start()
    {
        
    }
}
