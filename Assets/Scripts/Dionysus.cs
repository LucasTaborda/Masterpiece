using System.Collections.Generic;
using UnityEngine;

public class Dionysus : MonoBehaviour
{
    public Eye happyEye;
    public Eye sadEye;
    public int currentHappiness = 3;
    public int maxHappiness = 5;
    public Dictionary<string, string> dialogs = new Dictionary<string, string>();
    public DialogBox dialogBox;
    public float talkingSpeed = 0.05f;
    public static Dionysus Instance { get; private set; }
    public RectTransform rectTransform;
    private Vector3 originalPosition;
    public bool runIntro = false;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            throw new System.Exception("Only one Dionysus is allowed");
        
        InitializeDialogs();

        originalPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(originalPosition.x, 1000f);
    }

    private void InitializeDialogs()
    {
        dialogs.Add("HELLO", "Antes pensaba que la vida era una tragedia. Finalmente me di cuenta que es una comedia.");
        dialogs.Add("TEST_1", "Luna al medio, árbol a la derecha.");
        dialogs.Add("TEST_2", "Sol a la izquierda.");
        dialogs.Add("PRESENTATION_1", "Bienvenido a mi última obra maestra. Te he visto antes. Has disfrutado de mi arte con autenticidad y sincero placer. Por eso tendrás el honor de ayudarme a realizarla. Serás mi asistente de escenografía.");
        dialogs.Add("TUTORIAL_1", "Veamos si puedes usar los botones con símbolos de flechas de tu panel para mover el sol a la cruz que está arriba en el medio.");
        dialogs.Add("TUTORIAL_2", "Bien, se te da natural. Utiliza el último botón para que el sol cambie a luna.");
        dialogs.Add("TUTORIAL_3", "Excelente. Ahora intenta mover el árbol hasta la posición de la derecha. El primer botón sirve para intercambiar entre los rieles disponibles en escena.");
        dialogs.Add("TUTORIAL_4", "Ya eres todo un experto. Comencemos con la obra entonces. Trabaja rápido y hazme caso. No me hagas enfadar o pagarás las consecuencias.");
    }


    void Start()
    {
        // dialogBox.WriteMessage(dialogs["HELLO"], talkingSpeed);
    }

    public void MakeHappy()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_SUCCESS]);
        happyEye.Shine();
        if(currentHappiness < maxHappiness)
            currentHappiness++;
    }

    public void MakeSad()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_FAIL]);
        sadEye.Shine();
        if (currentHappiness > 0)
            currentHappiness--;
        else
            Debug.Log("You Lose");
    }

    void OnGUI(){
        GUILayout.Label("Happiness: " + currentHappiness);
    }

    public void SpawnTV()
    {
        LeanTween.moveY(rectTransform, originalPosition.y, 1f).setEase(LeanTweenType.easeOutBounce);
        if(runIntro) Invoke("MakePresentation", 2f);
        else{
            StartMasterpiece();
        }
        //
    }

    public void MakePresentation()
    {
        dialogBox.WriteMessage(dialogs["PRESENTATION_1"], talkingSpeed, StartMasterpieceRetarded);
    }

    private void StartMasterpieceRetarded()
    {
        Invoke("StartMasterpiece", 2f);
    }

    private void StartMasterpiece()
    {
        ActManager.Instance.StartMasterPiece();
    }
}
