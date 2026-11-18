using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }
    private VisualElement m_Healthbar;

    public float displayTime = 4.0f;
    private VisualElement m_NonPlayerDialogue;
    private float m_TimerDisplay;

    // Контейнер кінця гри та екрани Win / Lose
    private VisualElement m_EndScreen;
    private VisualElement m_WinPanel;
    private VisualElement m_LosePanel;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        m_Healthbar = root.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);

        m_NonPlayerDialogue = root.Q<VisualElement>("NonPlayerDialogue");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;

        // Знаходимо EndScreen та вкладені WinPanel і LosePanel
        m_EndScreen = root.Q<VisualElement>("EndScreen");
        m_WinPanel = root.Q<VisualElement>("WinPanel");
        m_LosePanel = root.Q<VisualElement>("LosePanel");

        m_EndScreen.style.display = DisplayStyle.None;
        m_WinPanel.style.display = DisplayStyle.None;
        m_LosePanel.style.display = DisplayStyle.None;
    }

    private void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
            {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }
        }
    }

    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }

    public void DisplayDialogue()
    {
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }

    public void ShowWin()
    {
        m_EndScreen.style.display = DisplayStyle.Flex;
        m_WinPanel.style.display = DisplayStyle.Flex;
        m_LosePanel.style.display = DisplayStyle.None;
        Invoke(nameof(RestartGame), 3.0f);
    }

    public void ShowLose()
    {
        m_EndScreen.style.display = DisplayStyle.Flex;
        m_LosePanel.style.display = DisplayStyle.Flex;
        m_WinPanel.style.display = DisplayStyle.None;
        Invoke(nameof(RestartGame), 3.0f);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}