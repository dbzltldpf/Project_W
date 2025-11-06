namespace S
{
    using System.Collections.Generic;
    public class InputStates
    {
        public IInputState Default { get; private set; }
        public IInputState ToolSelector { get; private set; }
        public IInputState PotionSelector { get; private set; }
        public IInputState PotionThrow { get; private set; }
        public InputStates() 
        {
            Default = new DefaultInputSystem();
            ToolSelector = new ToolSelectorInputSystem();
            PotionSelector = new PotionSelectorInputSystem();
            PotionThrow = new PotionThrowInputSystem();
        }
        public IEnumerable<IInputState> GetAll()
        {
            return new IInputState[] { Default, ToolSelector, PotionSelector, PotionThrow };
        }
    }
}
