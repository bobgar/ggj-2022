using System.Collections.Generic;
using UnityEngine;

namespace Bot
{
    public enum BotState
    {
        STARTING,
        FIGHTING,
        VICTORIOUS,
        DEFEATED
    }

    public class BotController : MonoBehaviour
    {
        //Note, for our game the target will be the other bot, probably always.
        //Body parts will grab this as needed to take actions on targets (aim, do damage, fire projectiles, etc.)
        //If we ever have more than two bots or more than two targets we may need to change this to an interface.
        public BotController target;

        //Keeping things flexible with a list of body parts for now.
        public BodyPart[] activeParts;

        public BodyPart[] allParts;

        public Collider[] childrenColliders;

        //TODO:  This will be calculated from the weapons.
        public float desiredDistance = 15f;
        public float acceptableDistnaceRange = 2.5f;
        private int _totalDamage;

        private int _totalHitpoints;
        [SerializeField] private string displayName;

        public Dictionary<Part, BodyPart> parts = new();

        public BotState State { get; private set; } = BotState.STARTING;

        // get method
        //set { name = value; }  // set method
        // Start is called before the first frame update
        private void Start()
        {
            childrenColliders = GetComponentsInChildren<Collider>();

            foreach (var b in allParts)
            {
                switch (b.name)
                {
                    case "Chest Piece":
                        parts.Add(Part.CHEST, b);
                        break;
                    case "Head_Basic":
                        parts.Add(Part.BASIC_HEAD, b);
                        break;
                    case "Wheels_2 pair":
                        parts.Add(Part.WHEELS, b);
                        break;
                    case "Tank Threads":
                        parts.Add(Part.TANK_TREADS, b);
                        break;
                    case "Sythe Arms L":
                        parts.Add(Part.SYTHE_ARM_LEFT, b);
                        break;
                    case "Sythe Arms R":
                        parts.Add(Part.SYTHE_ARM_RIGHT, b);
                        break;
                    case "Windmill Arm L":
                        parts.Add(Part.WINDMILL_ARM_LEFT, b);
                        break;
                    case "Windmill Arm R":
                        parts.Add(Part.WINDMILL_ARM_RIGHT, b);
                        break;
                    case "Hammer Hand L":
                        parts.Add(Part.HAMMER_ARM_LEFT, b);
                        break;
                    case "Hammer Hand R":
                        parts.Add(Part.HAMMER_ARM_RIGHT, b);
                        break;
                    case "Dragon Claw Feet":
                        parts.Add(Part.DRAGON_CLAW_FEET, b);
                        break;
                    case "Cannon Head":
                        parts.Add(Part.TANK_HEAD, b);
                        break;
                }

                b.gameObject.SetActive(false);
            }

            //Set the CHEST to active.
            parts[Part.CHEST].gameObject.SetActive(true);
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void LateUpdate()
        {
            if (_totalDamage >= _totalHitpoints) Lose();
        }

        public void AddPiece(Part part)
        {
            if (parts.ContainsKey(part)) parts[part].gameObject.SetActive(true);
        }

        public void RemovePiece(Part part)
        {
            if (parts.ContainsKey(part)) parts[part].gameObject.SetActive(false);
        }

        public Dictionary<Part, float> GetDamageByPart()
        {
            var damageByPart = new Dictionary<Part, float>();
            foreach (var bodyPart in allParts) damageByPart[partByName(bodyPart)] = bodyPart.GetHitPoints();

            return damageByPart;
        }

        private Part partByName(BodyPart bodyPart)
        {
            switch (bodyPart.name)
            {
                case "Chest Piece":
                    return Part.CHEST;
                case "Head_Basic":
                    return Part.BASIC_HEAD;
                case "Wheels_2 pair":
                    return Part.WHEELS;
                case "Tank Threads":
                    return Part.TANK_TREADS;
                case "Sythe Arms L":
                    return Part.SYTHE_ARM_LEFT;
                case "Sythe Arms R":
                    return Part.SYTHE_ARM_RIGHT;
                case "Windmill Arms L":
                    return Part.WINDMILL_ARM_LEFT;
                case "Windmill Arms R":
                    return Part.WINDMILL_ARM_RIGHT;
                case "Hammer Hand L":
                    return Part.HAMMER_ARM_LEFT;
                case "Hammer Hand R":
                    return Part.HAMMER_ARM_RIGHT;
                case "Dragon Claw Feet":
                    return Part.DRAGON_CLAW_FEET;
                case "Cannon Head":
                    return Part.TANK_HEAD;
            }

            // Hack to handle missing part
            return Part.CHEST;
        }

        public float GetDamage()
        {
            return _totalDamage;
        }

        //Even though we calculate damage per piece,
        //this accumulates global damage for if we want to show a health bar etc.
        public void TakeDamage(int damage)
        {
            _totalDamage += damage;
        }

        public void AddHitpoints(int hitpoints)
        {
            _totalHitpoints += hitpoints;
        }

        public float GetDesiredMinDistance()
        {
            return desiredDistance - acceptableDistnaceRange;
        }

        public float GetDesiredMaxDistance()
        {
            return desiredDistance + acceptableDistnaceRange;
        }

        public void Lose()
        {
            if (State == BotState.FIGHTING)
            {
                Destroy(gameObject.GetComponent<Rigidbody>());
                foreach (var b in activeParts)
                {
                    b.Deactivate();
                    b.gameObject.AddComponent<Rigidbody>();
                }

                State = BotState.DEFEATED;
            }
        }

        public void Win()
        {
            if (State == BotState.FIGHTING)
            {
                foreach (var b in activeParts) b.Deactivate();
                State = BotState.VICTORIOUS;
            }
        }

        public void StartBattle()
        {
            activeParts = gameObject.GetComponentsInChildren<BodyPart>(false);

            foreach (var bp in activeParts) AddHitpoints(bp.maxHitpoints);

            State = BotState.FIGHTING;
        }

        public float GetHealthPercentage()
        {
            if (_totalHitpoints > 0)
                return 1 - _totalDamage / (float)_totalHitpoints;
            return 1;
        }

        public string getDisplayName()
        {
            return displayName;
        }
    }
}