namespace GameRPG
{
    public class GoblinTNTStatsManager : EnemyStatsManager
    {
        private GoblinTNT goblinTNT;

        protected override void Awake()
        {
            base.Awake();

        }
        protected override void Start()
        {
            base.Start();
            goblinTNT = GetComponent<GoblinTNT>();
        }

        public override void TakeDamage(int amout)
        {
            base.TakeDamage(amout);
            goblinTNT.Anim.Play("Stun");
        }

        public override void Die()
        {
            base.Die();
        }
    }
}
