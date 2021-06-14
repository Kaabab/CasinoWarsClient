using UnityEngine;


public class PreferencesManager
{
    public const string GameSessionIdKey = "GameSessionId";
    public const string PlayerIdKey = "PlayerId";
    public const string PlayerNameKey = "PlayerName";
    
    public string GetPlayerId()
    {
        return (PlayerPrefs.GetString(PlayerIdKey, null));
    }

    public void SetPlayerId(string playerId)
    {
        PlayerPrefs.SetString(PlayerIdKey, playerId);
        PlayerPrefs.Save();
    }

    public void SetGameSessionId(string sessionId)
    {
        PlayerPrefs.SetString(GameSessionIdKey, sessionId);
        PlayerPrefs.Save();
    }

    public string GetGameSessionId()
    {
        return (PlayerPrefs.GetString(GameSessionIdKey, null));
    }

    public string GetPlayerName()
    {
        return (PlayerPrefs.GetString(PlayerNameKey, null));
    }

    public void SetPlayerName(string playerName)
    {
        PlayerPrefs.SetString(PlayerNameKey, playerName);
        PlayerPrefs.Save();
    }
}
