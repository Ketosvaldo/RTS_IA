using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public int playerScore;
    public int playerHealth;
    public Vector3 playerPosition;
    public List<EnemyState> enemies;
    // Agrega otros estados relevantes del juego

    [System.Serializable]
    public class EnemyState
    {
        public Vector3 position;
        public int health;
    }
}