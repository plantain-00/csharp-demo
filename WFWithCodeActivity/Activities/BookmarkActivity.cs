using System.Activities;

namespace WFWithCodeActivity.Activities
{
    public class BookmarkActivity : NativeActivity<bool>
    {
        [RequiredArgument]
        public InArgument<string> BookmarkName { get; set; }

        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }

        protected override void Execute(NativeActivityContext context)
        {
            context.CreateBookmark(BookmarkName.Get(context), OnResumeBookmark);
        }

        public void OnResumeBookmark(NativeActivityContext context, Bookmark bookmark, object obj)
        {
            Result.Set(context, (bool) obj);
        }
    }
}