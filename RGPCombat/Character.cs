using static System.Net.Mime.MediaTypeNames;

namespace RGPCombat
{
    public class Character
    {
        public double Health { get; set; } = 1000;
        public int Level { get; set; } = 1;
        public bool IsAlive { get; set; } = true; 

        public void Attack(Character victim, double damage)
        {
            if (victim.Level <= this.Level - 5)
            {
                damage = damage *= 1.5;
            }

            if (victim.Level >= this.Level + 5)
            {
                damage = damage / 2;
            }
            if (victim == this)
            {
                throw new InvalidOperationException();
            }
            //set health of victim to current health minus the damage.
            victim.Health -= damage;

            if (victim.Health <= 0)
            {
                victim.IsAlive = false;
            }
            if (victim.Health > 1000)
            {
                victim.Health = 1000;
            } 

        }

        public void Heal(Character target, int health)
        {
            //if character tries to heal another character, throw an execption
            if (target != this)
            {
                throw new InvalidOperationException();
            }
            if (target.IsAlive)
            {
                target.Health += health;
            }
            if (target.Health > 1000)
            {
                target.Health = 1000;
            }
        }
    }
}