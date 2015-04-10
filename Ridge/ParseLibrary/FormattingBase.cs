namespace ParseLibrary
{
    public abstract class FormattingBase
    {
        public abstract string ToString(Formatting formatting, int spaceNumber = 4);

        public override string ToString()
        {
            return ToString(Formatting.None);
        }
    }
}