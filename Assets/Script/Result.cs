using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    public TMP_Text time;
    public TMP_Text score;

    public static TMP_Text resulttime;
    public static TMP_Text resultscore;

    private void Start() {
        time.text = resulttime.text ;
        score.text = resultscore.text; 
    }
}
