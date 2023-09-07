using System;
using System.IO;
using System.Text;

namespace DataDevelop.IO
{
	public delegate void StreamWriteHandler(byte[] buffer, int offset, int count, Encoding encoding);

	public class StreamWriteDelegator : Stream
	{
		private readonly StreamWriteHandler write;
		private readonly Encoding encoding;

		public StreamWriteDelegator(StreamWriteHandler write, Encoding encoding)
		{
			this.write = write ?? throw new ArgumentNullException(nameof(write));
			this.encoding = encoding;
		}

		public override bool CanRead => false;
		
		public override bool CanSeek => false;
		
		public override bool CanWrite => true;

		public override long Length => throw new NotSupportedException();

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

		public override void Write(byte[] buffer, int offset, int count)
		{
			write(buffer, offset, count, encoding);
		}

		public override void Flush() { }

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}
	}
}
