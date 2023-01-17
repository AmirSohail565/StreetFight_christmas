using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingSoonHandle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject comingSoonPanel;
    void Start()
    {
        
    }
    public void TurnOnComingSoonPanel()
    {
        comingSoonPanel.SetActive(true);
    }
    public void TurnOffComingSoonPanel()
    {
        comingSoonPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
