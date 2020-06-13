/* Author: Jonah Bui
 * Contributors:
 * Date: June 12, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: Used to define the properties of a vehicle for identification.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum VehicleType
{ 
    Sedan,
    Van
};

public enum VehicleColor
{ 
    Red,
    Blue,
    Yellow,
    Green,
    White,
    Black,
    Gray,
    Orange,
    Brown,
    Purple
};

[CreateAssetMenu(fileName = "New Vehicle Info", menuName = "IMSE/VehicleInfo")]
public class VehicleInfo : ScriptableObject
{
    public VehicleType vehicleType;
    public VehicleColor vehicleColor;
    public string licensePlate;
    public Image companyLogo;
}
