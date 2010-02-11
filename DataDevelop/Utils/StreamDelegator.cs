using System;
using System.IO;
using System.Text;

namespace DataDevelop.Utils
{
	delegate void StreamWriteHandler(byte[] buffer, int offset, int count);

	class StreamWriteDelegator : Stream
	{
		private StreamWriteHandler write;

		public StreamWriteDelegator(StreamWriteHandler write)
		{
			if (write == null) {
				throw new ArgumentNullException("write");
			}
			this.write = write;
		}

		public override bool CanRead { get { return false; } }
		public override bool CanSeek { get { return false; } }
		public override bool CanWrite { get { return true; } }

		public override long Length { get { throw new NotSupportedException(); } }

		public override long Position
		{
			get { throw new NotSupportedException(); }
			set	{ throw new NotSupportedException(); }
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

		public override void Flush()
		{
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}
	}
}
