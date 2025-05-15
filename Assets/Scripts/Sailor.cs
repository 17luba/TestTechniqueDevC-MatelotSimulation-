using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sailor : MonoBehaviour
{
    public enum State { Idle, Working, Tired }
    public State currentState = State.Idle;

    public float taskDuration = 5f;
    private float taskTimer = 0f;

    [Header("Task")]
    public float tasksDoneSinceRest = 0f; // Compteur de la t�che effectu�e depuis le dernier repos
    public float restDuration = 10f; // Dur�e de repos
    public int tasksBeforeRest = 5; // Nombre de t�ches avant d'�tre fatigu�

    private float restTimer = 0f; // Compteur du temps de repos

    [Header("UI")]
    public GameObject progressBarUI; // Slider parent
    public Slider progressSlider;    // Le slider lui-m�me
    public TextMeshProUGUI statusText; // Texte d'�tat

    void Update()
    {
        switch (currentState)
        {
            case State.Idle: // Etat disponible pour effectuer une t�che
                statusText.text = "Disponible";

                if (Input.GetMouseButtonDown(0) && IsClicked()) // Detection du clic utilisateur sur le matelot
                {
                    if (tasksDoneSinceRest >= tasksBeforeRest) // Si nombre de taches effectu�es requis atteint, passer en mode repos
                    {
                        ChangeState(State.Tired);
                    }
                    else
                    {
                        StartTask();
                    }
                }
                break;

            case State.Working: // Etat de travail
                statusText.text = "Effectue une t�che";

                // Incr�mentation de la progression de la t�che
                taskTimer += Time.deltaTime;
                float progress = Mathf.Clamp01(taskTimer / taskDuration);
                progressSlider.value = progress;

                if (progress >= 1f)
                {
                    EndTask();
                }
                break;

            case State.Tired: // Etat de fatigue, ne peut pas travailler pendant un certain temps
                statusText.text = "Fatigu�";

                restTimer += Time.deltaTime; // Incr�mentation du temps de repos


                if (restTimer >= restDuration) // Fin de repos, retour � l'�tat disponible
                {
                    restTimer = 0f;
                    tasksDoneSinceRest = 0;
                    ChangeState(State.Idle);
                }
                break;
        }
    }

    void StartTask() // D�marrer une t�che
    {
        currentState = State.Working;
        taskTimer = 0f;

        progressSlider.value = 0f;
        progressBarUI.SetActive(true);
    }

    void EndTask()
    {
        currentState = State.Idle;
        progressBarUI.SetActive(false);

        tasksDoneSinceRest++; // Incr�mente le compteur de t�ches effectu�es
    }

    void ChangeState(State newState) // Changer l'�tat du matelot
    {
        currentState = newState;

        if (newState == State.Idle)
        {
            progressBarUI.SetActive(false);
            restTimer = 0f; // R�initialise le timer de repos
        }
        
    }

    bool IsClicked() // V�rifie si le matelot a �t� cliqu�
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.transform == transform;
        }
        return false;
    }
}
