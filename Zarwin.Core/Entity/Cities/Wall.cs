
namespace Zarwin.Core.Entity.Cities
{
    public class Wall
    {
        public int HealthPoints { get; private set; }

        public Wall()
        {
            this.HealthPoints = 10;
        }

        public Wall(int health)
        {
            this.HealthPoints = health;
        }

        public void Hurt(int health)
        {
            this.HealthPoints = health > this.HealthPoints ? 0 : this.HealthPoints - health;
        }
    }
}
