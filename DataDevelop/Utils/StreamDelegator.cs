using System;
using System.IO;

namespace DataDevelop.Utils
{
	delegate void StreamWriteHandler(byte[] buffer, int offset, int count);

	class StreamWriteDelegator : Stream
	{
		private readonly StreamWriteHandler write;

		public StreamWriteDelegator(StreamWriteHandler write)
		{
			this.write = write ?? throw new ArgumentNullException(nameof(write));
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
			write(buffer, offset, count);
		}

		public override void Flush() { }

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}
	}
}
