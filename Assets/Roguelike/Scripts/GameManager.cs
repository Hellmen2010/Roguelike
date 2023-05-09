using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private float _time = 0.1f;
    [SerializeField] private bool _isPlayerTurn = true;
    [SerializeField] private int _entityNum = 0;
    [SerializeField] private List<Entity> _entities = new List<Entity>();

    public bool IsPlayerTurn => _isPlayerTurn;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void StartTurn()
    {
        if (_entities[_entityNum].GetComponent<Player>()) _isPlayerTurn = true;
        else if (_entities[_entityNum].IsSentient) Action.SkipAction(_entities[_entityNum]);
    }

    public void EndTurn()
    {
        if (_entities[_entityNum].GetComponent<Player>()) _isPlayerTurn = false;
        
        if (_entityNum == _entities.Count - 1) _entityNum = 0;
        else _entityNum++;

        StartCoroutine(TurnDelay());
    }

    private IEnumerator TurnDelay()
    {
        yield return new WaitForSeconds(_time);
        StartTurn();
    }

    public void AddEntity(Entity entity) => _entities.Add(entity);

    public void InsertEntity(Entity entity, int i) => _entities.Insert(0, entity);
}
