using UnityEngine;
using System.Collections;

public class GlobalSettings
{
        private static GlobalSettings instance;
        public static GlobalSettings Instance
        {
            get
            {
                if(instance == null)
                    instance = new GlobalSettings();
                return instance;
            }
        }
        
        // ===== Army Settings =====
        public float horizontalMovementLong = 1.6f;
        public float horizontalMovementShort = 0.8f;
        public float verticalMovement = 1.3f;
        public int starvingFighters = 5;
        public float restartLevelDelay = 1f;
        public int ArmySpawnPositionX = 0;
        public int ArmySpawnPositionY = 1;
        
        // ===== Board Settings =====
        public int boardLength = 45;
        public int groundLength = 38;
        public int keyGroundLength = 7;
        public int transientFieldsLength = 7;
        public int rows = 13;
        public int shortColumn = 3;
        public int longColumn = 4;
        public float positionFactorX = 0.8f;
        public float positionFactorY = 2.6f;
        public float shortPositionOffset = 1.2f;
        public int loopOffset = -1;
        
        // ===== General Game Settings =====
        public float levelStartDelay = 3.5f;
        public int startingLevel = 1;
        public string startingEquipment = "light";
        public int startingGold = 10;
        public int startingFighters = 10;
        public int startingFood = 50;
        
        // ===== Level Settings =====
        public int hideout1XPos = 6;
        public int hideout1YPos = 0;
        public int hideout2XPos = 4;
        public int hideout2YPos = 2;
        public int hideout3XPos = 10;
        public int hideout3YPos = 2;
        public int cityPositionXPos = 10;
        public int cityPositionYPos = 2;
        public int asylumPositionXPos = 10;
        public int asylumPositionYPos = 2;
        
        // ===== Equipment Factor Settings =====
        public float attackFactorLight = 1.0f;
        public float defenseFactorLight = 1.0f;
        public float attackFactorMedium = 1.1f;
        public float defenseFactorMedium = 0.9f;
        public float attackFactorHeavy = 1.2f;
        public float defenseFactorHeavy = 0.8f;
        public float attackFactorSuperior = 1.3f;
        public float defenseFactorSuperior = 0.7f;
}