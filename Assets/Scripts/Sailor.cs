using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sailor : MonoBehaviour
{
    public enum State { Idle, Working, Tired }
    public State currentState = State.Idle;

    public float taskDuration = 5f;
    private float taskTimer = 0f;

    [Header("Task")]
    public float tasksDoneSinceRest = 0f; // Compteur de la tâche effectuée depuis le dernier repos
    public float restDuration = 10f; // Durée de repos

    private float restTimer = 0f;

    [Header("UI")]
    public GameObject progressBarUI; // Slider parent
    public Slider progressSlider;    // Le slider lui-même
    public TextMeshProUGUI statusText; // Texte d'état

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                statusText.text = "Disponible";

                if (Input.GetMouseButtonDown(0) && IsClicked())
                {
                    if (tasksDoneSinceRest >= 2)
                    {
                        ChangeState(State.Tired);
                    }
                    else
                    {
                        StartTask();
                    }
                }
                break;

            case State.Working:
                statusText.text = "Effectue une tâche";

                taskTimer += Time.deltaTime;
                float progress = Mathf.Clamp01(taskTimer / taskDuration);
                progressSlider.value = progress;

                if (progress >= 1f)
                {
                    EndTask();
                }
                break;

            case State.Tired:
                statusText.text = "Fatigué";

                restTimer += Time.deltaTime;
                if (restTimer >= restDuration)
                {
                    restTimer = 0f;
                    tasksDoneSinceRest = 0;
                    ChangeState(State.Idle);
                }
                break;
        }
    }

    void StartTask()
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

    void ChangeState(State newState)
    {
        currentState = newState;

        if (newState == State.Idle)
        {
            progressBarUI.SetActive(false);
            restTimer = 0f; // Réinitialise le timer de repos
        }
        
    }

    bool IsClicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.transform == transform;
        }
        return false;
    }
}
