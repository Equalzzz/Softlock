using ConsoleSoftlock.InputInterface;

namespace ConsoleSoftlock.Building
{
    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
    public interface IInteractive
    {
        public bool CanBeInteracted(GameState state, InteractAction? action = null) => true;
        public void Interact(GameState state, InteractAction action);
    }
    public interface IDirectional
    {
        public Direction Direction { get; set; }
        public bool IsActivationDirectional { get; }
        public bool IsPlacementDirectional { get; }
    }
    public interface IUpdatable
    {
        /// <summary>
        /// Determines update order: 255 means first, 0 means last. Default is 127  
        /// </summary>
        public byte UpdatePriority => 127;
        //public virtual void OnUpdate(GameState state) { }
        public virtual void OnTurnStart(GameState state) { }
        public virtual void OnTurnEnd(GameState state) { }
    }

}
