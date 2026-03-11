namespace GameRPG
{
    public class GoblinStatsManage : EnemyStatsManager
    {
        private Goblin goblin;

        protected override void Awake()
        {
            base.Awake();

        }
        protected override void Start()
        {
            base.Start();
            goblin = GetComponent<Goblin>();
        }

        public override void TakeDamage(int amout)
        {
            base.TakeDamage(amout);
            goblin.Anim.Play("Stun");
        }

        public override void Die()
        {
            base.Die();
        }

    }
}
