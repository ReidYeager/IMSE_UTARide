/* Author: Jonah Bui
 * Contributors:
 * Date: March 27, 2020
 * ------------------------
 * Purpose: Bindings for each of the possible inputs. Easy to change binds in a centralized file
 * Changelog:
 */
using UnityEngine;

public class UI_InputMapping 
{
    // Keyboard and Mouse Bindings
    public static KeyCode ToggleMenuVisibility = KeyCode.Tab;

    public static float VerticalInput = Input.GetAxis("Vertical");
    public static float HorizontalInput = Input.GetAxis("Horizontal");

    // Oculus Mappings
    public static OVRInput.Button OVR_ToggleMenuVisibilty = OVRInput.Button.Start;
    public static OVRInput.Button OVR_A = OVRInput.Button.One;
    public static OVRInput.Button OVR_B = OVRInput.Button.Two;
    public static OVRInput.Button OVR_X = OVRInput.Button.Three;
    public static OVRInput.Button OVR_Y = OVRInput.Button.Four;
    public static OVRInput.Button OVR_Start = OVRInput.Button.Start;

    public static OVRInput.Axis1D OVR_LeftIndexTrigger = OVRInput.Axis1D.PrimaryIndexTrigger;
    public static OVRInput.Axis1D OVR_RightIndexTrigger = OVRInput.Axis1D.SecondaryIndexTrigger;
    public static OVRInput.Axis1D OVR_LeftHandTrigger = OVRInput.Axis1D.PrimaryHandTrigger;
    public static OVRInput.Axis1D OVR_RightHandTrigger = OVRInput.Axis1D.SecondaryHandTrigger;

    public static OVRInput.Axis2D OVR_LeftJoyStick = OVRInput.Axis2D.PrimaryThumbstick;
    public static OVRInput.Axis2D OVR_RightJoyStick = OVRInput.Axis2D.SecondaryThumbstick;
}
