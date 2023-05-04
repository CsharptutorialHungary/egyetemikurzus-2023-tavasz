using System;

namespace T4XJYT_LGI301.Core.Models
{
    public record Word
    {
        public string Text { get; }

        public int Length { get; }

        public Word(string text)
        {
            Text = text;
            Length = text.Length;
        }
    }
}

