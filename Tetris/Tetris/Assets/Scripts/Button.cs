using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void PlayAgain() {
        Application.LoadLevel("Level");
    }
}
