using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatScript : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            _playerController.TakeDamage(_playerController._health - _playerController._maxHealth, _playerController);
        }
    }
}
