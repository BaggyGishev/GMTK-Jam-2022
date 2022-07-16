using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gisha.GMTK2022.Core
{
    public class Dice : MonoBehaviour
    {
        [SerializeField] private Sprite[] _sideSprites = new Sprite[6];

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            StartCoroutine(DiceRoutine());
        }

        private IEnumerator DiceRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                _spriteRenderer.sprite = _sideSprites[Random.Range(0, _sideSprites.Length)];
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.CompareTag("Player"))
                Debug.Log("Roll of the dice!");
        }
    }
}