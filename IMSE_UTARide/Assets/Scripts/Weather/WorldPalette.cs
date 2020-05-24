using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weather Palette", menuName = "IMSE/WeatherPalette")]
public class WorldPalette : ScriptableObject
{
    public Gradient[] skyGradients;
    public ParticleSystem particle;


}
