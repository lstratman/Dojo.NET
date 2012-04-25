using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Dojo.Net
{
	public class ResourceManagerStream : Stream
	{
		protected Stream InnerStream
		{
			get;
			set;
		}

		protected HttpContext Context
		{
			get;
			set;
		}

		protected static Regex _headTag = new Regex("</head>");

		public ResourceManagerStream(Stream inputStream, HttpContext context)
		{
			InnerStream = inputStream;
			Context = context;
		}

		public override void Flush()
		{
			InnerStream.Flush();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return InnerStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			InnerStream.SetLength(value);
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return InnerStream.Read(buffer, offset, count);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (ResourceManager.Current != null && Context.Response.ContentType.ToLower() == "text/html")
			{
				string bufferedHtml = Context.Response.ContentEncoding.GetString(buffer, offset, count);

				if (_headTag.IsMatch(bufferedHtml))
				{
					bufferedHtml = _headTag.Replace(bufferedHtml, ResourceManager.Current.Render(Context) + "</head>");

					byte[] data = Context.Response.ContentEncoding.GetBytes(bufferedHtml);
					InnerStream.Write(data, 0, data.Length);
				}

				else
					InnerStream.Write(buffer, offset, count);
			}

			else
				InnerStream.Write(buffer, offset, count);
		}

		public override bool CanRead
		{
			get
			{
				return InnerStream.CanRead;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return InnerStream.CanSeek;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return InnerStream.CanWrite;
			}
		}

		public override long Length
		{
			get
			{
				return InnerStream.Length;
			}
		}

		public override long Position
		{
			get
			{
				return InnerStream.Position;
			}

			set
			{
				InnerStream.Position = value;
			}
		}
	}
}
