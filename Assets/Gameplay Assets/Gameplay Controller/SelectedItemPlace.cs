using System;
using UnityEngine;

[Serializable]
public struct SelectedItemPlace
{
    [SerializeField] private Transform pos;

    [HideInInspector] public bool Used;

    public Vector3 GetPos { get => pos.position; }
}
