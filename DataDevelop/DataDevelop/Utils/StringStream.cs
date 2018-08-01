using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DataDevelop.Utils
{
	public class StringStream : Stream
	{
		private StringWriter writer;
		private Encoding encoding;

		public StringStream()
		{
			this.writer = new StringWriter();
		}

		public StringStream(Encoding encoding)
			: this()
		{
			this.encoding = encoding ?? Encoding.Default;
		}

		public override bool CanRead { get { return false; } }
		public override bool CanSeek { get { return false; } }
		public override bool CanWrite { get { return true; } }

		public override long Length { get { throw new NotSupportedException(); } }

		public override long Position
		{
			get { throw new NotSupportedException(); }
			set { throw new NotSupportedException(); }
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		public string GetString()
		{
			return GetString(true);
		}

		public string GetString(bool clear)
		{
			StringBuilder b = writer.GetStringBuilder();
			string s = b.ToString();
			if (clear) {
				b.Length = 0;
			}
			return s;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			string s = encoding.GetString(buffer, offset, count);
			writer.Write(s);
		}

		public override void Flush()
		{
			writer.Flush();
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}
	}
}
