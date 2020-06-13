/* Author: Jonah Bui
 * Contributors:
 * Date: June 7, 2020
 * ------------------------------------------------------------------------------------------------
 * Purpose: 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IMSE/Slide Color Scheme")]
public class SlideColorScheme : ScriptableObject
{
    public Color mainColor = new Color(1f, 1f, 1f, 1f);
    public Color accentColor = new Color(1f, 1f, 1f, 1f);
    public Color textColor = new Color(1f, 1f, 1f, 1f);

    public int fontSize = 64;
    public FontStyle fontStyle = FontStyle.Normal;
}
