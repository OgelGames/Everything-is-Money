using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class Stat : MonoBehaviour
{
    public bool interactable; // Is the stat interactable or just for display?
    public bool displayValueBelowName; // Text formatting option

    private TextMeshProUGUI text; // The display text
    private AudioSource sound; // The sound to play when a bullet hits
    private Collider2D trigger; // Trigger collider for detecting bullets

    // Stat properties (used so stat text gets updated automatically)
    #region Name property
    [SerializeField] private new string name;

    public string Name
    {
        get => name;
        set
        {
            this.name = value;
            Display();
        }
    }
    #endregion
    #region Value property
    [SerializeField] private int value;

    public int Value
    {
        get => value;
        set
        {
            this.value = value;
            Display();
        }
    }
    #endregion

    private void Start()
    {
        // Get references
        text = GetComponent<TextMeshProUGUI>();
        sound = GetComponent<AudioSource>();
        trigger = GetComponent<Collider2D>();

        // Make sure the trigger collider is a trigger
        trigger.isTrigger = true;

        // Display stat for the fist time
        Display();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // If a bullet hit, and the stat is interactable...
        if (collider.gameObject.tag == "Bullet" && interactable)
        {
            // Play sound
            sound.Play();

            // Destroy the bullet
            Destroy(collider.gameObject);

            // Increase the stat value
            Value++;
        }
    }

    private void Display()
    {
        // Update the displayed text
        text.text = ToString();
    }

    // ToString() is overridden for ease of use
    public override string ToString()
    {
        if (displayValueBelowName)
        {
            return string.Format("{0}:\n{1}", Name, Value);
        }
        return string.Format("{0}: {1}", Name, Value);
    }
}
