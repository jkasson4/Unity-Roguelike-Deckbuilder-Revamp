using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // Dependancies
    public SelectionManager selectionManager;
    public CardManager cardManager;
    public EnemyManager enemyManager;
    public PlayerManager playerManager;
    public CameraManager cameraManager;
    public GameManager gameManager;
    public TMP_Text turn_text;

    // Turn Variables
    private int turn_phases = 6;
    private int current_turn;

    /* ----Turn Phases----
     * 0: Enemy Upkeep
     * 1: Enemy Turn
     * 2: Enemy Cleanup
     * 3: Player Upkeep
     * 4: Player Turn
     * 5: Player Cleanup
     * ------------------
     */

    void Start()
    {
        ResetCombat();
    }

    // Increment Turn If Selection Queue Empty Except On Player's Turn

    void Update()
    {
        if ((current_turn > -1 && current_turn % turn_phases != 4) && selectionManager.GetQueueEmpty() == true)
        {
            IncrementTurn();
        }
    }

    // Start And Stop Combat
    // Only activates if currently in opposing state

    public void InitiateCombat(int id)
    {
        if (current_turn == -1)
        {
            List<(int, int)> enemy_list;

            // For Testing Loading Cards
            List<int> card_list;

            switch (id)
            {
                case 0:
                    enemy_list = new List<(int, int)>() { (1, 2), (0, 1), (0, 1), (0, 0) };
                    card_list = new List<int>() { 0, 0, 1, 1, 50, 50, 51, 51 };
                    break;
                case 1:
                    enemy_list = new List<(int, int)>() { (0, 3), (0, 2), (0, 1) };
                    card_list = new List<int>() { 0, 0, 0, 0, 1, 1, 1 };
                    break;
                case 2:
                    enemy_list = new List<(int, int)>() { (0, 2), (0, 1) };
                    card_list = new List<int>() { 0, 0, 1 };
                    break;
                default:
                    enemy_list = new List<(int, int)>();
                    card_list = new List<int>();
                    UnityEngine.Debug.LogError("No matching combat id for id " + id);
                    break;
            }

            enemyManager.LoadEnemies(enemy_list);

            // For Testing Loading Cards
            cardManager.LoadList(card_list);

            cardManager.GenerateDeck();
            playerManager.ResetStats();
            cameraManager.StartCombat();
            IncrementTurn();
        }
    }

    public void ResetCombat()
    {
        if (current_turn > -1)
        {
            cardManager.Reset();
            enemyManager.Reset();
            selectionManager.Reset();
            playerManager.ResetStats();
            cameraManager.EndCombat();
            current_turn = -1;
        }
    }

    // Player Turn Complete

    public void PlayerEndTurn()
    {
        if (current_turn % turn_phases == 4)
        {
            IncrementTurn();
        }
    }

    // Testing

    public void TestIncrementTurn()
    {
        IncrementTurn();
    }

    // Increment Turn Phase
    // Turns cycle through all phases before proceeding to the next.

    private void IncrementTurn()
    {
        switch (++current_turn % turn_phases)
        {
            case 0:
                enemyManager.ResetDefense();
                enemyManager.SendUpkeepEffects();
                break;
            case 1:
                enemyManager.ActivateEnemies(current_turn / turn_phases);
                enemyManager.PerformActions();
                break;
            case 2:
                break;
            case 3:
                enemyManager.UpdateActionFlags();
                cardManager.ResetPlayCardActions();
                playerManager.ResetMana();
                playerManager.ResetDefense();
                cardManager.ResetDefense();
                cardManager.SendUpkeepEffects();
                if (enemyManager.GetEnemiesCleared() || !playerManager.GetActive())
                {
                    gameManager.EndCombat();
                }
                break;
            case 4:
                if (current_turn < 5)
                {
                    cardManager.DrawCards(playerManager.GetStartingDraw());
                }
                else
                {
                    cardManager.DrawCards(playerManager.GetNormalDraw());
                }
                break;
            case 5:
                break;
            default:
                UnityEngine.Debug.LogError("No matching turn phase for current_turn " + current_turn);
                break;
        }
        if(current_turn > -1)
        {
            UpdateTurnText();
        }
    }

    // Update Turn Display Text

    private void UpdateTurnText()
    {
        string round = "Round " + (current_turn / turn_phases + 1);
        string turn = "";
        switch (current_turn % turn_phases)
        {
            case 0:
                turn = "Enemy Upkeep";
                break;
            case 1:
                turn = "Enemy Turn";
                break;
            case 2:
                turn = "Enemy Cleanup";
                break;
            case 3:
                turn = "Player Upkeep";
                break;
            case 4:
                turn = "Player Turn";
                break;
            case 5:
                turn = "Player Cleanup";
                break;
            default:
                UnityEngine.Debug.LogError("No matching turn phase for current_turn " + current_turn);
                break;
        }
        turn_text.text = round + ":\n" + turn;
    }
}
