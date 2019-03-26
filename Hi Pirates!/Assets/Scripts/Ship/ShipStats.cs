using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ShipStats : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private ShipStats_SO _shipDefinition_Template;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private ShipStats_SO _ship;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    public Player Owner;

    #region Initializations

    private void Awake() {
        if (_shipDefinition_Template != null) {
            _ship = Instantiate(_shipDefinition_Template);
        }
    }

    #endregion

    public void Initialize() {
        Hashtable props = new Hashtable() { { GameVariables.PLAYER_HEALTH_FIELD, GameVariables.DEFAULT_PLAYER_HEALTH } };

        Owner.SetCustomProperties(props);

        //_char.Name = networkObject.PlayerData.Name;

        //_char.CurrentHealth = networkObject.PlayerData.CurrentHp;
        //_char.MaxHealth = networkObject.PlayerData.MaxHp;
        //_char.HealthRegen = AccountManager.instance.GetSelectedCharacter().stat.health_regen;

        //_char.AttackDamage = networkObject.PlayerData.AttackDamage;
        //_char.AttackSpeed = networkObject.PlayerData.AttackSpeed;
        //_char.AttackRange = networkObject.PlayerData.AttackRange;
        //_char.MovementSpeed = networkObject.PlayerData.MoveSpeed;

        //_char.Level = AccountManager.instance.GetSelectedCharacter().level;
        //_char.MaxExperience = ;
        //_char.CurrentExperience = AccountManager.instance.GetSelectedCharacter().exp;
    }

    #region Stat Increasers

    public void ApplyHealth(int healthAmount) {

        if ((_ship.CurrentHealth + healthAmount) > _ship.MaxHealth) {
            _ship.CurrentHealth = _ship.MaxHealth;
        } else {
            _ship.CurrentHealth += healthAmount;
        }
        Hashtable props = new Hashtable() { { GameVariables.PLAYER_HEALTH_FIELD, _ship.CurrentHealth } };
        Owner.SetCustomProperties(props);
    }

    public void AddAttackDamage(int damageAmount) {
        _ship.AttackDamage += damageAmount;
    }

    public void AddAttackSpeed(float speedAmount) {
        _ship.AttackSpeed += speedAmount;
    }

    public void AddAttackRange(float rangeAmount) {
        _ship.AttackRange += rangeAmount;
    }

    public void AddExp(int expAmount) {
        if (_ship.CurrentExperience + expAmount >= _ship.MaxExperience) {
            int needExpAmount = _ship.MaxExperience - _ship.CurrentExperience;
            int remainingExpAmount = expAmount - needExpAmount;

            LevelUp();

            if (remainingExpAmount > 0) {
                AddExp(remainingExpAmount);
            } else {
                _ship.CurrentExperience += needExpAmount;
            }
        } else {
            _ship.CurrentExperience += expAmount;
        }
    }

    #endregion

    #region Stat Reducers

    public void TakeDamage(int amount) {
        _ship.CurrentHealth -= amount;

        if (_ship.CurrentHealth <= 0) {
            _ship.CurrentHealth = 0;
        }
        Hashtable props = new Hashtable() { { GameVariables.PLAYER_HEALTH_FIELD, _ship.CurrentHealth } };
        Owner.SetCustomProperties(props);
    }

    public void ReduceAttackDamage(int damageAmount) {
        _ship.AttackDamage -= damageAmount;

        if (_ship.AttackDamage <= 0) {
            _ship.AttackDamage = 0;
        }
    }

    public void ReduceAttackSpeed(float speedAmount) {
        _ship.AttackSpeed -= speedAmount;

        if (_ship.AttackSpeed <= 0) {
            _ship.AttackSpeed = 0;
        }
    }

    public void ReduceAttackRange(float rangeAmount) {
        _ship.AttackRange -= rangeAmount;

        if (_ship.AttackRange <= 0) {
            _ship.AttackRange = 0;
        }
    }

    public void LooseExp(int expAmount) {
        _ship.CurrentExperience -= expAmount;

        if (_ship.CurrentExperience <= 0) {
            _ship.CurrentExperience = 0;
        }
    }

    #endregion

    #region Reporters

    public string GetName() {
        return _ship.Name;
    }

    public bool IsDeath() {
        return _ship.CurrentHealth <= 0;
    }

    public int GetMaxHealth() {
        return _ship.MaxHealth;
    }

    public int GetCurrentHealth() {
        return _ship.CurrentHealth;
    }

    public int GetAttackDamage() {
        return _ship.AttackDamage;
    }

    public float GetAttackSpeed() {
        return _ship.AttackSpeed;
    }

    public float GetMovementSpeed() {
        return _ship.MovementSpeed;
    }

    public float GetRotationSpeed() {
        return _ship.RotationSpeed;
    }

    public float GetAttackRange() {
        return _ship.AttackRange;
    }

    public int GetLevel() {
        return _ship.Level;
    }

    public int GetMaxExperience() {
        return _ship.MaxExperience;
    }

    public int GetCurrentExperience() {
        return _ship.CurrentExperience;
    }

    #endregion

    private void LevelUp() {
        _ship.Level++;

        _ship.CurrentExperience = 0;
        _ship.MaxExperience = 10 + (_ship.Level * 10) + (int)Mathf.Pow(_ship.Level + 1, 3);
    }

}
