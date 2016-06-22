using CheckTextFileEncodings.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CheckTextFileEncodings
{
    public class Model : INotifyPropertyChanged
    {
        private byte[] content;
        private string currentEncoding;
        private string decodedText;
        private readonly IDictionary<string, string> encodingToTextDictionary = new Dictionary<string, string>();
        static Encoding[] encodings = Encoding.GetEncodings().Select(ei =>
        {
            var encoding = (Encoding)ei.GetEncoding().Clone();

            encoding.DecoderFallback = new DecoderExceptionFallback();

            return encoding;

        }).ToArray();

        public event PropertyChangedEventHandler PropertyChanged;

        public string FilePath
        {
            get;
            set;
        }

        public byte[] Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;

                encodingToTextDictionary.Clear();
                foreach (var encoding in encodings)
                {
                    encoding.DecoderFallback = new DecoderExceptionFallback();

                    try
                    {
                        encodingToTextDictionary.Add(encoding.EncodingName, encoding.GetString(content));
                    }
                    catch (Exception)
                    {
                        // Ignored.
                    }
                }

                OnPropertyChanged(nameof(AvailableEncodings));
            }
        }

        public IEnumerable<string> AvailableEncodings => encodingToTextDictionary.Keys.OrderBy(e => e);

        public string CurrentEncoding
        {
            get
            {
                return currentEncoding;
            }
            set
            {
                currentEncoding = value;

                if (currentEncoding != null)
                {
                    DecodedText = encodingToTextDictionary[currentEncoding];
                }
            }
        }

        public string DecodedText
        {
            get
            {
                return decodedText;
            }
            set
            {
                decodedText = value;

                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Commit()
        {
            try
            {
                Content = File.ReadAllBytes(FilePath);
            }
            catch (Exception)
            {
                // Ignored.
            }
        }
    }
}
