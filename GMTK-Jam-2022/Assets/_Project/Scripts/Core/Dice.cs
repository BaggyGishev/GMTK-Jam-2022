using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gisha.GMTK2022.Core
{
    public enum DiceType
    {
        Master,
        EnemyType,
        EnemyCount,
        WeaponType,
        Location
    }

    public class Dice : MonoBehaviour
    {
        [SerializeField] private Sprite[] _sideSprites = new Sprite[6];
        [SerializeField] private float rollDelay = 1.5f;
        [SerializeField] private DiceType diceType;

        public static Action<DiceResult> DiceRolled;

        private int _result;
        private float _rollDelay;
        private SpriteRenderer _spriteRenderer;

        public DiceType DiceType => diceType;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            if (_rollDelay > 0)
                _rollDelay -= Time.deltaTime;
        }

        private int DiceRoll()
        {
            int result = Random.Range(1, 7);
            _spriteRenderer.sprite = _sideSprites[result - 1];
            return result;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.CompareTag("Player") && _rollDelay <= 0)
            {
                _result = DiceRoll();
                _rollDelay = rollDelay;
                Destroy(gameObject, 1f);
            }
        }

        private void OnDestroy()
        {
            var result = new DiceResult(_result, diceType);
            DiceRolled?.Invoke(result);
        }
    }

    [Serializable]
    public class DiceResult
    {
        public DiceType DiceType { get; private set; }
        public int Result { get; private set; }

        public DiceResult(int result, DiceType diceType)
        {
            DiceType = diceType;
            Result = result;
        }
    }
}