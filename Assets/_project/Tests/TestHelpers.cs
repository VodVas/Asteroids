//using UnityEngine;

//namespace AsteroidsClone.Tests
//{
//    public static class TestHelpers
//    {

//        public static ScreenConfig CreateValidScreenConfig()
//        {
//            var config = ScriptableObject.CreateInstance<ScreenConfig>();
//            SetAutoPropertyBackingField(config, "ScreenWidth", 20f);
//            SetAutoPropertyBackingField(config, "ScreenHeight", 15f);
//            return config;
//        }

//        public static PlayerConfig CreateValidPlayerConfig()
//        {
//            var config = ScriptableObject.CreateInstance<PlayerConfig>();
//            SetAutoPropertyBackingField(config, "PlayerAcceleration", 10f);
//            SetAutoPropertyBackingField(config, "PlayerMaxSpeed", 8f);
//            SetAutoPropertyBackingField(config, "PlayerRotationSpeed", 180f);
//            SetAutoPropertyBackingField(config, "PlayerDrag", 0.99f);
//            return config;
//        }

//        public static WeaponsConfig CreateValidWeaponsConfig()
//        {
//            var config = ScriptableObject.CreateInstance<WeaponsConfig>();
//            SetAutoPropertyBackingField(config, "BulletSpeed", 15f);
//            SetAutoPropertyBackingField(config, "BulletLifetime", 2f);
//            SetAutoPropertyBackingField(config, "BulletCooldown", 0.25f);
//            SetAutoPropertyBackingField(config, "BulletPositionOffset", 0.5f);
//            SetAutoPropertyBackingField(config, "BulletInheritVelocityFactor", 0.5f);
//            SetAutoPropertyBackingField(config, "VisualBulletRotationOffset", -90f);
//            SetAutoPropertyBackingField(config, "MaxLaserCharges", 3);
//            SetAutoPropertyBackingField(config, "LaserRechargeTime", 5f);
//            SetAutoPropertyBackingField(config, "LaserRange", 50f);
//            SetAutoPropertyBackingField(config, "LaserVisualActiveTime", 1f);
//            return config;
//        }

//        public static AsteroidConfig CreateValidAsteroidConfig()
//        {
//            var config = ScriptableObject.CreateInstance<AsteroidConfig>();
//            SetAutoPropertyBackingField(config, "AsteroidSpeeds", new float[] { 2f, 3f, 4f });
//            SetAutoPropertyBackingField(config, "AsteroidScores", new int[] { 20, 50, 100 });
//            SetAutoPropertyBackingField(config, "AsteroidFragments", 2);
//            SetAutoPropertyBackingField(config, "AsteroidColliderRadiusPerSize", 0.3f);
//            SetAutoPropertyBackingField(config, "AsteroidFragmentOffsetDistance", 0.5f);
//            SetAutoPropertyBackingField(config, "AsteroidVisualScaleFactor", 0.2f);
//            return config;
//        }

//        public static UfoConfig CreateValidUfoConfig()
//        {
//            var config = ScriptableObject.CreateInstance<UfoConfig>();
//            SetAutoPropertyBackingField(config, "UfoSpeed", 3f);
//            SetAutoPropertyBackingField(config, "UfoScore", 200);
//            SetAutoPropertyBackingField(config, "UfoColliderRadius", 0.5f);
//            return config;
//        }

//        public static SpawningConfig CreateValidSpawningConfig()
//        {
//            var config = ScriptableObject.CreateInstance<SpawningConfig>();
//            SetAutoPropertyBackingField(config, "InitialSpawnDelay", 3f);
//            SetAutoPropertyBackingField(config, "MinSpawnDelay", 0.5f);
//            SetAutoPropertyBackingField(config, "SpawnAcceleration", 0.95f);
//            SetAutoPropertyBackingField(config, "UfoSpawnDelayMultiplier", 3f);
//            SetAutoPropertyBackingField(config, "InitialAsteroidsCount", 3);
//            SetAutoPropertyBackingField(config, "DefaultAsteroidSize", 3);
//            SetAutoPropertyBackingField(config, "EdgeSpawnMargin", 1f);
//            return config;
//        }

//        public static ViewConfig CreateValidViewConfig()
//        {
//            var config = ScriptableObject.CreateInstance<ViewConfig>();
//            SetAutoPropertyBackingField(config, "AsteroidPoolInitial", 20);
//            SetAutoPropertyBackingField(config, "BulletPoolInitial", 30);
//            SetAutoPropertyBackingField(config, "UfoPoolInitial", 5);
//            SetAutoPropertyBackingField(config, "PlayerViewRotationOffset", 270f);
//            return config;
//        }

//        public static CollisionConfig CreateValidCollisionConfig()
//        {
//            var config = ScriptableObject.CreateInstance<CollisionConfig>();
//            SetAutoPropertyBackingField(config, "DefaultColliderRadius", 0.3f);
//            return config;
//        }

//        public static InputConfig CreateValidInputConfig()
//        {
//            var config = ScriptableObject.CreateInstance<InputConfig>();
//            SetAutoPropertyBackingField(config, "ThrustKey", KeyCode.W);
//            SetAutoPropertyBackingField(config, "BulletKey", KeyCode.Space);
//            SetAutoPropertyBackingField(config, "LaserKey", KeyCode.LeftShift);
//            SetAutoPropertyBackingField(config, "RestartKey", KeyCode.R);
//            SetAutoPropertyBackingField(config, "RotationAxis", "Horizontal");
//            return config;
//        }

        
//        /// <summary>
//        /// Устанавливает значение backing field автосвойства с [field: SerializeField]
//        /// </summary>
//        public static void SetAutoPropertyBackingField(object obj, string propertyName, object value)
//        {
//            // Компилятор C# создаёт backing fields для автосвойств с именами вида <PropertyName>k__BackingField
//            var backingFieldName = $"<{propertyName}>k__BackingField";
//            var field = obj.GetType().GetField(backingFieldName, 
//                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            
//            if (field != null)
//            {
//                field.SetValue(obj, value);
//            }
//            else
//            {
//                // Fallback: пытаемся найти старое именование приватного поля
//                var fallbackFieldName = $"_{char.ToLower(propertyName[0])}{propertyName.Substring(1)}";
//                var fallbackField = obj.GetType().GetField(fallbackFieldName,
//                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//                fallbackField?.SetValue(obj, value);
//            }
//        }
        
//    }
//}