namespace GameRPG
{
    public class TreeManager : ResourceManager
    {
        public TreeStatManager treeStatManager;


        protected override void Awake()
        {
            base.Awake();
            treeStatManager = GetComponent<TreeStatManager>();
        }
        protected override void Start()
        {
            base.Start();
           
        }
    }
}
