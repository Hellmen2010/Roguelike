using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private bool _isSentient = false;
    
    public bool IsSentient => _isSentient;

    private void Start()
    {
        if (GetComponent<Player>()) GameManager.Instance.InsertEntity(this, 0);
        else if (IsSentient) GameManager.Instance.AddEntity(this);
    }

    public void Move(Vector2 direction) => transform.position += (Vector3)direction;
}
