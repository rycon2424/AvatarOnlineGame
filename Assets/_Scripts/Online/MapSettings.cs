using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MapSettings
{
    public string SceneName;

    [Header ("Gamemodes")]
    public List<bool> selectGameMode = new List<bool>();

    public void UpdateList()
    {
        Debug.Log(GameModeEnum.GetNames(typeof(GameModeEnum)).Length);
        if (GameModeEnum.GetNames(typeof(GameModeEnum)).Length != selectGameMode.Count)
        {
            selectGameMode.Clear();
            for (int i = 0; i < GameModeEnum.GetNames(typeof(GameModeEnum)).Length; i++)
            {
                selectGameMode.Add(false);
            }
        }
    }
}
