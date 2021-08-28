using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarkType : MonoBehaviour
{
    public enum QType
    {
        Up,
        Down
    }

    [SerializeField] private QType type;

    public QType qType => type;
}
