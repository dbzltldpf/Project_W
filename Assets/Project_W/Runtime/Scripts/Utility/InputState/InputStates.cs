namespace S
{
    using System.Collections.Generic;
    public class InputStates
    {
        public IInputState Default { get; private set; }
        public IInputState ToolSelector { get; private set; }
        public InputStates() 
        {
            Default = new DefaultInputSystem();
            ToolSelector = new TooSelectorInputSystem();
        }
        public IEnumerable<IInputState> GetAll()
        {
            return new IInputState[] { Default, ToolSelector };
        }
    }
}
