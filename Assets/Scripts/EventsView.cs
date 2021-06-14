using System;
using UnityEngine.UI;

public class EventsView : GameView
{
    public Text eventsText;

    void Start()
    {
        AddListeners();   
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void OnPlayerRetrieved(Player player)
    {
        Log("Player Retreived " + player.ToString() + "\n");
    }

    private void OnPlayerCreated(Player player)
    {
        Log("Player Created " + player.ToString() + "\n");
    }

    private void OnGameSessionCreated(string id)
    {
        Log("GameSession Created " + id + "\n");
    }

    public void Log(string eventLog) {
        eventsText.text += eventLog + "\n";
    }
    
    private void RemoveListeners()
    {
        GameplayEvents.BetResponse -= OnBetResponse;
        GameplayEvents.CreateGameSession -= OnGameSessionCreated;
        GameplayEvents.CreatePlayerResponse -= OnPlayerCreated;
        GameplayEvents.GetPlayerResponse -= OnPlayerRetrieved;
    }

    private void OnBetResponse(BetResponse response, Player player)
    {
        String log = "BetReponse";
        if (response != null) {
            log += " result " + response.result;
        }
        if (player != null) {
            log += " newTotal" + player.tokenCount;
        }
        Log(log);
    }

    private void AddListeners()
    {
        GameplayEvents.BetResponse += OnBetResponse;
        GameplayEvents.CreateGameSession += OnGameSessionCreated;
        GameplayEvents.CreatePlayerResponse += OnPlayerCreated;
        GameplayEvents.GetPlayerResponse += OnPlayerRetrieved;
    }

}
