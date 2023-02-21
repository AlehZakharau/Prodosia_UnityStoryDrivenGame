using Gameplay;

namespace Core.States
{
    public class Skipper
    {
        private readonly LineScriptController lineController;
        private readonly CommentsController commentsController;
        private readonly DecorationsController decorationsController;

        public Skipper(LineScriptController lineController, CommentsController commentsController, 
            DecorationsController decorationsController)
        {
            this.lineController = lineController;
            this.commentsController = commentsController;
            this.decorationsController = decorationsController;
        }

        public void Skip()
        {
            if (lineController.IsSkippable)
            {
                decorationsController.StopShake();    
                lineController.StopPlayLine();
                lineController.Skip();
            }
            if (lineController.IsSkippableComment)
            {
                decorationsController.StopShake();
                lineController.StopPlayLine();
                commentsController.Skip();
            }
        }
    }
}