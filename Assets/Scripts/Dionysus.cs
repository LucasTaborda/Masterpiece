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
    public GameObject gameOverScreen;
    public Animator dionysusAnimator;

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
        dialogs.Add("ACT_1_SCENE_1", "La noche yacía en penumbra y frío.");
        dialogs.Add("ACT_1_SCENE_2", "El Rey se alzaba enorme ante todos; el príncipe no se creía digno de esa estatura.");
        dialogs.Add("ACT_1_SCENE_3", "Con él, el Rey no fingía. El príncipe se apartaba, quebrado, y de rodillas lloraba al descubrir la máscara de amor y el monstruo que ocultaba.");
        dialogs.Add("ACT_2_SCENE_1", "Bajo el ardiente sol del mediodía, el príncipe vagaba a la vera del río.");
        dialogs.Add("ACT_2_SCENE_2", "Cruzó el puente y halló a su prometida al otro lado. Subió a una roca para saludarla. Ella recogía, distraída, las rosas caídas.");
        dialogs.Add("ACT_2_SCENE_3", "Rosas ajenas encendieron su furia. El príncipe ante todos señaló la traición y supo otra vez que no una mujer, sino una máscara amó. Y fue una hoguera la que la mentira consumió.");
        dialogs.Add("ACT_3_SCENE_1", "Bajo el cielo ceniciento, en un intento de redención artística, el príncipe a las musas buscó en el bosque.");
        dialogs.Add("ACT_3_SCENE_2", "El bosque le negó ninfas y le dio ladrones: acero en alto, rodillas temblando, una súplica.");
        dialogs.Add("ACT_3_SCENE_3", "Pero al reconocer al príncipe, arrepentido y temblando de rodillas cayó. Tras la máscara del bravo león vivía un cobarde ratón.");
        dialogs.Add("ACT_4_SCENE_1", "Al alba, tras la torre oeste, el príncipe comenzó a pintar su obra.");
        dialogs.Add("ACT_4_SCENE_2", "Al dar las doce, la presentó. Desde poltronas de jueces llovieron críticas afiladas y el ánimo del príncipe al suelo cayó.");
        dialogs.Add("ACT_4_SCENE_3", "Bajo sus máscaras virtuosas anidaban la envidia y la torpeza. El príncipe juró venganza y, en el ocaso, se alzó y partió al este a desenmascarar y destruir.");
        dialogs.Add("LAST_SCENE", "Y como el príncipe habría deseado, reuní a mis monstruos enmascarados para poner fin a su patética existencia.");
        dialogs.Add("GAME_OVER", "Me has fastidiado a mí y a mi perfecta obra. ¡Muere, muere, muere!");

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
            ActManager.Instance.Interrupt();
    }

    void OnGUI(){
        GUILayout.Label("Happiness: " + currentHappiness);
    }

    public void SpawnTV()
    {
        LeanTween.moveY(rectTransform, originalPosition.y, 1f).setEase(LeanTweenType.easeOutCubic);
        if(ActManager.Instance.runIntro) Invoke("MakePresentation", 2f);
        else{
            StartMasterpiece();
        }
    }

    public void HideTV()
    {
        LeanTween.moveY(rectTransform, 300f, 2f).setEase(LeanTweenType.easeInBounce);
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

    public void SayGameOver()
    {
        dialogBox.WriteMessage(dialogs["GAME_OVER"], talkingSpeed, Explode);
    }

    public void SayEndMasterpiece()
    {
        dialogBox.WriteMessage(dialogs["LAST_SCENE"], talkingSpeed, MakeReverence);
    }

    private void MakeReverence()
    {
        dionysusAnimator.SetTrigger("Reverence");
    }

    public void Explode()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_EXPLOSION]);
        gameOverScreen.SetActive(true);
    }

    private void EndGame()
    {
        ActManager.Instance.ChooseEnd();
    }

    public void OnReverenceFinished()
    {

    }
}
