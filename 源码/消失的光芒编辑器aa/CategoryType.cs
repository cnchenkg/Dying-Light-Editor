using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace 消失的光芒编辑器aa
{
    /// <summary>
    ///  商品类别类型。
    /// </summary>
     class CategoryType
    {
        /// <summary>
        ///  攻击速度（挥舞）。
        /// </summary>
        private string swingSpeed;
        /// <summary>
        ///  挥舞速度。
        /// </summary>
        public string SwingSpeed { get => swingSpeed; set => swingSpeed = value; }
        /// <summary>
        ///  物品最大堆叠数。
        /// </summary>
        protected string maxStackCount;
        /// <summary>
        ///  物品价格。
        /// </summary>
        protected string price;
        /// <summary>
        ///  伤害属性。
        /// </summary>
        protected string damage;
        /// <summary>
        ///  伤害范围。
        /// </summary>
        protected string damageRange;
        /// <summary>
        ///  物理伤害。
        /// </summary>
        protected string physicsDamage;
        /// <summary>
        ///  对物理对象的伤害。
        /// </summary>
        protected string damageToPhysicsObjects;
        /// <summary>
        ///  最小攻击伤害角度。
        /// </summary>
        protected string minDamageAngle;
        /// <summary>
        ///  最大攻击伤害角度。
        /// </summary>
        protected string maxDamageAngle;
        /// <summary>
        ///  最小攻击伤害相乘系数。
        /// </summary>
        protected string minDamageMult;
        /// <summary>
        ///  横向攻击伤害相乘系数。
        /// </summary>
        protected string horizontalAttackDamageMul;
        /// <summary>
        ///  攻击力大小。
        /// </summary>
        protected string damageSize;
        /// <summary>
        ///  颜色。
        /// </summary>
        protected string color;
        /// <summary>
        ///  声音类型。
        /// </summary>
        protected string noiseType;
        /// <summary>
        ///  消耗体力。
        /// </summary>
        protected string staminaUsage;
        /// <summary>
        ///  升级插槽。
        /// </summary>
        protected string equipmentSlot;

        /// <summary>
        ///  弹药基数。
        /// </summary>
        protected string ammoCount;
        /// <summary>
        ///  最大堆叠属性。
        /// </summary>
        public string MaxStackCount { get => maxStackCount; set => maxStackCount = value; }
        /// <summary>
        ///  价格。
        /// </summary>
        public string Price { get => price; set => price = value; }
        /// <summary>
        ///  伤害属性。
        /// </summary>
        public string Damage { get => damage; set => damage = value; }
        /// <summary>
        ///  伤害范围。
        /// </summary>
        public string DamageRange { get => damageRange; set => damageRange = value; }
        /// <summary>
        ///  物理伤害。
        /// </summary>
        public string PhysicsDamage { get => physicsDamage; set => physicsDamage = value; }
        /// <summary>
        ///  对屋里对象的伤害。
        /// </summary>
        public string DamageToPhysicsObjects { get => damageToPhysicsObjects; set => damageToPhysicsObjects = value; }
        /// <summary>
        ///  最小攻击角度。
        /// </summary>
        public string MinDamageAngle { get => minDamageAngle; set => minDamageAngle = value; }
        /// <summary>
        ///  最大攻击角度。
        /// </summary>
        public string MaxDamageAngle { get => maxDamageAngle; set => maxDamageAngle = value; }
        /// <summary>
        ///  最小伤害系数。
        /// </summary>
        public string MinDamageMult { get => minDamageMult; set => minDamageMult = value; }
        /// <summary>
        ///  横向伤害系数。
        /// </summary>
        public string HorizontalAttackDamageMul { get => horizontalAttackDamageMul; set => horizontalAttackDamageMul = value; }
        /// <summary>
        ///  伤害大小。
        /// </summary>
        public string DamageSize { get => damageSize; set => damageSize = value; }
        /// <summary>
        ///  颜色。
        /// </summary>
        public string Color { get => color; set => color = value; }
        /// <summary>
        ///  声音。
        /// </summary>
        public string NoiseType { get => noiseType; set => noiseType = value; }
        /// <summary>
        ///  体力消耗。
        /// </summary>
        public string StaminaUsage { get => staminaUsage; set => staminaUsage = value; }
        /// <summary>
        ///  升级插槽。
        /// </summary>
        public string EquipmentSlot { get => equipmentSlot; set => equipmentSlot = value; }
        /// <summary>
        ///  弹药基数。
        /// </summary>
        public string AmmoCount { get => ammoCount; set => ammoCount = value; }
     ////   public string[] GetValuableProperty()
     //   {
     //       string[] result = null;
     //       for (int i = 0; i < length; i++)
     //       {

     //       }
     //   }
    }
}
