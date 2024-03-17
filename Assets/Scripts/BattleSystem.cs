using deVoid.UIFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Dugeon.Signal;
using deVoid.Utils;
using static Dugeon.Signal.SignalDefine;
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public enum CharacterState { WAITING, BUSY }
public class BattleSystem : MonoBehaviour
{
    public CharacterState CharacterState;
    public BattleState BattleState;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemySpawn;
    [SerializeField] private Transform _playerSpawn;
    private Unit _playerUnit;
    private Unit _enemyUnit;
    private Unit _characterSelect;
    private bool _isDead;
    private void Awake()
    {
    }
    void Start()
    {
        StartCoroutine(BattleSetup());

    }
    private IEnumerator BattleSetup()
    {

        //spawn player
        GameObject goPlayer = null;
        goPlayer = Instantiate(_playerPrefab);
        goPlayer.transform.position = _playerSpawn.position;
        goPlayer.TryGetComponent<Unit>(out _playerUnit);
        _playerUnit.InitalTransform = _playerSpawn.transform;
        //spawn enemy
        GameObject goEnemy;
        goEnemy = Instantiate(_enemyPrefab);
        goEnemy.transform.position = _enemySpawn.position;
        goEnemy.TryGetComponent<Unit>(out _enemyUnit);
        _enemyUnit.InitalTransform = _enemySpawn.transform;
        yield return new WaitUntil(() => _enemyUnit != null);
        UIManager.Instance.uIFrame.ShowPanel("UI_Battle",
            new UI_BattleSystemProperties { Enemy = _enemyUnit, Player = _playerUnit, BattleSystem = this });
        _characterSelect = _playerUnit;
    }



    public void AttackFunc()
    {
        ChooseNextActiveUnit();

    }

    public void AttackCharacter(Unit character, Unit target, Action onCompleteActtack)
    {
        character.SlideToTarget(target);
        target.TakeDamage(character.Damage);
        character.SlideToBack(character.InitalTransform);
        Signals.Get<HandleHealthyBar>().Dispatch();
        onCompleteActtack();
        Debug.Log("AttackCharacter");
    }

    private IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1);
        AttackCharacter(_enemyUnit, _playerUnit, () =>
        {
            CharacterState = CharacterState.WAITING;
        });
    }
    private Unit SelectActiveTurn(Unit character)
    {
        return _characterSelect = character;
    }

    private void ChooseNextActiveUnit()
    {
        if (_characterSelect == _playerUnit)
        {
            _characterSelect = SelectActiveTurn(_enemyUnit) as Unit;
            AttackCharacter(_playerUnit, _enemyUnit, () =>
              {
                  CharacterState = CharacterState.BUSY;
                  ChooseNextActiveUnit();

              });
        }
        else
        {
            _characterSelect = SelectActiveTurn(_playerUnit) as Unit;
            StartCoroutine(EnemyTurn());
        }
    }
}
