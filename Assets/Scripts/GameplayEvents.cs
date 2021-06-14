using System;
using UnityEngine;

public class GameplayEvents : MonoBehaviour
{
    public static event Action<string> CreateGameSession = delegate { };
    public static event Action<Player> GetPlayerResponse = delegate { };
    public static event Action<Player> CreatePlayerResponse = delegate { };
    public static event Action<BetResponse, Player> BetResponse = delegate { };

    public static void DispatchGetPlayerResponse(Player player)
    {
        Debug.Log("Dispatching get player " + player);
        GetPlayerResponse(player);
    }

    public static void DispatchCreatePlayerResponse(Player player)
    {
        Debug.Log("Dispatching create player " + player);
        CreatePlayerResponse(player);
    }
    
    public static void DispatchCreateGameSessionResponse(String sessionId)
    {
        Debug.Log("DispatchCreateGameSessionResponse " + sessionId);
        CreateGameSession(sessionId);
    }

    public static void DispatchBetResponse(BetResponse betResponse, Player player)
    {
        Debug.Log("betResponse " + betResponse);
        BetResponse(betResponse, player);
    }


}
