using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleType : MonoBehaviour
{
    public enum PType
    {
        Quark,
        Electron,
        Gluon
    }

    [SerializeField] private PType type;

    public PType pType => type;
}
