using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public GameObject prop;

    public PlayerManager pm;
    private void Awake()
    {
        pm = PlayerManager.Instance;
        if (prop == null)
            prop = this.gameObject.transform.GetChild(0).gameObject;
        prop.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (pm.propBlockades)
            SetProp();
    }

    public void SetProp()
    {
        prop.SetActive(true);
    }
}
