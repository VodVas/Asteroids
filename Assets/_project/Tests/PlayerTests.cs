//using NUnit.Framework;
//using UnityEngine;

//namespace AsteroidsClone.Tests
//{
//    public class PlayerTests : BaseTest
//    {
//        private ScreenConfig _screenConfig;
//        private PlayerConfig _playerConfig;
//        private WeaponsConfig _weaponsConfig;
//        private Player _player;

//        [SetUp]
//        public void SetUp()
//        {
//            _screenConfig = TestHelpers.CreateValidScreenConfig();
//            _playerConfig = TestHelpers.CreateValidPlayerConfig();
//            _weaponsConfig = TestHelpers.CreateValidWeaponsConfig();
//            _player = new Player(_screenConfig, _playerConfig, _weaponsConfig);
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            if (_screenConfig != null) UnityEngine.Object.DestroyImmediate(_screenConfig);
//            if (_playerConfig != null) UnityEngine.Object.DestroyImmediate(_playerConfig);
//            if (_weaponsConfig != null) UnityEngine.Object.DestroyImmediate(_weaponsConfig);
//        }

//        [Test]
//        public void Constructor_InitializesPlayerAtOriginAlive()
//        {
//            Assert.AreEqual(Vector2.zero, _player.Position);
//            Assert.AreEqual(Vector2.zero, _player.Velocity);
//            Assert.AreEqual(0f, _player.Rotation);
//            Assert.IsTrue(_player.IsAlive);
//            Assert.AreEqual(_weaponsConfig.MaxLaserCharges, _player.LaserCharges);
//        }

//        [Test]
//        public void Rotate_UpdatesRotationCorrectly()
//        {
//            _player.Rotate(1f, 1f);
            
//            Assert.AreEqual(_playerConfig.PlayerRotationSpeed, _player.Rotation);
//        }

//        [Test]
//        public void Rotate_NormalizesRotationTo360Range()
//        {
//            _player.Rotate(1f, 3f);
            
//            Assert.AreEqual(180f, _player.Rotation);
//        }

//        [Test]
//        public void Thrust_AcceleratesPlayer()
//        {
//            _player.Thrust(true, 1f);
            
//            Assert.Greater(_player.Velocity.magnitude, 0f);
//            Assert.IsTrue(_player.IsThrusting);
//        }

//        [Test]
//        public void Thrust_CapsSpeedAtMaxSpeed()
//        {
//            for (int i = 0; i < 10; i++)
//            {
//                _player.Thrust(true, 1f);
//            }
            
//            Assert.LessOrEqual(_player.Velocity.magnitude, _playerConfig.PlayerMaxSpeed + 0.01f);
//        }

//        [Test]
//        public void Thrust_False_AppliesDrag()
//        {
//            _player.Thrust(true, 1f);
//            var velocityAfterThrust = _player.Velocity;
            
//            _player.Thrust(false, 1f);
            
//            Assert.Less(_player.Velocity.magnitude, velocityAfterThrust.magnitude);
//            Assert.IsFalse(_player.IsThrusting);
//        }

//        [Test]
//        public void UpdatePosition_WrapsAroundScreen_RightEdge()
//        {
//            var halfWidth = _screenConfig.ScreenWidth / 2f;
//            var startX = halfWidth + 1f;
//            var expectedX = -halfWidth;
            
//            SetPlayerPosition(startX, 0f);
//            _player.UpdatePosition(0f);
            
//            Assert.AreEqual(expectedX, _player.Position.x, 0.01f);
//            Assert.AreEqual(0f, _player.Position.y, 0.01f);
//        }

//        [Test]
//        public void UpdatePosition_WrapsAroundScreen_LeftEdge()
//        {
//            var halfWidth = _screenConfig.ScreenWidth / 2f;
//            var startX = -halfWidth - 1f;
//            var expectedX = halfWidth;
            
//            SetPlayerPosition(startX, 0f);
//            _player.UpdatePosition(0f);
            
//            Assert.AreEqual(expectedX, _player.Position.x, 0.01f);
//            Assert.AreEqual(0f, _player.Position.y, 0.01f);
//        }

//        [Test]
//        public void UpdatePosition_WrapsAroundScreen_TopEdge()
//        {
//            var halfHeight = _screenConfig.ScreenHeight / 2f;
//            var startY = halfHeight + 1f;
//            var expectedY = -halfHeight;
            
//            SetPlayerPosition(0f, startY);
//            _player.UpdatePosition(0f);
            
//            Assert.AreEqual(0f, _player.Position.x, 0.01f);
//            Assert.AreEqual(expectedY, _player.Position.y, 0.01f);
//        }

//        [Test]
//        public void UpdatePosition_WrapsAroundScreen_BottomEdge()
//        {
//            var halfHeight = _screenConfig.ScreenHeight / 2f;
//            var startY = -halfHeight - 1f;
//            var expectedY = halfHeight;
            
//            SetPlayerPosition(0f, startY);
//            _player.UpdatePosition(0f);
            
//            Assert.AreEqual(0f, _player.Position.x, 0.01f);
//            Assert.AreEqual(expectedY, _player.Position.y, 0.01f);
//        }

//        private void SetPlayerPosition(float x, float y)
//        {
//            var positionField = typeof(Player).GetField("_position", 
//                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//            positionField.SetValue(_player, new Vector2(x, y));
//        }

//        [Test]
//        public void UpdateLaser_RechargesOverTime()
//        {
//            while (_player.TryFireLaser()) { }
//            Assert.AreEqual(0, _player.LaserCharges);
            
//            _player.UpdateLaser(_weaponsConfig.LaserRechargeTime);
            
//            Assert.AreEqual(1, _player.LaserCharges);
//        }

//        [Test]
//        public void TryFireLaser_ConsumesChargeWhenAvailable()
//        {
//            var initialCharges = _player.LaserCharges;
            
//            bool result = _player.TryFireLaser();
            
//            Assert.IsTrue(result);
//            Assert.AreEqual(initialCharges - 1, _player.LaserCharges);
//        }

//        [Test]
//        public void TryFireLaser_FailsWhenNoCharges()
//        {
//            while (_player.TryFireLaser()) { }
            
//            bool result = _player.TryFireLaser();
            
//            Assert.IsFalse(result);
//            Assert.AreEqual(0, _player.LaserCharges);
//        }

//        [Test]
//        public void Kill_SetsPlayerDead()
//        {
//            _player.Kill();
            
//            Assert.IsFalse(_player.IsAlive);
//        }

//        [Test]
//        public void Reset_RestoresInitialState()
//        {
//            _player.Thrust(true, 1f);
//            _player.Kill();
//            _player.TryFireLaser();
            
//            _player.Reset();
            
//            Assert.AreEqual(Vector2.zero, _player.Position);
//            Assert.AreEqual(Vector2.zero, _player.Velocity);
//            Assert.IsTrue(_player.IsAlive);
//            Assert.AreEqual(_weaponsConfig.MaxLaserCharges, _player.LaserCharges);
//        }
//    }
//}