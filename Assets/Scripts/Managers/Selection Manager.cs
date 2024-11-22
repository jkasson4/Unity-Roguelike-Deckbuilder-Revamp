using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // Dependancies
    public CardManager cardManager;
    public PlayerManager playerManager;

    // Current Selection Variables
    [SerializeField] private int selection_state;
    [SerializeField] private GameObject source_obj;
    [SerializeField] private List<int> selection_types = new List<int>();
    [SerializeField] private List<GameObject> selected_objects = new List<GameObject>();

    // Selection Stack
    [SerializeField] private Stack<Selection> selection_stack = new Stack<Selection>();

    /* ----Selection Types----
     * 0 = Active IDamageable
     * 1 = Active IBuffable
     * 2 = Card In Grave
     * 3 = Card In Hand
     * 4 = Creature In Play
     * 5 = Enemy In Play
     * ------------------------
     */

    /* ----Selection State----
     * 0: Casting Card
     * 1: Creature Attack
     * 2: Upkeep Effect
     * 3: Death Effect
     * 4: Damage Effect
     * ------------------------
     */

    // Temporary Object For Storing Selection In Stack
    private struct Selection
    {
        public Selection(GameObject source, int state)
        {
            this.source = source;
            this.state = state;
        }
        public GameObject source;
        public int state;
    }

    void Awake()
    {
        ResetSelection();
    }

    // Process First Selection In Stack
    // Also decrements selection on right-click pressed.

    void Update()
    {
        ProcessSelection();
        if (Input.GetMouseButtonDown(1))
        {
            DecrementSelection();
        }
    }

    // Reset Selection Stack

    public void Reset()
    {
        ResetSelection();
        selection_stack.Clear();
    }

    // Parse Selection In Context Of Source/State And Update Current Selection Variables If Valid
    public void SendSelection(GameObject source)
    {
        // Initiate Selection If No Current Selection And Valid Card In Hand Or Valid Card In Play
        if (source_obj == null)
        {
            if (source.GetComponent<Card>() != null && cardManager.HandContains(source.GetComponent<Card>()) && playerManager.GetMana() >= source.GetComponent<Card>().GetManaCost())
            {
                selection_stack.Push(new Selection(source, 0));
            }
            else if (source.GetComponent<Creature>() != null && cardManager.PlayContains(source.GetComponent<Creature>()) && source.GetComponent<Creature>().GetActions() > 0)
            {
                selection_stack.Push(new Selection(source, 1));
            }
        }
        // Add Card To Selection Unless Invalid
        // Invalid selection occurs when selection matches source or already selected card, selection not active, or selection not contained in proper zone.
        else if (source_obj != source && selected_objects.Contains(source) == false)
        {
            switch (selection_types[selected_objects.Count])
            {
                case 0:
                    if (source.GetComponent<IDamageable>() != null && source.GetComponent<IDamageable>().GetActive() == true)
                    {
                        selected_objects.Add(source);
                        source.GetComponent<CardEmitter>().PlaySelectionSelected();
                    }
                    break;
                case 1:
                    if (source.GetComponent<IBuffable>() != null && source.GetComponent<IBuffable>().GetActive() == true)
                    {
                        selected_objects.Add(source);
                        source.GetComponent<CardEmitter>().PlaySelectionSelected();
                    }
                    break;
                case 3:
                    if (source.GetComponent<Card>() != null && cardManager.HandContains(source.GetComponent<Card>()))
                    {
                        selected_objects.Add(source);
                        source.GetComponent<CardEmitter>().PlaySelectionSelected();
                    }
                    break;
                default:
                    Debug.LogError("No matching selection type for type " + selection_types[0]);
                    break;
            }
            // Finish Selection If Selected Objects Satisfies Source Targets
            if(selected_objects.Count >= selection_types.Count)
            {
                CompleteSelection();
            }
        }
    }

    // Public Methods To Add Selections To Queue

    public void SendUpkeep(IUpkeepable source)
    {
        selection_stack.Push(new Selection(source.gameObject, 2));
    }

    public void SendDeath(IDeathable source)
    {
        selection_stack.Push(new Selection(source.gameObject, 3));
    }

    public void SendDamage(IDamageable source)
    {
        selection_stack.Push(new Selection(source.gameObject, 4));
    }

    // Initiate Selection Of Object

    private void StartSelection(Selection source)
    {
        source_obj = source.source;
        selection_state = source.state;

        // Parse Selection Type Based On State
        switch (source.state)
        {
            case 0:
                selection_types = source_obj.GetComponent<Card>().GetCastTargets();
                break;
            case 1:
                selection_types = source_obj.GetComponent<Creature>().GetAttackTargets();
                break;
            case 2:
                selection_types = source_obj.GetComponent<IUpkeepable>().GetUpkeepTargets();
                break;
            case 3:
                selection_types = source_obj.GetComponent<IDeathable>().GetDeathTargets();
                break;
            case 4:
                selection_types = source_obj.GetComponent<IDamageable>().GetDamageTargets();
                break;
            default:
                Debug.LogError("No matching state on process for selection state " + selection_state);
                break;
        }

        // Play Source Selection Particles
        source_obj.GetComponent<CardEmitter>().PlaySelectionSource();

        // Complete Selections If Selection Targets Already Satisfied
        if (selected_objects.Count >= selection_types.Count)
        {
            CompleteSelection();
        }
    }

    // Finish Selection With Remainder As Null

    public void Done()
    {
        if (source_obj != null)
        {
            for (int i = selected_objects.Count; i < selection_types.Count; i++)
            {
                selected_objects.Add(null);
            }
            CompleteSelection();
        }
    }

    // Finish Selection Of State

    private void CompleteSelection()
    {
        // Parse Selection Type Based On State
        switch (selection_state)
        {
            case 0:
                source_obj.GetComponent<Card>().Cast(selected_objects);
                break;
            case 1:
                source_obj.GetComponent<Creature>().AttackEffect(selected_objects);
                break;
            case 2:
                source_obj.GetComponent<IUpkeepable>().UpkeepEffect(selected_objects);
                break;
            case 3:
                source_obj.GetComponent<IDeathable>().DeathEffect(selected_objects);
                break;
            case 4:
                source_obj.GetComponent<IDamageable>().DamageEffect(selected_objects);
                break;
            default:
                Debug.LogError("No matching state on complete for selection state " + selection_state);
                break;
        }
        // Reset Selection And Start Processing Next Selection
        ResetSelection();
        ProcessSelection();
    }

    // Remove Most Recent Selection
    // Cancels selection if selected_object is empty and selection is user-prompted (states 0 or 1).

    private void DecrementSelection()
    {
        if (selection_state < 2)
        {
            if (selected_objects.Count < 1)
            {
                ResetSelection();
            }
            else
            {
                selected_objects[selected_objects.Count - 1].GetComponent<CardEmitter>().StopSelectionSelected();
                selected_objects.RemoveAt(selected_objects.Count - 1);
            }
        }
    }

    // Resets Current Selection Variables

    private void ResetSelection()
    {
        if(source_obj != null)
        {
            source_obj.GetComponent<CardEmitter>().StopSelectionSource();
        }
        for (int i = 0; i < selected_objects.Count; i++)
        {
            if (selected_objects[i] != null)
            {
                selected_objects[i].GetComponent<CardEmitter>().StopSelectionSelected();
            }
        }
        source_obj = null;
        selection_state = -1;
        selection_types.Clear();
        selected_objects.Clear();
    }

    // Processes Top Selection In Stack

    private void ProcessSelection()
    {
        if (selection_stack.Count > 0 && source_obj == null)
        {
            Selection source = selection_stack.Pop();

            StartSelection(source);
        }
    }

    // Getters

    public bool GetQueueEmpty()
    {
        return (selection_stack.Count < 1 && source_obj == null);
    }
}
