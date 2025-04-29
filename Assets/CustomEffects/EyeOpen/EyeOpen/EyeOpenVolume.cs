using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenu("Custom/EyeOpen")]
public class EyeOpen : VolumeComponent, IPostProcessComponent
{
    public ClampedFloatParameter _Strength = new ClampedFloatParameter(1f, 0f, 1f, true);
    public ClampedFloatParameter _Width = new ClampedFloatParameter(1.5f, 1f, 3f, true);
    public ClampedFloatParameter _EdgeWidth = new ClampedFloatParameter(0.1f, 0.01f, 0.5f, true);

    public void load(Material material)
    {
        material.SetFloat("_Strength", _Strength.value);
        material.SetFloat("_Width", _Width.value);
        material.SetFloat("_EdgeWidth", _EdgeWidth.value);
    }

    public bool IsActive() => true;
    public bool IsTileCompatible() => false;
}
