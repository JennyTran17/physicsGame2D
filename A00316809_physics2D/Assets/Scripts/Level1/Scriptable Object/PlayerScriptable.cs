using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects")]
public class PlayerScriptable : ScriptableObject
{
    public float health = 100;
    public float kill = 0;
    public float gems = 0;
    public float time = 0;
    public int score = 0;
    public List<int> highScore;
}
