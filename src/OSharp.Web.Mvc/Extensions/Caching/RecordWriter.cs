// -----------------------------------------------------------------------
//  <copyright file="RecordWriter.cs" company="OSharp开源团队">
//      Copyright (c) 2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2015-01-18 13:16</last-date>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;


namespace OSharp.Web.Mvc.Extensions
{
    internal class RecordWriter : TextWriter
    {
        private readonly TextWriter _innerWriter;
        private readonly List<StringBuilder> _recorders = new List<StringBuilder>();

        public RecordWriter(TextWriter innerWriter)
        {
            _innerWriter = innerWriter;
        }

        public override Encoding Encoding { get { return _innerWriter.Encoding; } }

        public override void Write(char value)
        {
            _innerWriter.Write(value);

            if (_recorders.Count > 0)
            {
                foreach (StringBuilder recorder in _recorders)
                {
                    recorder.Append(value);
                }
            }
        }

        public override void Write(string value)
        {
            if (value != null)
            {
                _innerWriter.Write(value);

                if (_recorders.Count > 0)
                {
                    foreach (StringBuilder recorder in _recorders)
                    {
                        recorder.Append(value);
                    }
                }
            }
        }

        public override void Write(char[] buffer, int index, int count)
        {
            _innerWriter.Write(buffer, index, count);

            if (_recorders.Count > 0)
            {
                foreach (StringBuilder recorder in _recorders)
                {
                    recorder.Append(buffer, index, count);
                }
            }
        }

        public void AddRecorder(StringBuilder recorder)
        {
            _recorders.Add(recorder);
        }

        public void RemoveRecorder(StringBuilder recorder)
        {
            _recorders.Remove(recorder);
        }
    }
}