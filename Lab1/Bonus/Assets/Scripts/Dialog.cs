using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Dialog : MonoBehaviour
{
    public ImageTargetBehaviour carte1;
    public ImageTargetBehaviour carte2;
    public Canvas bula1;
    public Canvas bula2;
    public Text text1;
    public Text text2;
    public float distanta = 0.25f;
    public float plecare = 0.40f;
    public float pauza = 4f;

    string state = "idle";
    bool wasVisible1;
    bool wasVisible2;
    bool tauntedEmet;
    bool tauntedElidibus;
    float timp;
    int poveste;
    int replica;
    int liniste;
    Vector3 pozitie1;
    Vector3 pozitie2;
    float distantaVeche;

    string[] liniste1 = {
        "At last, a moment\nwithout a sermon.",
        "Amaurot deserved better\nthan another empty stage.",
        "Even death cannot cure\nan excess of company.",
        "I had hoped silence\nmight improve the company."
    };

    string[] liniste2 = {
        "Our people are waiting.\nI cannot stop here.",
        "The duty remains.\nEven if I stand alone.",
        "I must remember\nwhy I began.",
        "No more borrowed promises.\nI will see this through."
    };

    string[] emet = {
        "Another borrowed hero?\nYour wardrobe grows ambitious.",
        "A mask is no conviction.\nYou were never this hollow.",
        "I remember the boy beneath it.\nHe would hate this performance.",

        "You cling to duty.\nCan you still name its dead?",
        "No. They are ours.\nYet you have misplaced them.",
        "A perfect answer.\nFrom a man avoiding the question.",

        "Our grand design failed.\nMust you be its loudest echo?",
        "I tested their resolve.\nYou merely borrowed a champion.",
        "You mistake noise for resolve.\nI thought better of you."
    };

    string[] elidibus = {
        "You lost to one.\nYour advice is worth little.",
        "Conviction did not save you.\nI will finish what you could not.",
        "Then stop mourning him.\nStand aside.",

        "Their names are not yours\nto turn into a jest.",
        "I have not forgotten\nwhat must be restored.",
        "Better a purpose than\nanother of your bitter elegies.",

        "Your surrender was yours.\nDo not mistake it for mine.",
        "And your little theatre\nleft our people waiting.",
        "Then keep your judgment.\nI still have work to do."
    };

    string[] taunt1 = {
        "Off to find another costume?\nCome back and finish your sermon.",
        "Do return, Elidibus.\nYour dramatic exit needs work."
    };

    string[] taunt2 = {
        "Leaving again, Emet-Selch?\nReturn and face your failure.",
        "Come back, Emet-Selch.\nYour silence settles nothing."
    };

    string[] raspuns1 = {
        "You called for me?\nHow touching. Miss my company?",
        "Next time, spare me the summons.\nI am not your errand boy.",

        "You returned so promptly.\nDo try to look less flattered.",
        "An excellent memory for slights.\nNow put it to better use."
    };

    string[] raspuns2 = {
        "I called for an answer.\nYour vanity supplied the rest.",
        "Then spare me your retreat.\nWe still have unfinished work.",

        "You called me back to mock me?\nYou have learned nothing.",
        "I remember every word.\nLet us see you defend them."
    };

    void Update()
    {
        bool vazuta1;
        bool vazuta2;

        if (carte1.TargetStatus.Status == Status.TRACKED)
        {
            vazuta1 = true;
        }
        else
        {
            vazuta1 = false;
        }

        if (carte2.TargetStatus.Status == Status.TRACKED)
        {
            vazuta2 = true;
        }
        else
        {
            vazuta2 = false;
        }

        bula1.gameObject.SetActive(vazuta1);
        bula2.gameObject.SetActive(vazuta2);

        if (wasVisible1 && !vazuta1 && vazuta2)
        {
            tauntedEmet = true;
        }
        if (wasVisible2 && !vazuta2 && vazuta1)
        {
            tauntedElidibus = true;
        }
        float distantaCarti = Vector3.Distance(carte1.transform.position, carte2.transform.position);

        if (wasVisible1 && wasVisible2 && vazuta1 && vazuta2
            && distantaVeche <= plecare && distantaCarti > plecare)
        {
            float miscare1 = Vector3.Distance(carte1.transform.position, pozitie1);
            float miscare2 = Vector3.Distance(carte2.transform.position, pozitie2);
            if (miscare1 >= miscare2)
            {
                tauntedEmet = true;
            }
            else
            {
                tauntedElidibus = true;
            }
        }

        wasVisible1 = vazuta1;
        wasVisible2 = vazuta2;
        pozitie1 = carte1.transform.position;
        pozitie2 = carte2.transform.position;
        distantaVeche = distantaCarti;

        string nextState = "idle";

        if (vazuta1 && vazuta2)
        {
            if (tauntedEmet && distantaCarti <= distanta)
            {
                nextState = "returnEmet";
            }
            else if (tauntedElidibus && distantaCarti <= distanta)
            {
                nextState = "returnElidibus";
            }
            else if (tauntedEmet)
            {
                nextState = "tauntEmet";
            }
            else if (tauntedElidibus)
            {
                nextState = "tauntElidibus";
            }
            else if (distantaCarti <= distanta)
            {
                nextState = "bickering";
            }
        }
        else if (vazuta1 && tauntedElidibus)
        {
            nextState = "tauntElidibus";
        }
        else if (vazuta2 && tauntedEmet)
        {
            nextState = "tauntEmet";
        }

        if (nextState != state)
        {
            state = nextState;
            replica = 0;
            poveste = Random.Range(0, emet.Length / 3) * 3;
            timp = 0;
        }

        timp -= Time.deltaTime;
        if (timp > 0)
        {
            return;
        }

        if (state == "bickering")
        {
            text1.text = "Emet-Selch\n" + emet[poveste + replica];
            text2.text = "Elidibus\n" + elidibus[poveste + replica];
            replica++;
            if (replica == 3)
            {
                replica = 0;
                poveste = Random.Range(0, emet.Length / 3) * 3;
            }
        }
        else if (state == "tauntEmet")
        {
            text1.text = "Emet-Selch\n" + liniste1[Random.Range(0, liniste1.Length)];
            text2.text = "Elidibus\n" + taunt2[Random.Range(0, taunt2.Length)];
        }
        else if (state == "tauntElidibus")
        {
            text1.text = "Emet-Selch\n" + taunt1[Random.Range(0, taunt1.Length)];
            text2.text = "Elidibus\n" + liniste2[Random.Range(0, liniste2.Length)];
        }
        else if (state == "returnEmet")
        {
            if (replica < 2)
            {
                text1.text = "Emet-Selch\n" + raspuns1[replica];
                text2.text = "Elidibus\n" + raspuns2[replica];
                replica++;
            }
            else
            {
                tauntedEmet = false;
            }
        }
        else if (state == "returnElidibus")
        {
            if (replica < 2)
            {
                text1.text = "Emet-Selch\n" + raspuns1[replica + 2];
                text2.text = "Elidibus\n" + raspuns2[replica + 2];
                replica++;
            }
            else
            {
                tauntedElidibus = false;
            }
        }
        else
        {
            liniste = (liniste + Random.Range(1, liniste1.Length)) % liniste1.Length;
            text1.text = "Emet-Selch\n" + liniste1[liniste];
            text2.text = "Elidibus\n" + liniste2[liniste];
        }
        timp = pauza;
    }
}
