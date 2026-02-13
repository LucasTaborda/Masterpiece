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
        dialogs.Add("TUTORIAL_4", "Ya eres todo un experto. Comencemos con la obra entonces. Trabaja rápido y hazme caso. No me hagas enfadar o pagarás las consecuencias. Puedes consultar qué actor es cada personaje en el folleto que te di. No te preocupes por que los actores se liberen, están bien amarrados.");
        dialogs.Add("ACT_1_SCENE_1_OPENING", "Hubo una vez un príncipe que de la peor forma entendió que todos llevan una máscara puesta. Fue en una noche, luego de una fiesta en el palacio.");
        dialogs.Add("ACT_1_SCENE_1", "El astro plateado alumbraba desde el centro del cielo.");
        dialogs.Add("ACT_1_SCENE_1_ENDING", "La fiesta ya había terminado, el príncipe había estado toda la noche realizando sublimes actuaciones y profundos monólogos para los invitados.");
        dialogs.Add("ACT_1_SCENE_2", "La luz de la luna alumbraba al renombrado Rey y al príncipe que estaban justo debajo. El primero lo miraba subido a su estrado con altanería.");
        dialogs.Add("ACT_1_SCENE_2_ENDING", "Con crueldad acusó el mandatario: \"Eres una vergüenza, yo quería un heredero y el destino me ha dado a un mojigato. Hubiese preferido tener otro caballo antes que un hijo como tú\".");
        dialogs.Add("ACT_1_SCENE_3", "El Rey se puso su corona frente al heredero. El príncipe se apartaba, quebrado y llorando de rodillas.");
        dialogs.Add("ACT_1_SCENE_3_ENDING", "Y así el príncipe vio que bajo la máscara del buen gobernante de su padre, se hallaba un monstruo.");
        dialogs.Add("ACT_2_SCENE_1_OPENING", "Ante semejante dolor, el príncipe fue en búsqueda del consuelo de su amada. Ella solía pasear por el río todos los días.");
        dialogs.Add("ACT_2_SCENE_1", "El sol se alzaba ardiente al mediodía. A las 3:00 se hallaba el caudaloso río.");
        dialogs.Add("ACT_2_SCENE_2", "El heredero cruzó el puente y divisó a su prometida a lo lejos. Subió a una roca para saludarla. Ella recogía, distraída, algo del suelo.");
        dialogs.Add("ACT_2_SCENE_2_ENDING", "Lo que ella recogía eran rosas que el príncipe no había regalado. Loco de celos por la presunta traición ordenó capturarla.");
        dialogs.Add("ACT_2_SCENE_3_OPENING", "Esa misma noche y en el mismo lugar donde presenció la infidelidad, el iracundo príncipe llevó a cabo su castigo.");
        dialogs.Add("ACT_2_SCENE_3", "La ató en lo alto de una hoguera y ella erguida murió. La luna se empequeñecía detrás de la doncella. El príncipe desde el río, la señalaba por su traición.");
        dialogs.Add("ACT_2_SCENE_3_ENDING", "Y en ese momento se dio cuenta que no había amado a una mujer, sino a una falsa máscara de amor.");
        dialogs.Add("ACT_3_SCENE_1_OPENING", "Como última esperanza, el príncipe quiso encontrar en el arte consuelo para su alma dolida. Y se dirigió al bosque en búsqueda de musas que le dieran inspiración.");
        dialogs.Add("ACT_3_SCENE_1", "Las nubes se acercaban desde el este al bosque de las musas del oeste.");
        dialogs.Add("ACT_3_SCENE_1_ENDING", "El príncipe llegó al bosque, pero en lugar de una ninfa que le diera inspiración, se encontró con un temible ladrón.");
        dialogs.Add("ACT_3_SCENE_2", "Cuando las nubes estuvieron en la entrada del bosque, el ladrón alzó su espada para decapitarlo. El príncipe suplicaba junto a él.");
        dialogs.Add("ACT_3_SCENE_2_ENDING", "Pero el ladrón vio que la víctima no era cualquier viajero, sino el hijo del Rey.");
        dialogs.Add("ACT_3_SCENE_3", "El atacante arrepentido se alejó y se reverenció.");
        dialogs.Add("ACT_3_SCENE_3_ENDING", "Y el príncipe vio que tras la máscara de bravo león, había un cobarde ratón. Esa era la inspiración que necesitaba. O eso creyó.");
        dialogs.Add("ACT_4_SCENE_1_OPENING", "En lo alto de la torre el príncipe comenzó a escribir su nueva obra. El tema: Las máscaras.");
        dialogs.Add("ACT_4_SCENE_1", "Al alba, el sol iluminaba la torre oeste.");
        dialogs.Add("ACT_4_SCENE_2_OPENING", "Al dar las doce, el príncipe bajó de la torre para presentar su obra a los habitantes del reino.");
        dialogs.Add("ACT_4_SCENE_2", "Justo bajo el sol del mediodía el público criticó afiladamente al artista mientras el príncipe subido al escenario actuaba de rey.");
        dialogs.Add("ACT_4_SCENE_2_ENDING", "Bajo las máscaras virtuosas del público anidaban la envidia y la torpeza, El príncipe harto de la falsedad ordenó quemar a los monstruos.");
        dialogs.Add("ACT_4_SCENE_3", "En el ocaso, los críticos fueron quemados suplicantes, mientras el príncipe se despedía de ellos desde arriba del escenario.");
        dialogs.Add("ACT_4_SCENE_3_ENDING", "Y el príncipe complacido comprendió el verdadero significado de su obra. Que es justo antes de morir que a los monstruos se les cae la máscara y se les ve su verdadero rostro.");
        dialogs.Add("LAST_SCENE", "Por eso ahora les toca morir a los monstruos enmascarados que criticaron mi arte por celos y falta de talento. ¡Por el arte y la belleza!");
        dialogs.Add("GAME_OVER", "Me has fastidiado a mí y a mi perfecta obra. ¡Muere, muere, muere!");
    }


    void Start()
    {
        // dialogBox.WriteMessage(dialogs["HELLO"], talkingSpeed);
    }

    public void MakeHappy()
    {
        DecisionFeedback.Instance.SetFeedback(true);
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_SUCCESS]);
        happyEye.Shine();
        if(currentHappiness < maxHappiness)
            currentHappiness++;
    }

    public void MakeSad()
    {
        DecisionFeedback.Instance.SetFeedback(false);
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
        dialogBox.WriteMessage(dialogs["PRESENTATION_1"], talkingSpeed, StartMasterpiece, false, default, true);
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
