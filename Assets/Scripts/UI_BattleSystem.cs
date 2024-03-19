using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine.UI;
using System;
using Dugeon.Signal;

namespace Dugeon
{

    [Serializable]
    public class UI_BattleSystemProperties : PanelProperties
    {
        public Unit Enemy;
        public Unit Player;
        public BattleSystem BattleSystem;
    }

    public class UI_BattleSystem : APanelController<UI_BattleSystemProperties>
    {
        [SerializeField] private Button _attackBtn;
        [SerializeField] private Button _healkBtn;
        [SerializeField] private Slider _playerHealth;
        [SerializeField] private Slider _enemyHealth;

        protected override void OnPropertiesSet()
        {
            base.OnPropertiesSet();

        }

        protected override void AddListeners()
        {
            base.AddListeners();

            _attackBtn?.onClick.AddListener(() => { Properties.BattleSystem.AttackFunc(); });
            Signals.Get<HandleHealthyBar>().AddListener(OnClickActtackBtn);

        }
        private void OnClickActtackBtn()
        {
            _enemyHealth.value = (float)Properties.Enemy.CurrentHP / Properties.Enemy.HealtPointMax;

            _playerHealth.value = (float)Properties.Player.CurrentHP / Properties.Player.HealtPointMax;

        }
        protected override void RemoveListeners()
        {
            base.RemoveListeners();
            Signals.Get<HandleHealthyBar>().RemoveListener(OnClickActtackBtn);

        }


    }
}
