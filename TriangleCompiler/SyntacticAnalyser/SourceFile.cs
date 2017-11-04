/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * CM4106 - Full Time: Languages and Compilers
 */

using System;
using System.IO;
using System.Collections.Generic;

namespace TriangleCompiler.SyntacticAnalyser
{
    public class SourceFile : IEnumerator<int>
    {
		// file will be stored here
        StreamReader _source;

		string _buffer;

		int _index;

		int _lineNumber;

		public string Name { get; private set; }

		public bool IsValid { get { return _source != null; } }

		public int Current { get { return _buffer == null ? -1 : _buffer[_index]; } }

        object System.Collections.IEnumerator.Current { get { return Current; } }

		
        // Constructor
        public SourceFile(string sourceFileName)
		{
			Name = sourceFileName;
			try
			{
				_source = new StreamReader(new FileStream(sourceFileName, FileMode.Open));
				Reset();
			}
			catch (FileNotFoundException)
			{
				_source = null;
			}
		}

        // Adds new line to buffer
		void ReadLine()
		{
			_buffer = _source.ReadLine();
			if (_buffer != null) { _buffer += "\n"; }

			_index = 0;
			_lineNumber++;
		}

        // resets source file buffer to origin of file and resets line index / number
		public void Reset()
		{
			if (_source == null) { throw new InvalidOperationException(); }
			if (!_source.BaseStream.CanSeek) { throw new NotSupportedException(); }

			_source.BaseStream.Seek(0L, SeekOrigin.Begin);
			_source.DiscardBufferedData();

			_index = 0;
			_lineNumber = 0;

			ReadLine();
		}

		public bool SkipRestOfLine()
		{
			_index = _buffer.Length;
			return MoveNext();
		}

		public bool MoveNext()
		{
			if (_buffer != null)
			{
				_index++;
				if (_index >= _buffer.Length)
				{
					ReadLine();
				}
			}

			return _buffer != null;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_source != null)
				{
					_source.Dispose();
					_source = null;
				}
			}
		}

        // return location of current line number and index
        public Location getLocation(){
            return new Location(this._lineNumber, this._index);
        }
    }
}
