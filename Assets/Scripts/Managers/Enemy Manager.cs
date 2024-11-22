using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Dependancies
    public GameManager gameManager;
    public EnemyGenerator enemyGenerator;
    public SelectionManager selectionManager;
    public EnemySelectionAI enemySelectionAI;

    // Markers
    public GameObject start_marker;
    public GameObject play_marker;
    public GameObject grave_marker;

    // Enemy Zone Lists
    private List<Enemy> start = new List<Enemy>();
    private List<Enemy> play = new List<Enemy>();
    private List<Enemy> grave = new List<Enemy>();

    // Zone-Specific Card Spacing and Rotation
    private float card_height = 0.015f;
    private float grave_distance = 0.3f;
    private int play_distance = 6;
    private float death_rise = 0.3f;
    private Quaternion inactive_rot = Quaternion.Euler(-90, 180, 0);
    private Quaternion active_rot = Quaternion.Euler(90, 0, 0);

    // Load Enemies

    public void LoadEnemies(List<(int id, int active_turn)> enemy_tuples)
    {
        for (int i = 0; i < enemy_tuples.Count; i++)
        {
            AddEnemy(enemy_tuples[i].id, enemy_tuples[i].active_turn);
        }
    }

    // Adding Enemies
    // active_turn = turn of combat where enemy becomes active.

    public void AddEnemy(Enemy enemy)
    {
        start.Add(enemy);
        AdjustStart();
    }

    public void AddEnemy(int id, int active_turn)
    {
        AddEnemy(enemyGenerator.GenerateEnemy(id, active_turn));
    }

    // Transferring Enemies Between Zones

    public void StartToPlay(int pos)
    {
        if (start.Count > pos && pos > -1)
        {
            play.Add(start[pos]);
            start.RemoveAt(pos);
            AdjustStart();
            AdjustPlay();
        }
    }

    public void StartToPlay(Enemy enemy)
    {
        if (play.Contains(enemy))
        {
            StartToPlay(start.FindIndex(a => a == enemy));
        }
    }

    public void PlayToGrave(int pos)
    {
        if (play.Count > pos && pos > -1)
        {
            grave.Add(play[pos]);
            play.RemoveAt(pos);
            AdjustPlay();
            AdjustGrave();
        }

        // Check If Combat Complete
        if (play.Count == 0 && start.Count == 0)
        {
            gameManager.EndCombat();
        }
    }

    public void PlayToGrave(Enemy enemy)
    {
        if (play.Contains(enemy))
        {
            PlayToGrave(play.FindIndex(a => a == enemy));
        }
    }

    // Reset Zones

    public void Reset()
    {
        for (int i = 0; i < start.Count; i++)
        {
            Destroy(start[i].gameObject);
        }
        start.Clear();
        for (int i = 0; i < play.Count; i++)
        {
            Destroy(play[i].gameObject);
        }
        play.Clear();
        for (int i = 0; i < grave.Count; i++)
        {
            Destroy(grave[i].gameObject);
        }
        grave.Clear();
    }

    // Check Combat Complete

    public bool GetEnemiesCleared()
    {
        return play.Count == 0 && start.Count == 0;
    }

    // Active Enemies
    // For each enemy, activates only if active_turn = turn

    public void ActivateEnemies(int turn)
    {
        for (int i = start.Count - 1; i > -1; i--)
        {
            if (start[i].GetActiveTurn() < turn + 2)
            {
                StartToPlay(i);
            }
        }
        for(int i = play.Count - 1; i > -1; i--)
        {
            if (play[i].GetActiveTurn() <= turn)
            {
                play[i].Activate();
            }
        }
        AdjustPlay();
    }

    // Perform Enemy Actions If Active

    public void PerformActions()
    {
        for(int i = play.Count - 1; i > -1; i--)
        {
            if (play[i].GetActionFlag() == true)
            {
                play[i].ActionEffect(enemySelectionAI.GetTargets(play[i].GetActionTargets()));
            }
            play[i].SetActionFlag(false);
        }
    }

    public void UpdateActionFlags()
    {
        for(int i = 0; i < play.Count ; i++)
        {
            if (play[i].GetActive())
            {
                play[i].SetActionFlag(true);
            }
        }
    }

    // Set Defense To 0
    public void ResetDefense()
    {
        for (int i = 0; i < play.Count; i++)
        {
            if (play[i].GetActive())
            {
                play[i].ResetDefense();
            }
        }
    }

    // Adjust Enemy Positions In Zones
    // Run every time a card is added or removed from a zone.

    private void AdjustStart()
    {
        for (int i = 0; i < start.Count; i++)
        {
            CardHighlight highlight = start[i].GetComponent<CardHighlight>();
            Vector3 pos = start_marker.transform.position + new Vector3(0, card_height, 0) * (i + 1);
            highlight.SetOriginPos(pos);
            highlight.SetHighlightPos(pos);
            highlight.SetOriginRot(inactive_rot);
            highlight.SetOriginRot(inactive_rot);
        }
    }

    private void AdjustPlay()
    {
        for (int i = 0; i < play.Count; i++)
        {
            CardHighlight highlight = play[i].GetComponent<CardHighlight>();
            Vector3 pos = play_marker.transform.position - play_marker.transform.right * play_distance * 0.5f
                + play_marker.transform.right * play_distance * (i + 1) / (play.Count + 1) + card_height * play_marker.transform.up * (i + 1);
            highlight.SetOriginPos(pos);
            highlight.SetHighlightPos(pos);
            if (play[i].GetActive() == false)
            {
                highlight.SetOriginRot(inactive_rot);
                highlight.SetHighlightRot(inactive_rot);
            }
            else
            {
                highlight.SetOriginRot(active_rot);
                highlight.SetHighlightRot(active_rot);
            }
        }
    }

    private void AdjustGrave()
    {
        for (int i = 0; i < grave.Count; i++)
        {
            CardHighlight highlight = grave[i].GetComponent<CardHighlight>();
            Vector3 pos = grave_marker.transform.position + new Vector3(0, card_height, -grave_distance) * (i + 1);
            highlight.SetOriginPos(pos);
            highlight.SetHighlightPos(pos);
            highlight.SetOriginRot(active_rot);
            highlight.SetHighlightRot(active_rot);
        }
    }

    public void RaiseInPlay(Enemy enemy)
    {
        if (play.Contains(enemy))
        {
            CardHighlight highlight = play[play.FindIndex(a => a == enemy)].GetComponent<CardHighlight>();
            Vector3 pos = enemy.transform.position + new Vector3(0, death_rise, 0);
            highlight.SetOriginPos(pos);
            highlight.SetHighlightPos(pos);
        }
    }

    // Send Upkeep Effects Of All Enemies

    public void SendUpkeepEffects()
    {
        for (int i = play.Count - 1; i > -1; i--)
        {
            if (play[i].GetActive())
            {
                play[i].ResetDefense();
                selectionManager.SendUpkeep(play[i]);
            }
        }
    }

    // Play Contains

    public bool PlayContains(Enemy enemy)
    {
        return play.Contains(enemy);
    }
}
