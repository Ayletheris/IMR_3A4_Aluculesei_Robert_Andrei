using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    int scor = 0;
    int lovituri = 0;

    void Start()
    {
        scoreText.text = "Scor: 0\nTine trigger, roteste toiagul, da drumul";
    }

    public void AdaugaPuncte(int puncte)
    {
        scor = scor + puncte;
        lovituri = lovituri + 1;
        scoreText.text = "Scor: " + scor + "\nLovituri: " + lovituri + " | ultima: +" + puncte;
    }
}
