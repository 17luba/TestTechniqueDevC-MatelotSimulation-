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
    public float tasksDoneSinceRest = 0f; // Compteur de la tâche effectuée depuis le dernier repos
    public float restDuration = 10f; // Durée de repos
    public int tasksBeforeRest = 5; // Nombre de tâches avant d'être fatigué

    private float restTimer = 0f; // Compteur du temps de repos

    [Header("UI")]
    public GameObject progressBarUI; // Slider parent
    public Slider progressSlider;    // Le slider lui-même
    public TextMeshProUGUI statusText; // Texte d'état

    void Update()
    {
        switch (currentState)
        {
            case State.Idle: // Etat disponible pour effectuer une tâche
                statusText.text = "Disponible";

                if (Input.GetMouseButtonDown(0) && IsClicked()) // Detection du clic utilisateur sur le matelot
                {
                    if (tasksDoneSinceRest >= tasksBeforeRest) // Si nombre de taches effectuées requis atteint, passer en mode repos
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
                statusText.text = "Effectue une tâche";

                // Incrémentation de la progression de la tâche
                taskTimer += Time.deltaTime;
                float progress = Mathf.Clamp01(taskTimer / taskDuration);
                progressSlider.value = progress;

                if (progress >= 1f)
                {
                    EndTask();
                }
                break;

            case State.Tired: // Etat de fatigue, ne peut pas travailler pendant un certain temps
                statusText.text = "Fatigué";

                restTimer += Time.deltaTime; // Incrémentation du temps de repos


                if (restTimer >= restDuration) // Fin de repos, retour à l'état disponible
                {
                    restTimer = 0f;
                    tasksDoneSinceRest = 0;
                    ChangeState(State.Idle);
                }
                break;
        }
    }

    void StartTask() // Démarrer une tâche
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

        tasksDoneSinceRest++; // Incrémente le compteur de tâches effectuées
    }

    void ChangeState(State newState) // Changer l'état du matelot
    {
        currentState = newState;

        if (newState == State.Idle)
        {
            progressBarUI.SetActive(false);
            restTimer = 0f; // Réinitialise le timer de repos
        }
        
    }

    bool IsClicked() // Vérifie si le matelot a été cliqué
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.transform == transform;
        }
        return false;
    }
}
