using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Button startButton;
        [SerializeField] private PlayerService playerService;
        [SerializeField] private BulletSystem bulletSystem;
        [SerializeField] private BulletConfig bulletConfig;
        [SerializeField] private EnemyPool enemyPool;
        [SerializeField] private EnemyCooldownSpawner enemyCooldownSpawner;

        public override void InstallBindings()
        {
            BulletSystemInstaller();
            PlayerInstaller();
            GameManagerInstaller();
            EnemySystemInstaller();
        }

        private void BulletSystemInstaller()
        {
            Container.Bind<BulletConfig>().FromInstance(bulletConfig).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BulletSystem>().FromInstance(bulletSystem).AsSingle().NonLazy();
        }

        private void PlayerInstaller()
        {
            Container.Bind<PlayerService>().FromInstance(playerService).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterMoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterAttackController>().AsSingle().NonLazy();
        }

        private void GameManagerInstaller()
        {
            Container.Bind<GameManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StartButtonListener>().AsSingle().WithArguments(startButton).NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterDeathObserver>().AsSingle().NonLazy();
        }

        private void EnemySystemInstaller()
        {
            Container.Bind<EnemyPool>().FromInstance(enemyPool).AsSingle().NonLazy();
            Container.Bind<EnemyManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyCooldownSpawner>().AsSingle().NonLazy();
        }
    }
}