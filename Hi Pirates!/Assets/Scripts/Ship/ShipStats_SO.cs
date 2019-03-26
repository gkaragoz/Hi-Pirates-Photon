using UnityEngine;

[CreateAssetMenu(fileName = "Ship Stats", menuName = "Scriptable Objects/Ship Stats")]
public class ShipStats_SO : ScriptableObject {

    #region Properties

    public bool isPlayer = false;

    [SerializeField]
    private string _name = string.Empty;

    [SerializeField]
    private int _maxHealth = 0;
    [SerializeField]
    private int _currentHealth = 0;
    [SerializeField]
    private float _healthRegen = 0;

    [SerializeField]
    private int _attackDamage = 5;
    [SerializeField]
    private float _attackSpeed = 1.0f;
    [SerializeField]
    private float _movementSpeed = 3;
    [SerializeField]
    private float _rotationSpeed = 1.75f;
    [SerializeField]
    private float _attackRange = 1;

    [SerializeField]
    private int _level = 0;
    [SerializeField]
    private int _maxExperience = 0;
    [SerializeField]
    private int _currentExperience = 0;

    #endregion

    #region Getter Setters

    public string Name {
        get {
            return _name;
        }

        set {
            _name = value;
        }
    }

    public int MaxHealth {
        get {
            return _maxHealth;
        }

        set {
            _maxHealth = value;
        }
    }

    public int CurrentHealth {
        get {
            return _currentHealth;
        }

        set {
            _currentHealth = value;
        }
    }

    public float HealthRegen {
        get {
            return _healthRegen;
        }

        set {
            _healthRegen = value;
        }
    }

    public int AttackDamage {
        get {
            return _attackDamage;
        }

        set {
            _attackDamage = value;
        }
    }

    public float AttackSpeed {
        get {
            return _attackSpeed;
        }

        set {
            _attackSpeed = value;
        }
    }

    public float AttackRange {
        get {
            return _attackRange;
        }

        set {
            _attackRange = value;
        }
    }

    public float MovementSpeed {
        get {
            return _movementSpeed;
        }

        set {
            _movementSpeed = value;
        }
    }

    public float RotationSpeed {
        get {
            return _rotationSpeed;
        }

        set {
            _rotationSpeed = value;
        }
    }

    public int Level {
        get {
            return _level;
        }

        set {
            _level = value;
        }
    }

    public int MaxExperience {
        get {
            return _maxExperience;
        }

        set {
            _maxExperience = value;
        }
    }

    public int CurrentExperience {
        get {
            return _currentExperience;
        }

        set {
            _currentExperience = value;
        }
    }

    #endregion

}
