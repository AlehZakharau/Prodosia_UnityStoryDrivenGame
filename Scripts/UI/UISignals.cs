namespace UI
{
    public struct OpenWindowSignal
    {
        public WindowType WindowType;
        public OpenWindowSignal(WindowType windowType)
        {
            WindowType = windowType;
        }
    }
}